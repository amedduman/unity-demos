using System;
using UnityEngine;

namespace WordGame
{
    [CreateAssetMenu(menuName = "_game/data/WordCreationData")]
    public class WordCreationData : ScriptableObject
    {
        public Tile startingTile { get; private set; }
        public WordCreationDirectionE direction { get; private set; }

        public void SetData(Tile dataSetter, Tile tile, WordCreationDirectionE dir)
        {
            startingTile = tile;
            direction = dir;
        }

        void OnDisable()
        {
            startingTile = null;
            direction = WordCreationDirectionE.none;
        }
    }

    public enum WordCreationDirectionE
    {
        none = 0,
        right = 10,
        down = 20,
    }
}
