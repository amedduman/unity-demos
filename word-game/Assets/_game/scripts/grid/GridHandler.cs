using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WordGame
{
    [RequireComponent(typeof(Grid))]
    public class GridHandler : MonoBehaviour
    {
        #region fields

        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] OnGenerateBtnClicked onGenerateBtnClicked;
        [SerializeField] GridData gridData;
        [SerializeField] Grid grid;
        [SerializeField] Tile tilePrefab; 
        [SerializeField] Vector2 buffer;
        readonly List<Tile> tiles= new List<Tile>();
        
        
        #endregion

        void Awake()
        {
            gridData.ResetData();
        }

        void OnEnable()
        {
            onGenerateBtnClicked.AddListener(Generate);
        }

        void OnDisable()
        {
            onGenerateBtnClicked.RemoveListener(Generate);
        }
        
        void Generate(OnGenerateBtnClicked.Data obj)
        {
            for (int i = tiles.Count - 1; i >= 0; i--)
            {
                Destroy(tiles[i].gameObject);
            }
            
            tiles.Clear();
            gridData.ResetData();
            
            GenerateTiles(obj.rows , obj.columns);
            SetCam(obj.rows , obj.columns);

            gridData.SetTileData(this, tiles.ToList(), in_rows:obj.rows, in_columns:obj.columns);
        }

        void GenerateTiles(int rows, int columns)
        {
            // int index = 0;
            
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

                    // index++;
                }
            }
        }

        void SetCam(int rows, int columns)
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
