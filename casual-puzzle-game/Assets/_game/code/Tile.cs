using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CasualPuzzle
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] ItemsToSpawnData itemPrefabs;
        [SerializeField] TileData tileData;
        [SerializeField] GridData gridData;
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] SpriteRenderer iceSprite;
        [HideInInspector] public Item item;
        [HideInInspector] public Vector3Int cellPos;
        [HideInInspector] public bool IsSpawner;

        public bool TryGetNearestUpperFullTile(out Tile upperTile)
        {
            Vector3Int cell = cellPos;
            var oneUpCell = cell + Vector3Int.up;
            if (DoesCellHaveTile(oneUpCell))
            {
                var t = GetTileInTheCell(oneUpCell);
                if (t.IsFrozen() == false)
                {
                    upperTile = t;
                    return true;
                }
            }

            Vector3Int nearestRightUpCell = cell + Vector3Int.up;
            Vector3Int nearestLeftUpCell = cell + Vector3Int.up;

            for (int i = 0; i < gridData.width; i++)
            {
                nearestRightUpCell += Vector3Int.right;
                if (DoesCellHaveTile(nearestRightUpCell))
                {
                    var t = GetTileInTheCell(nearestRightUpCell);
                    if (t.IsFrozen() == false)
                    {
                        upperTile = t;
                        return true;
                    }
                }
                
                nearestLeftUpCell += Vector3Int.left;
                if (DoesCellHaveTile(nearestLeftUpCell))
                {
                    var t = GetTileInTheCell(nearestLeftUpCell);
                    if (t.IsFrozen() == false)
                    {
                        upperTile = t;
                        return true;
                    }
                }
            }

            upperTile = null;
            return false;
        }
        
        bool DoesCellHaveTile(Vector3Int gridPos)
        {
            foreach (Tile tile in tileData.tiles)
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
            foreach (Tile tile in tileData.tiles)
            {
                if (tile.cellPos.x == gridPos.x && tile.cellPos.y == gridPos.y)
                    return tile;
            }

            throw new NotImplementedException();
        }
        
        public void Freeze()
        {
            iceSprite.enabled = true;
        }

        public void SpawnItem()
        {
            var tr = transform;
            var i = Instantiate(GetRandomItemPrefab(), tr.position, Quaternion.identity, tr);
            item = i;
        }
        
        Item GetRandomItemPrefab()
        {
            var rnd = Random.Range(0, itemPrefabs.items.Count);
            return itemPrefabs.items[rnd];
        }
        
        public void TryDestroyItemImmediate()
        {
            if (item != null)
            {
                Destroy(item.gameObject);
                item = null;
            }
        }

        public bool IsFrozen()
        {
            return iceSprite.enabled;
        }
        
        public Tween SetItemPos()
        { 
            return item.MoveToPos(transform.position);
        }

        public Tween DestroyItem()
        {
            var t = item.DestroyProcess();
            item = null;
            return t;
        }
    }
}
