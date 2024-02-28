using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WordGame
{
    public class TileButtons : MonoBehaviour
    {
        [SerializeField] Tile tile;
        [SerializeField] WordCreationDirectionE dir;
        void OnMouseDown()
        {
            tile.SetWordDirection(dir);
        }
    }
}
