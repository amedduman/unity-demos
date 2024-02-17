using System.Collections.Generic;
using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/data/tile data")]
    public class TileData : ScriptableObject
    {
        public int[] gridValues = new int[8];
        public List<Tile> tiles { get; private set; } = new List<Tile>();

        // since this is a scriptable object we need to reset its fields
        public void ResetTileData()
        {
            tiles.Clear();
        }
        
        [ContextMenu("fill array")]
        void fillArray()
        {
            gridValues = new int[48];
            for (int i = 0; i < gridValues.Length; i++)
            {
                gridValues[i] = i;
            }
            
            for (int i = 0; i < gridValues.Length; i++)
            {
                UnityEngine.Debug.Log(gridValues[i]);
            }
        }

        public void SetTileData(GridHandler gridHandler, Tile tile)
        {
            tiles.Add(tile);
        }
    }
}