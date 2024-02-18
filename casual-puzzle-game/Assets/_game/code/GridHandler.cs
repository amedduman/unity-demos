using UnityEngine;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        #region fields

        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] GridData gridData;
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
            int index = 0;
            for (int r = 0; r < gridData.rows; r++)
            {
                for (int c = 0; c < gridData.columns; c++)
                {
                    if (gridData.gridValues[index] == 0)
                    {
                        index++;
                        continue;
                    }
                    Vector3Int cellPos = new Vector3Int(c, r, 0);
                    var tileWorldPos = grid.GetCellCenterWorld(cellPos);
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);
                    
                    tile.gameObject.name = $"{c}, {r}";
                    tile.cellPos = cellPos;

                    if (gridData.gridValues[index] == 5)
                    {
                        tile.IsSpawner = true;
                    }
                    else if (gridData.gridValues[index] == 9)
                    {
                        tile.Freeze();
                    }
                    
                    gridData.SetTileData(this, tile);
                    
                    index++;
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
