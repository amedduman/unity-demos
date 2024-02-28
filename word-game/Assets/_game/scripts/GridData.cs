using System.Collections.Generic;
using UnityEngine;

namespace WordGame
{
    // [CreateAssetMenu(menuName = "_game/data/grid data")]
    // public class GridData : ScriptableObject
    // {
    //     public int rows { get; private set; }
    //     public int columns { get; private set; }
    //     List<Tile> myTiles  = new List<Tile>();
    //     public IReadOnlyList<Tile> tiles => myTiles.AsReadOnly();
    //
    //     // since this is a scriptable object we need to reset its fields
    //     public void ResetData()
    //     {
    //         myTiles.Clear();
    //         rows = 0;
    //         columns = 0;
    //     }
    //     
    //     public void SetTileData(GridHandler gridHandler, List<Tile> in_tiles, int in_rows, int in_columns)
    //     {
    //         myTiles = in_tiles;
    //         rows = in_rows;
    //         columns = in_columns;
    //     }
    //
    //     void OnDisable()
    //     {
    //         ResetData();
    //     }
    // }
}