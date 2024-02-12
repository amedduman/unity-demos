using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/data/swiped cell data")]
    public class SwipedCellData : ScriptableObject
    {
        public Vector3Int cellPos { get; private set; }

        public void SetCellPos(CellSelectionHandler cellSelectionHandler, Vector3Int currentCellPos)
        {
            cellPos = currentCellPos;
        }
    }
}