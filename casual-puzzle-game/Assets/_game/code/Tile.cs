using System.Collections.Generic;
using UnityEngine;

namespace CasualPuzzle
{
    public class Tile : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer spriteRenderer;
        public Item item { get; set; }
        public Vector3Int gridPos { get; set; }
        public List<Tile> neighbors;
    }
}
