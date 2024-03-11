using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tetris
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        [SerializeField] Grid grid;
        [SerializeField] Tile tilePrefab;
        [SerializeField] int rows, columns = 5;
        [SerializeField] Vector2 buffer;

        // Vector3 cellSize;
        readonly List<Tile> tiles = new List<Tile>();


        public void OnStart(CameraController cam)
        {
            // cellSize = grid.cellSize;
            GenerateTiles();
            SetCam(cam);
        }
        
        void GenerateTiles()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Vector3Int cellPos = new Vector3Int(c, r, 0);
                    
                    var tileWorldPos = grid.GetCellCenterWorld(cellPos);
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);
                    
                    tile.gameObject.name = $"{c}, {r}";
                    tile.cellPos = cellPos;
                    
                    tiles.Add(tile);
                }
            }
        }

        Bounds bo;
        void SetCam(CameraController cam)
        {
            Bounds bounds = new Bounds();
            
            float width = (columns * tiles[0].spriteRenderer.bounds.size.x);
            float height = (rows * tiles[0].spriteRenderer.bounds.size.y);
            Vector3 gridLocalCenter = new Vector3(width/2, height/2, 0);

            bounds.size = new Vector3
            (
                width, 
                height,
                0
            );
            bounds.center = transform.position + gridLocalCenter;
            
            Bounds boundsBeforeBuffer = bounds;
            bounds.Expand(new Vector3(buffer.x, buffer.y, 0));
            
            cam.SetPositionAndOrthographicSize(bounds, boundsBeforeBuffer);
        }
    }
}
