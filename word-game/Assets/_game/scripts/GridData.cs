using System.Collections.Generic;
using UnityEngine;

namespace WordGame
{
    [CreateAssetMenu(menuName = "_game/data/grid data")]
    public class GridData : ScriptableObject
    {
        List<Tile> myTiles  = new List<Tile>();
        public IReadOnlyList<Tile> tiles => myTiles.AsReadOnly();

        // since this is a scriptable object we need to reset its fields
        public void ResetTileData()
        {
            myTiles.Clear();
        }
        
        public void SetTileData(GridHandler gridHandler, List<Tile> in_tiles)
        {
            myTiles = in_tiles;
        }
    }
}