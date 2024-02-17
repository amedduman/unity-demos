using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CasualPuzzle
{
    public class GameplayHandler : MonoBehaviour
    {
        #region fields

        [SerializeField] InputHandler inputHandler;
        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] OnSwipeInput onSwipeInput;
        [FormerlySerializedAs("tileData")] [SerializeField] GridData gridData;
        [SerializeField] SwipedCellData swipedCellData;
        [SerializeField] ItemsToSpawnData itemsToSpawnData;
        bool ignoreInput;

        #endregion

        void Awake()
        {
            ignoreInput = true;
        }

        void OnEnable()
        {
            onGridCreated.AddListener(SpawnItems);
            onSwipeInput.AddListener(HandleSwipe, 10);
        }

        void OnDisable()
        {
            onGridCreated.RemoveListener(SpawnItems);
            onSwipeInput.RemoveListener(HandleSwipe);
        }

        void Start()
        {
            inputHandler.Enable();
        }

        void SpawnItems(GridCreatedEventData data)
        {
            do
            {
                for (int i = gridData.tiles.Count - 1; i >= 0; i--)
                {
                    if(gridData.tiles[i].item != null)
                        Destroy(gridData.tiles[i].item.gameObject);
                }
                foreach (Tile tile in gridData.tiles)
                {
                    var tr = tile.transform;
                    var i = Instantiate(GetRandomItemPrefab(), tr.position, Quaternion.identity, tr);
                    tile.item = i;
                }    
            } while (HasMatch());

            ignoreInput = false;
        }
        
        void HandleSwipe(SwipeData swipeData)
        {
            if (ignoreInput)return;

            // var cellUnderCursor = swipedCellData.cellPos;
            // if (DoesCellHaveTile(cellUnderCursor))
            // {
            //     var adjacentCell = GetAdjacentCell(cellUnderCursor, swipeData.swipe);
            //     if (DoesCellHaveTile(adjacentCell))
            //     {
            //         var a =GetTileInTheCell(cellUnderCursor);
            //         var b = GetTileInTheCell(adjacentCell);
            //         TrySwapTileItems(a,b);
            //     }
            // }

            var cellUnderCursor = swipedCellData.cellPos;
            if (TryGetTileInTheCell(out Tile tile, cellUnderCursor))
            {
                if (tile.IsFrozen()) return;
                var adjacentCell = GetAdjacentCell(cellUnderCursor, swipeData.swipe);
                if (TryGetTileInTheCell(out Tile adjacentTile, adjacentCell))
                {
                    TrySwapTileItems(tile,adjacentTile);
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
        
        bool DoesCellHaveTile(Vector3Int gridPos)
        {
            foreach (Tile tile in gridData.tiles)
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
            foreach (Tile tile in gridData.tiles)
            {
                if (tile.cellPos.x == gridPos.x && tile.cellPos.y == gridPos.y)
                    return tile;
            }

            throw new NotImplementedException();
        }

        bool TryGetTileInTheCell(out Tile tile, Vector3Int cell)
        {
            foreach (Tile t in gridData.tiles)
            {
                if (t.cellPos.x == cell.x && t.cellPos.y == cell.y)
                {
                    tile = t;
                    return true;
                }
            }

            tile = null;
            return false;
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
            foreach (Tile tile in gridData.tiles)
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
                    var t = matchTile.HandleMatch();
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
                foreach (Tile tile in gridData.tiles)
                {
                    if (tile.item == null)
                    {
                        if (tile.IsSpawner)
                        {
                            var tr = tile.transform;
                            var item = Instantiate(GetRandomItemPrefab(), tr.position + Vector3Int.up, Quaternion.identity, tr);
                            tile.item = item;
                        }
                        else if (tile.TryGetNearestUpperFullTile(out Tile upperFullTile))
                        {
                            tile.item = upperFullTile.item;
                            upperFullTile.item = null;
                        }
                    }
                }
                
                var s = DOTween.Sequence();
                foreach (Tile tile in gridData.tiles)
                {
                    if (tile.item != null)
                    {
                        var t = tile.SetItemPos();
                        s.Join(t);
                    }
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
            foreach (Tile tile in gridData.tiles)
            {
                if (tile.item == null)
                {
                    return true;
                }
            }

            return false;
        }
        
        bool HasMatch()
        {
            foreach (Tile tile in gridData.tiles)
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
                    if (t.item.myType == tile.item.myType && t.IsFrozen() == false)
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
        
        Item GetRandomItemPrefab()
        {
            var rnd = Random.Range(0, itemsToSpawnData.items.Count);
            return itemsToSpawnData.items[rnd];
        }

    }
    
    public struct MatchSearchResult
    {
        public int horizontalMatchCount;
        public int verticalMatchCount;
    }
}
