using System;
using UnityEngine;

namespace WordGame
{
    public class LevelGenManager : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] Grid grid;
        [SerializeField] Camera cam;

        void OnEnable()
        {
            inputHandler.OnTap += HandleOnTap;
            Game.onWordCreationPanelEnterButtonPressed.AddListener(WriteWordToGrid);
        }

        void OnDisable()
        {
            inputHandler.OnTap -= HandleOnTap;
            Game.onWordCreationPanelEnterButtonPressed.RemoveListener(WriteWordToGrid);
        }

        void HandleOnTap()
        {
            var worldPos = cam.ScreenToWorldPoint(new Vector3(inputHandler.mousePos.x, inputHandler.mousePos.y, 0));
            var cellUnderCursor = grid.WorldToCell(worldPos);

            if (TryGetTileInTheCell(out Tile tile, cellUnderCursor))
            {
                tile.Select();
                inputHandler.Disable();
            }
        }
        
        void WriteWordToGrid()
        {
            var cell = Game.wordInputData.tile.cellPos;
            Vector3Int dir = Vector3Int.zero;
            switch (Game.wordInputData.dir)
            {
                case WordCreationDirectionE.none:
                    Debug.LogError("direction shouldn't be none");
                    break;
                case WordCreationDirectionE.right:
                    dir = Vector3Int.right;
                    break;
                case WordCreationDirectionE.down:
                    dir = Vector3Int.down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        
            for (int i = 0; i < Game.wordInputData.word.Length; i++)
            {
                if (TryGetTileInTheCell(out Tile tile, cell))
                {
                    tile.SetLetter(Game.wordInputData.word[i]);
                }
                else
                {
                    break;
                }
        
                cell += dir;
            }
        }
        
        bool TryGetTileInTheCell(out Tile tile, Vector3Int cell)
        {
            foreach (Tile t in Game.gridData.tiles)
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