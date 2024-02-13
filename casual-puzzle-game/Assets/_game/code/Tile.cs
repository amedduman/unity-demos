using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CasualPuzzle
{
    public class Tile : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer spriteRenderer;
        public Item item;
        public Vector3Int cellPos { get; set; }
        public bool IsSpawner {get; set; }
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
