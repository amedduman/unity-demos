using UnityEngine;

namespace Tetris
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        [HideInInspector] public Vector3Int cellPos;
        public SpriteRenderer spriteRenderer;
    }
}