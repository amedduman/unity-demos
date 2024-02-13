using System.Collections.Generic;
using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/data/tile data")]
    public class TileData : ScriptableObject
    {
        public List<Tile> tiles { get; private set; } = new List<Tile>();

        // since this is a scriptable object we need to reset its fields
        public void ResetTileData()
        {
            tiles.Clear();
        }

        public void SetTileData(GridHandler gridHandler, Tile tile)
        {
            tiles.Add(tile);
        }
    }
}