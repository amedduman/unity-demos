using System;
using System.Collections.Generic;
using UnityEngine;

namespace WordGame
{
    public class Tile : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Vector3Int cellPos;
        [SerializeField] GameObject Buttons;

        public void OnTileSelected()
        {
            Buttons.SetActive(true);
        }
    }
}
