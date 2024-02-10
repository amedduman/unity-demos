using System;
using UnityEngine;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        #region fields

        [SerializeField] InputHandler inputHandler;
        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] Grid grid;
        [SerializeField] Tile tilePrefab; 
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] Vector2 buffer;

        Tile[] tiles;
        
        #endregion

        void OnEnable()
        {
            inputHandler.Touch += OnTouch;
        }

        void OnDisable()
        {
            inputHandler.Touch -= OnTouch;
        }

        void Start()
        { 
            tiles = new Tile[width * height];
            GenerateGrid();
            SetCam();
            
            inputHandler.Enable();
        }

        void GenerateGrid()
        {
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
        }

        void SetCam()
        {
            Bounds bounds = new Bounds();
            foreach (Tile tile in tiles)
            {
                bounds.Encapsulate(tile.spriteRenderer.bounds);
            }
            bounds.Expand(new Vector3(buffer.x, buffer.y, 0));
            
            var gridTopRightPos = grid.CellToWorld(new Vector3Int(width, height));
            var camPos = gridTopRightPos / 2;
            
            onGridCreated.Invoke(new GridData(camPos, bounds));
        }
        
        void OnTouch()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var x = grid.WorldToCell(ray.origin);
            Debug.Log(x);
        }
    }
}
