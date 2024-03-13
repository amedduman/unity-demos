using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Tetris
{
    public class Block : MonoBehaviour
    {
        GridHandler.Data gridData;
        
        public void OnSpawn(Vector3Int cellPos, GridHandler.Data in_gridData)
        {
            gridData = in_gridData;
            Move(cellPos);
        }
        
        void Move(Vector3Int cellPos)
        {
            transform.DOMove(cellPos, 1f).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
            {
                if (gridData.TryGetDownTile(cellPos, out Tile tile))
                {
                    Move(tile.cellPos);
                }
            });
        }
    }
}
