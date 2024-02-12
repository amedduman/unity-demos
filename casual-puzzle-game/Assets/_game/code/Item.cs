using System;
using DG.Tweening;
using UnityEngine;

namespace CasualPuzzle
{
    public enum ItemE
    {
        none = 0,
        Blue = 10,
        Red = 20,
        Green = 30,
        Yellow = 40,
        Orange = 50,
    }
    
    public class Item : MonoBehaviour
    {
        [field:SerializeField] public ItemE myType { get; private set; }

        public Tween MoveToPos(Vector3 pos)
        {
            return transform.DOMove(pos, .3f);
        }

        void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}
