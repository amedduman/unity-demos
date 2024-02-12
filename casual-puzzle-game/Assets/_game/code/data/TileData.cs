using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/data/tile data")]
    public class TileData : ScriptableObject
    {
        public Tile[] tiles { get; private set; }

        public void InitTileArray(GridHandler gridHandler, int width, int height)
        {
            tiles = new Tile[width * height];
        }
        
        public void SetTileData(GridHandler gridHandler, int index, Tile tile)
        {
            tiles[index] = tile;
        }
    }
}