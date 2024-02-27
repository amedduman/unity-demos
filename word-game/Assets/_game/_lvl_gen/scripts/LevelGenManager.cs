using UnityEngine;

namespace WordGame
{
    public class LevelGenManager : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] GridData gridData;
        [SerializeField] Grid grid;
        [SerializeField] Camera cam;

        void OnEnable()
        {
            inputHandler.OnTap += HandleOnTap;
        }

        void OnDisable()
        {
            inputHandler.OnTap -= HandleOnTap;
        }

        void HandleOnTap()
        {
            var worldPos = cam.ScreenToWorldPoint(new Vector3(inputHandler.mousePos.x, inputHandler.mousePos.y, 0));
            var cellUnderCursor = grid.WorldToCell(worldPos);

            if (TryGetTileInTheCell(out Tile tile, cellUnderCursor))
            {
                tile.spriteRenderer.color = Color.blue;
            }
        }
        
        bool TryGetTileInTheCell(out Tile tile, Vector3Int cell)
        {
            foreach (Tile t in gridData.tiles)
            {
                if (t.cellPos.x == cell.x && t.cellPos.y == cell.y)
                {
                    tile = t;
                    return true;
                }
            }

            tile = null;
            return false;
        }
    }
}