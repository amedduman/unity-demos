using DG.Tweening;
using UnityEngine;

namespace Tetris
{
    public class Block : MonoBehaviour
    {
        public void OnSpawn(Vector3Int cellPos)
        {
            Move(cellPos);
        }
        
        void Move(Vector3Int cellPos)
        {
            transform.DOMove(cellPos, 1f)
                .SetEase(Ease.Linear)
                .SetSpeedBased()
                .OnComplete(OnMoveComplete);

            void OnMoveComplete()
            {
                if (GridHandler.TryGetDownTileCellPos(cellPos, out Vector3Int downCellPos))
                {
                    Move(downCellPos);
                }
                else
                {
                    GameplaySequenceHandler.
                }
            }
        }
    }
}
