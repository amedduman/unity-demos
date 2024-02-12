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

            tiles = new Tile[width * height];
            GenerateGrid();
            SetCam();
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
                    tile.cellPos = gridPos;
                    tiles[index] = tile;
                    index++;
                }
            }
        }

        void SetCam()
        {
            Bounds bounds = new Bounds();
            foreach (Tile tile in tiles)
            {
                bounds.Encapsulate(tile.spriteRenderer.bounds);
            }

            Bounds boundsBeforeBuffer = bounds;
            bounds.Expand(new Vector3(buffer.x, buffer.y, 0));
            
            var gridTopRightPos = grid.CellToWorld(new Vector3Int(width, height));
            var camPos = gridTopRightPos / 2;
            
            onGridCreated.Invoke(new GridData(camPos, bounds, boundsBeforeBuffer));
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
                    ClearMatches();
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

        void ClearMatches()
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
                        matchTiles.Add(GetTileInTheCell(tile.cellPos + Vector3Int.right * i));
                    }
                }

                if (vMatch >= 3)
                {
                    for (int i = 0; i < vMatch; i++)
                    {
                        matchTiles.Add(GetTileInTheCell(tile.cellPos + Vector3Int.up * i));
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
                HashSet<Tile> emptiedTiles = new HashSet<Tile>();
            
                foreach (Tile tile in tiles)
                {
                    if (tile.item == null)
                    {
                        var upGridCell = tile.cellPos + Vector3Int.up;
                        if (DoesCellHaveTile(upGridCell))
                        {
                            var upTile = GetTileInTheCell(upGridCell);
                            if (upTile.item != null)
                            {
                                tile.item = upTile.item;
                                upTile.item = null;
                                emptiedTiles.Add(upTile);
                                itemsToMoveDown.Add(tile.item);
                            }
                        }
                        else
                        {
                            emptiedTiles.Add(tile);
                        }
                    }
                }

                var s = DOTween.Sequence();

                foreach (Tile emptiedTile in emptiedTiles)
                {
                    if (emptiedTile.cellPos.y == height - 1)
                    {
                        var item = Instantiate(GetRandomItem(), emptiedTile.transform.position + Vector3Int.up, Quaternion.identity, emptiedTile.transform);
                        emptiedTile.item = item;
                        itemsToMoveDown.Add(item);
                    }
                }
                
                foreach (Item item in itemsToMoveDown)
                {
                    var t = item.MoveToPos(item.transform.position + Vector3Int.down);
                    s.Join(t);
                }

                yield return new DOTweenCYInstruction.WaitForCompletion(s);
            }
            
            if (HasMatch())
                ClearMatches();
            else
                ignoreInput = false;
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
                if (tile.cellPos.x == gridPos.x && tile.cellPos.y == gridPos.y)
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
                if (tile.cellPos.x == gridPos.x && tile.cellPos.y == gridPos.y)
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
                    var i = Instantiate(GetRandomItem(), tile.transform.position, Quaternion.identity, tile.transform);
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

        MatchSearchResult GetMatchCount(Tile tile)
        {
            var gridPos = tile.cellPos;
            MatchSearchResult result = new MatchSearchResult();
            
            
            List<Vector3Int> directions = new List<Vector3Int>
            {
                Vector3Int.right, // horizontal
                Vector3Int.up, // vertical
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

        Item GetRandomItem()
        {
            var rnd = Random.Range(0, items.Count);
            return items[rnd];
        }
    }

    public struct MatchSearchResult
    {
        public int horizontalMatchCount;
        public int verticalMatchCount;
    }
}
