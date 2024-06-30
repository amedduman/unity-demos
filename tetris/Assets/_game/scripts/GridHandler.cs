using System.Collections;
using System.Collections.Generic;
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

        public static GridHandler instance;
        readonly List<Tile> tiles = new List<Tile>();
        Vector3Int cellSize = new Vector3Int(1,1,1);

        public Coroutine Init(CameraController cam)
        {
            cellSize = new Vector3Int((int)grid.cellSize.x, (int)grid.cellSize.y, (int)grid.cellSize.z);
            var co = StartCoroutine(GenerateTiles());
            SetCam(cam);
            return co;
        }
        
        IEnumerator GenerateTiles()
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
                    tiles.Add(tile);
                    yield return null;
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
        
        public bool SetTilesOccupied()
        {
            width = 4;
            return true;
        }

        #region static

        public static int Width => instance.width;
        public static int Height => instance.height;
        public static Vector3Int TopBlockCellPos
        {
            get
            {
                var index = (instance.height * instance.width) - Mathf.FloorToInt((float)instance.width/2 + 1);
                return instance.tiles[index].cellPos;
            }
        }
        
        public static bool TryGetDownTileCellPos(Vector3Int cell, out Vector3Int downCell)
        {
            cell.y -= instance.cellSize.y;
            foreach (var t in instance.tiles)
            {
                if (t.cellPos.x == cell.x && t.cellPos.y == cell.y)
                {
                    downCell = t.cellPos;
                    return true;
                }
            }

            downCell = Vector3Int.zero;
            return false;
        }

        #endregion
    }
}
