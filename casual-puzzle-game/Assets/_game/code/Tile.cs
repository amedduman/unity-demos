using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CasualPuzzle
{
    public class Tile : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer spriteRenderer;
        public Item item { get; set; }
        public Vector3Int gridPos { get; set; }
        public List<Tile> neighbors;

        public Tween SetItemPos()
        { 
            return item.MoveToPos(transform.position);
        }

        public Tween DestroyItem()
        {
            var t = item.DestroyProcess();
            var x = item.destroyCancellationToken;
            x.Register(DoStuff);
            item = null;
            return t;
        }

        void DoStuff()
        {
            Debug.Log("item is destroyed");
        }
    }
}
