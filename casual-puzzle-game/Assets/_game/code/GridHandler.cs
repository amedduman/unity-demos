using UnityEngine;
using UnityEngine.Serialization;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        #region fields

        [SerializeField] OnGridCreated onGridCreated;
        // [SerializeField] GridData gridData;
        [FormerlySerializedAs("tileData")] [SerializeField] GridData gridData;
        [SerializeField] Grid grid;
        [SerializeField] Tile tilePrefab; 
        
        [SerializeField] Vector2 buffer;
        
        #endregion

        void Start()
        {
            GenerateTiles();
            SetCam();
        }
        
        void GenerateTiles()
        {
            gridData.ResetTileData();
            
            for (int x = 0; x < gridData.columns; x++)
            {
                for (int y = 0; y < gridData.rows; y++)
                {
                    if (x == 3 && y == 4) continue;
                    // if (x == 2 && y == 4) continue;
                    // if (x == 2 && y == 3) continue;
                    if (x == 1 && y == 4) continue;
                    Vector3Int cellPos = new Vector3Int(x, y, 0);
                    var tileWorldPos = grid.GetCellCenterWorld(cellPos);
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);

                    tile.gameObject.name = $"{x}, {y}";
                    tile.cellPos = cellPos;
                    if (tile.cellPos.y == gridData.rows - 1)
                    {
                        tile.IsSpawner = true;
                    }

                    if (tile.cellPos is { x: 2, y: 3 })
                    {
                        tile.Freeze();
                    }
                    if (tile.cellPos is { x: 2, y: 4 })
                    {
                        tile.Freeze();
                    }
                    gridData.SetTileData(this, tile);
                }
            }
        }

        void SetCam()
        {
            Bounds bounds = new Bounds();
            foreach (Tile tile in gridData.tiles)
            {
                bounds.Encapsulate(tile.spriteRenderer.bounds);
            }

            Bounds boundsBeforeBuffer = bounds;
            bounds.Expand(new Vector3(buffer.x, buffer.y, 0));
            
            var gridTopRightPos = grid.CellToWorld(new Vector3Int(gridData.columns, gridData.rows));
            var camPos = gridTopRightPos / 2;
            
            onGridCreated.Invoke(new GridCreatedEventData(camPos, bounds, boundsBeforeBuffer));
        }
    }
}
