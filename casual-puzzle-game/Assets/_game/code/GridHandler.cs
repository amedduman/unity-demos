using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {

        #region fields

        [SerializeField] Grid grid;
        [SerializeField] Tile tilePrefab; 
        [SerializeField] int width;
        [SerializeField] int height;

        Tile[] tiles;
        
        #endregion
        
        void Start()
        {
            tiles = new Tile[width * height];
            int index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tileWorldPos = grid.GetCellCenterWorld(new Vector3Int(x,y,0));
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);
                    
                    tiles[index] = tile;
                    index++;
                }
            }

            Bounds bounds = new Bounds();
            foreach (Tile tile in tiles)
            {
                bounds.Encapsulate(tile.spriteRenderer.bounds);
            }
            bounds.Encapsulate(new Vector3(1,1,0)); 
            var cam = FindObjectOfType<CameraController>();
            
            var gridTopRightPos = grid.CellToWorld(new Vector3Int(width, height));
            var camPos = gridTopRightPos / 2;
            
            cam.SetPositionAndOrthographicSize(camPos, bounds);
            
        }
    }
}
