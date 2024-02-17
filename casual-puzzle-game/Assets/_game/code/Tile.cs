using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace CasualPuzzle
{
    public class Tile : MonoBehaviour
    {

        #region fields

        [FormerlySerializedAs("tileData")] [SerializeField] GridData gridData;
        // [SerializeField] GridData gridData;
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] SpriteRenderer iceSprite;
        [HideInInspector] public Item item;
        [HideInInspector] public Vector3Int cellPos;
        [HideInInspector] public bool IsSpawner;

        #endregion

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

            for (int i = 0; i < gridData.columns; i++)
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
        
        public void Freeze()
        {
            iceSprite.enabled = true;
        }

        void UnFreeze()
        {
            iceSprite.enabled = false;
        }

        public bool IsFrozen()
        {
            return iceSprite.enabled;
        }

        void HandleNeighborMatch()
        {
            if (IsFrozen() == false) return;
            UnFreeze();
        }
        
        public Tween SetItemPos()
        { 
            return item.MoveToPos(transform.position);
        }

        public Tween HandleMatch()
        {
            LetTheNeighborTilesKnowAboutMatch();
            var t = item.DestroyProcess();
            item = null;
            return t;
        }

        void LetTheNeighborTilesKnowAboutMatch()
        {
            var directions = new List<Vector3Int>
            {
                Vector3Int.right, 
                Vector3Int.left, 
                Vector3Int.up, 
                Vector3Int.down
            };
            foreach (var dir in directions)
            {
                if (TryGetTileInTheCell(out Tile tile, cellPos + dir))
                {
                    tile.HandleNeighborMatch();
                }
            }
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
        
    }
}
