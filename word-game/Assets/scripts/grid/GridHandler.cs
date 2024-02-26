using System.Collections.Generic;
using UnityEngine;

namespace WordGame
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        #region fields

        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] Grid grid;
        [SerializeField] int rows = 3;
        [SerializeField] int columns = 4;
        [SerializeField] Tile tilePrefab; 
        [SerializeField] Vector2 buffer;
        List<Tile> tiles;
        
        #endregion

        void Start()
        {
            GenerateTiles();
            SetCam();
        }
        
        void GenerateTiles()
        {
            // int index = 0;

            tiles = new List<Tile>();
            
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Vector3Int cellPos = new Vector3Int(c, r, 0);
                    var tileWorldPos = grid.GetCellCenterWorld(cellPos);
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);
                    
                    tile.gameObject.name = $"{c}, {r}";
                    
                    tiles.Add(tile);

                    // index++;
                }
            }
        }

        void SetCam()
        {
            Bounds bounds = new Bounds();
            foreach (Tile tile in tiles)
            {
                bounds.Encapsulate(tile.spriteRenderer.bounds);
            }

            Bounds boundsBeforeBuffer = bounds;
            bounds.Expand(new Vector3(buffer.x, buffer.y, 0));
            
            var gridTopRightPos = grid.CellToWorld(new Vector3Int(columns, rows));
            var camPos = gridTopRightPos / 2;
            
            onGridCreated.Invoke(new GridCreatedEventData(camPos, bounds, boundsBeforeBuffer));
        }
    }
}
