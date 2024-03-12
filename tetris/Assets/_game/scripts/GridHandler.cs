using UnityEngine;

namespace Tetris
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        [SerializeField] Grid grid;
        [SerializeField] Tile tilePrefab;
        [SerializeField] int width = 5;
        [SerializeField] int height = 5;
        [SerializeField] Vector2 buffer;

        public void OnStart(CameraController cam)
        {
            GenerateTiles();
            SetCam(cam);
        }
        
        void GenerateTiles()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3Int cellPos = new Vector3Int(x, y, 0);
                    
                    var tileWorldPos = grid.GetCellCenterWorld(cellPos);
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);
                    
                    tile.gameObject.name = $"{x}, {y}";
                    tile.cellPos = cellPos;
                }
            }
        }

        void SetCam(CameraController cam)
        {
            Bounds gridBounds = new Bounds();

            Bounds tileBounds = tilePrefab.spriteRenderer.bounds;
            float gridWidth = (width * tileBounds.size.x);
            float gridHeight = (height * tileBounds.size.y);
            Vector3 gridLocalCenter = new Vector3(gridWidth/2, gridHeight/2, 0);

            gridBounds.size = new Vector3
            (
                gridWidth, 
                gridHeight,
                0
            );
            gridBounds.center = transform.position + gridLocalCenter;
            
            Bounds gridBoundsBeforeBuffer = gridBounds;
            gridBounds.Expand(new Vector3(buffer.x, buffer.y, 0));
            
            cam.SetPositionAndOrthographicSize(gridBounds, gridBoundsBeforeBuffer);
        }
    }
}
