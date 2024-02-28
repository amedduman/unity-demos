using System;
using System.Linq;
using UnityEngine;

namespace WordGame
{
    public class LevelGenManager : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] WordCreationAction wordCreationAction;
        [SerializeField] GridData gridData;
        [SerializeField] Grid grid;
        [SerializeField] Camera cam;

        void OnEnable()
        {
            inputHandler.OnTap += HandleOnTap;
            wordCreationAction.AddListener(HandleWordCreation, 0);
        }

        void OnDisable()
        {
            inputHandler.OnTap -= HandleOnTap;
            wordCreationAction.RemoveListener(HandleWordCreation);
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
        
        void HandleWordCreation(WordCreationAction.Data obj)
        {
            var cell = obj.tile.cellPos;
            Vector3Int dir = Vector3Int.zero;
            switch (obj.dir)
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

            for (int i = 0; i < obj.word.Length; i++)
            {
                if (TryGetTileInTheCell(out Tile tile, cell))
                {
                    tile.spriteRenderer.color = Color.blue;
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