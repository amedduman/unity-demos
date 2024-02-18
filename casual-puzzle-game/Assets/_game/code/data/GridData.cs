using System.Collections.Generic;
using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/data/grid data")]
    public class GridData : ScriptableObject
    {
        [field:SerializeField] public int columns { get; private set; } = 5;
        [field:SerializeField] public int rows { get; private set; } = 8;
        public int[] gridValues;
        readonly List<Tile> myTiles  = new List<Tile>();
        public IReadOnlyList<Tile> tiles => myTiles.AsReadOnly();

        // since this is a scriptable object we need to reset its fields
        public void ResetTileData()
        {
            myTiles.Clear();
        }
        
        public void SetTileData(GridHandler gridHandler, Tile tile)
        {
            myTiles.Add(tile);
        }

        [ContextMenu("update")]
        void UpdateGrid()
        {
            gridValues = new int[rows * columns];
            for (int i = 0; i < gridValues.Length; i++)
            {
               gridValues[i] = 1;
            }
        }
    }
}