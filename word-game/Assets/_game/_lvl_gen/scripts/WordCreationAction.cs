using System;
using UnityEngine;

namespace WordGame
{
    [CreateAssetMenu(menuName = "_game/data/WordCreationData")]
    public class WordCreationAction : EventSO<WordCreationAction.Data>
    {
        Tile startingTile;
        WordCreationDirectionE direction;
        string word;

        public void SetData(Tile dataSetter, Tile tile, WordCreationDirectionE dir)
        {
            startingTile = tile;
            direction = dir;
        }

        public void SetWord(string in_word)
        {
            word = in_word;
        }

        public void InvokeEvent()
        {
            Invoke(new Data(startingTile, direction, word));
        }

        void OnDisable()
        {
            startingTile = null;
            direction = WordCreationDirectionE.none;
            word = string.Empty;
        }

        public struct Data
        {
            public Tile tile { get; private set; }
            public WordCreationDirectionE dir { get; private set; }
            public string word { get; private set; }

            public Data(Tile tile, WordCreationDirectionE dir, string word)
            {
                this.tile = tile;
                this.dir = dir;
                this.word = word;
            }
        }
    }

    public enum WordCreationDirectionE
    {
        none = 0,
        right = 10,
        down = 20,
    }
}
