using UnityEngine;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        #region fields

        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] GridData gridData;
        [SerializeField] TileData tileData;
        [SerializeField] Grid grid;
        [SerializeField] Tile tilePrefab; 
        
        [SerializeField] Vector2 buffer;
        

        Tile[] tiles;
        Camera cam;
        
        #endregion

        void Start()
        {
            cam = Camera.main;

            tiles = new Tile[gridData.width * gridData.height];
            tileData.InitTileArray(this, gridData.width, gridData.height);
            GenerateTiles();
            SetCam();
        }
        
        void GenerateTiles()
        {
            int index = 0;
            for (int x = 0; x < gridData.width; x++)
            {
                for (int y = 0; y < gridData.height; y++)
                {
                    Vector3Int cellPos = new Vector3Int(x, y, 0);
                    var tileWorldPos = grid.GetCellCenterWorld(cellPos);
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);

                    tile.gameObject.name = $"{x}, {y}";
                    tile.cellPos = cellPos;
                    tiles[index] = tile;
                    tileData.SetTileData(this, index, tile);
                    index++;
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
            
            var gridTopRightPos = grid.CellToWorld(new Vector3Int(gridData.width, gridData.height));
            var camPos = gridTopRightPos / 2;
            
            onGridCreated.Invoke(new GridCreatedEventData(camPos, bounds, boundsBeforeBuffer));
        }
    }
}
