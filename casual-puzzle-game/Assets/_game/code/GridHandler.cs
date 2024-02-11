using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        [SerializeField] List<Item> items;

        Tile[] tiles;
        
        #endregion

        void OnEnable()
        {
            inputHandler.TouchStart += OnTouchStart;
        }

        void OnDisable()
        {
            inputHandler.TouchStart -= OnTouchStart;
        }

        void Start()
        { 
            tiles = new Tile[width * height];
            GenerateGrid();
            SetCam();
            SetNeighbors();
            SpawnItems();
            
            inputHandler.Enable();
        }

        void GenerateGrid()
        {
            int index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3Int gridPos = new Vector3Int(x, y, 0);
                    var tileWorldPos = grid.GetCellCenterWorld(gridPos);
                    var tile = Instantiate(tilePrefab, tileWorldPos, Quaternion.identity, transform);

                    tile.gameObject.name = $"{x}, {y}";
                    tile.gridPos = gridPos;
                    tiles[index] = tile;
                    index++;
                }
            }
        }

        void SetCam()
        {
            Bounds bounds = new Bounds();
            Bounds boundsBeforeBuffer = new Bounds();
            foreach (Tile tile in tiles)
            {
                bounds.Encapsulate(tile.spriteRenderer.bounds);
            }

            boundsBeforeBuffer = bounds;
            bounds.Expand(new Vector3(buffer.x, buffer.y, 0));
            
            var gridTopRightPos = grid.CellToWorld(new Vector3Int(width, height));
            var camPos = gridTopRightPos / 2;
            
            onGridCreated.Invoke(new GridData(camPos, bounds, boundsBeforeBuffer));
        }

        void SetNeighbors()
        {
            foreach (Tile tile in tiles)
            {
                Vector3Int left = tile.gridPos + new Vector3Int(-1, 0, 0);
                Vector3Int right = tile.gridPos + new Vector3Int(1, 0, 0);
                Vector3Int up = tile.gridPos + new Vector3Int(0, 1, 0);
                Vector3Int down = tile.gridPos + new Vector3Int(0, -1, 0);

                List<Vector3Int> cells = new List<Vector3Int>
                {
                    left,
                    right,
                    up,
                    down
                };

                foreach (Vector3Int cell in cells)
                {
                    if(DoesCellHaveTile(cell))
                        tile.neighbors.Add(GetTileInTheCell(cell));
                }
            }
        }
        
        void OnTouchStart()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var cellUnderCursor = grid.WorldToCell(ray.origin);
            if (DoesCellHaveTile(cellUnderCursor))
            {
                foreach (Tile neighbor in GetTileInTheCell(cellUnderCursor).neighbors)
                {
                    neighbor.spriteRenderer.color = Color.blue;
                }
            }
        }

        bool DoesCellHaveTile(Vector3Int gridPos)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.gridPos.x == gridPos.x && tile.gridPos.y == gridPos.y)
                {
                    return true;
                }
            }

            return false;
        }

        Tile GetTileInTheCell(Vector3Int gridPos)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.gridPos.x == gridPos.x && tile.gridPos.y == gridPos.y)
                    return tile;
            }

            throw new NotImplementedException();
        }

        void SpawnItems()
        {
            do
            {
                for (int i = tiles.Length - 1; i >= 0; i--)
                {
                    if(tiles[i].item != null)
                        Destroy(tiles[i].item.gameObject);
                }
                foreach (Tile tile in tiles)
                {
                    var i = Instantiate(GetRandom(), tile.transform.position, Quaternion.identity, tile.transform);
                    tile.item = i;
                }    
            } while (HasMatch());
            
        }

        bool HasMatch()
        {
            foreach (Tile tile in tiles)
            {
                if (GetMatchCount(tile).horizontalMatchCount >= 3 || GetMatchCount(tile).verticalMatchCount   >= 3)
                    return true;
            }
            return false;
        }

        [ContextMenu("mark tiles")]
        void MarkTiles()
        {
            foreach (Tile tile in tiles)
            {
                if (GetMatchCount(tile).horizontalMatchCount >= 3 || GetMatchCount(tile).verticalMatchCount   >= 3)
                    tile.spriteRenderer.color = Color.blue;
            }
        }

        MatchSearchResult GetMatchCount(Tile tile)
        {
            var gridPos = tile.gridPos;
            MatchSearchResult result = new MatchSearchResult();
            
            
            List<Vector3Int> directions = new List<Vector3Int>
            {
                new Vector3Int(1, 0, 0), // horizontal
                new Vector3Int(0, 1, 0), // vertical
            };

            for (int i = 0; i < directions.Count; i++)
            {
                var currentCellGridPos = gridPos + directions[i];
                int matchCount = 1;
            
                while (DoesCellHaveTile(currentCellGridPos))
                {
                    var t = GetTileInTheCell(currentCellGridPos);
                    if (t.item.myType == tile.item.myType)
                        matchCount++;
                    else
                        break;
                    currentCellGridPos += directions[i];
                }

                if (i == 0)
                {
                    result.horizontalMatchCount = matchCount;
                }
                else
                {
                    result.verticalMatchCount = matchCount;
                }
            }

            return result;
        }

        Item GetRandom()
        {
            var rnd = Random.Range(0, items.Count);
            return items[rnd];
        }
    }

    public struct MatchSearchResult
    {
        public MatchSearchResult(int h = 0, int v = 0)
        {
            horizontalMatchCount = 0;
            verticalMatchCount = 0;
        }
        public int horizontalMatchCount;
        public int verticalMatchCount;
        // public int totalMatchCount => horizontalMatchCount + verticalMatchCount;

    }
}
