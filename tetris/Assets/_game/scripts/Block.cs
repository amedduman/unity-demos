using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Tetris
{
    public class Block : MonoBehaviour
    {
        public void Move(Vector3Int cellPos)
        {
            transform.DOMove(cellPos, 1f).SetEase(Ease.Linear);
        }
    }
}
