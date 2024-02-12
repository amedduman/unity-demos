using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        #region fields

        [SerializeField] InputHandler inputHandler;
        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] OnSwipeInput onSwipeInput;
        [SerializeField] Grid grid;
        [SerializeField] Tile tilePrefab; 
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] Vector2 buffer;
        [SerializeField] List<Item> items;

        Tile[] tiles;
        bool ignoreInput;
        Camera cam;
        Vector3Int[] directions;
        
        #endregion

        void OnEnable()
        {
            onSwipeInput.AddListener(HandleSwipe);
        }

        void OnDisable()
        {
            onSwipeInput.RemoveListener(HandleSwipe);
        }

        void Start()
        {
            cam = Camera.main;

            directions = new[]
            {
                Vector3Int.right,
                Vector3Int.left,
                Vector3Int.up,
                Vector3Int.down
            };
            
            tiles = new Tile[width * height];
            GenerateGrid();
            SetCam();
            SetNeighbors();
            SpawnItems();
            
            inputHandler.Enable();
        }

        void GenerateGrid()
        {
            int index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3Int gridPos = new Vector3Int(x, y, 0);
                    var tileWorldPos = grid.GetCellCenterWorld(gridPos);
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);

                    tile.gameObject.name = $"{x}, {y}";
                    tile.gridPos = gridPos;
                    tiles[index] = tile;
                    index++;
                }
            }
        }

        void SetCam()
        {
            Bounds bounds = new Bounds();
            Bounds boundsBeforeBuffer = new Bounds();
            foreach (Tile tile in tiles)
            {
                bounds.Encapsulate(tile.spriteRenderer.bounds);
            }

            boundsBeforeBuffer = bounds;
            bounds.Expand(new Vector3(buffer.x, buffer.y, 0));
            
            var gridTopRightPos = grid.CellToWorld(new Vector3Int(width, height));
            var camPos = gridTopRightPos / 2;
            
            onGridCreated.Invoke(new GridData(camPos, bounds, boundsBeforeBuffer));
        }

        void SetNeighbors()
        {
            foreach (Tile tile in tiles)
            {
                Vector3Int left = tile.gridPos + new Vector3Int(-1, 0, 0);
                Vector3Int right = tile.gridPos + new Vector3Int(1, 0, 0);
                Vector3Int up = tile.gridPos + new Vector3Int(0, 1, 0);
                Vector3Int down = tile.gridPos + new Vector3Int(0, -1, 0);

                List<Vector3Int> cells = new List<Vector3Int>
                {
                    left,
                    right,
                    up,
                    down
                };

                foreach (Vector3Int cell in cells)
                {
                    if(DoesCellHaveTile(cell))
                        tile.neighbors.Add(GetTileInTheCell(cell));
                }
            }
        }
        
        void HandleSwipe(SwipeData swipeData)
        {
            if (ignoreInput)return;

            var worldPos = cam.ScreenToWorldPoint(new Vector3(swipeData.touchStartPos.x, swipeData.touchStartPos.y, 0));
            var cellUnderCursor = grid.WorldToCell(worldPos);
            if (DoesCellHaveTile(cellUnderCursor))
            {
                
                var adjacentCell = GetAdjacentCell(cellUnderCursor, swipeData.swipe);
                if (DoesCellHaveTile(adjacentCell))
                {
                    var a =GetTileInTheCell(cellUnderCursor);
                    var b = GetTileInTheCell(adjacentCell);
                    TrySwapTileItems(a,b);
                }
            }
        }

        Vector3Int GetAdjacentCell(Vector3Int cell, SwipeE swipe)
        {
            Vector3Int result = Vector3Int.zero;
            switch (swipe)
            {
                case SwipeE.none:
                    break;
                case SwipeE.right:
                    result = cell + Vector3Int.right;
                    break;
                case SwipeE.left:
                    result = cell + Vector3Int.left;
                    break;
                case SwipeE.up:
                    result = cell + Vector3Int.up;
                    break;
                case SwipeE.down:
                    result = cell + Vector3Int.down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(swipe), swipe, null);
            }

            return result;
        }

        void TrySwapTileItems(Tile a, Tile b)
        {
            ignoreInput = true;
            StartCoroutine(SwapCo());
            return;

            IEnumerator SwapCo()
            {
                yield return new DOTweenCYInstruction.WaitForCompletion(Swap());

                if (HasMatch())
                {
                    CleanMatches();
                    yield break;
                }
                
                yield return new DOTweenCYInstruction.WaitForCompletion(Swap());

                ignoreInput = false;
            }

            Sequence Swap()
            {
                (a.item, b.item) = (b.item, a.item);
                var s = DOTween.Sequence();
                s.Join(a.SetItemPos());
                s.Join(b.SetItemPos());
                return s;
            }
        }

        void CleanMatches()
        {
            HashSet<Tile> matchTiles = new HashSet<Tile>();
            foreach (Tile tile in tiles)
            {
                int hMatch = GetMatchCount(tile).horizontalMatchCount;
                int vMatch = GetMatchCount(tile).verticalMatchCount;
                
                if (hMatch >= 3)
                {
                    for (int i = 0; i < hMatch; i++)
                    {
                        matchTiles.Add(GetTileInTheCell(tile.gridPos + Vector3Int.right * i));
                    }
                }

                if (vMatch >= 3)
                {
                    for (int i = 0; i < vMatch; i++)
                    {
                        matchTiles.Add(GetTileInTheCell(tile.gridPos + Vector3Int.up * i));
                    }
                }
                    
            }

            StartCoroutine(DestroyItems());
            return;

            IEnumerator DestroyItems()
            {
                var s = DOTween.Sequence();
                foreach (Tile matchTile in matchTiles)
                {
                    var t = matchTile.DestroyItem();
                    s.Join(t);
                }
                yield return new DOTweenCYInstruction.WaitForCompletion(s);

                StartCoroutine(MoveItems());
            }
        }

        IEnumerator MoveItems()
        {
            while (HasEmptyTile())
            {
                HashSet<Item> itemsToMoveDown = new HashSet<Item>();
            
                foreach (Tile tile in tiles)
                {
                    if (tile.item == null)
                    {
                        var upGridCell = tile.gridPos + Vector3Int.up;
                        if (DoesCellHaveTile(upGridCell))
                        {
                            var upTile = GetTileInTheCell(upGridCell);
                            if (upTile.item != null)
                            {
                                tile.item = upTile.item;
                                upTile.item = null;
                                itemsToMoveDown.Add(tile.item);
                            }
                        }
                    }
                }

                var s = DOTween.Sequence();
                
                foreach (Item item in itemsToMoveDown)
                {
                    var t = item.MoveToPos(item.transform.position + Vector3Int.down);
                    s.Join(t);
                }

                yield return new DOTweenCYInstruction.WaitForCompletion(s);
            }
        }

        bool HasEmptyTile()
        {
            foreach (Tile tile in tiles)
            {
                if (tile.item == null)
                {
                    return true;
                }
            }

            return false;
        }

        bool DoesCellHaveTile(Vector3Int gridPos)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.gridPos.x == gridPos.x && tile.gridPos.y == gridPos.y)
                {
                    return true;
                }
            }

            return false;
        }

        Tile GetTileInTheCell(Vector3Int gridPos)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.gridPos.x == gridPos.x && tile.gridPos.y == gridPos.y)
                    return tile;
            }

            throw new NotImplementedException();
        }

        void SpawnItems()
        {
            do
            {
                for (int i = tiles.Length - 1; i >= 0; i--)
                {
                    if(tiles[i].item != null)
                        Destroy(tiles[i].item.gameObject);
                }
                foreach (Tile tile in tiles)
                {
                    var i = Instantiate(GetRandom(), tile.transform.position, Quaternion.identity, tile.transform);
                    tile.item = i;
                }    
            } while (HasMatch());
            
        }

        bool HasMatch()
        {
            foreach (Tile tile in tiles)
            {
                if (GetMatchCount(tile).horizontalMatchCount >= 3 || GetMatchCount(tile).verticalMatchCount   >= 3)
                    return true;
            }
            return false;
        }

        [ContextMenu("mark tiles")]
        void MarkTiles()
        {
            foreach (Tile tile in tiles)
            {
                if (GetMatchCount(tile).horizontalMatchCount >= 3 || GetMatchCount(tile).verticalMatchCount   >= 3)
                    tile.spriteRenderer.color = Color.blue;
            }
        }

        MatchSearchResult GetMatchCount(Tile tile)
        {
            var gridPos = tile.gridPos;
            MatchSearchResult result = new MatchSearchResult();
            
            
            List<Vector3Int> directions = new List<Vector3Int>
            {
                new Vector3Int(1, 0, 0), // horizontal
                new Vector3Int(0, 1, 0), // vertical
            };

            for (int i = 0; i < directions.Count; i++)
            {
                var currentCellGridPos = gridPos + directions[i];
                int matchCount = 1;
            
                while (DoesCellHaveTile(currentCellGridPos))
                {
                    var t = GetTileInTheCell(currentCellGridPos);
                    if (t.item.myType == tile.item.myType)
                        matchCount++;
                    else
                        break;
                    currentCellGridPos += directions[i];
                }

                if (i == 0)
                {
                    result.horizontalMatchCount = matchCount;
                }
                else
                {
                    result.verticalMatchCount = matchCount;
                }
            }

            return result;
        }

        Item GetRandom()
        {
            var rnd = Random.Range(0, items.Count);
            return items[rnd];
        }
    }

    public struct MatchSearchResult
    {
        public MatchSearchResult(int h = 0, int v = 0)
        {
            horizontalMatchCount = 0;
            verticalMatchCount = 0;
        }
        public int horizontalMatchCount;
        public int verticalMatchCount;
        // public int totalMatchCount => horizontalMatchCount + verticalMatchCount;

    }
}
