using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/data/grid data")]
    public class GridData : ScriptableObject
    {
        [field:SerializeField] public int width { get; private set; } = 5;
        [field:SerializeField] public int height { get; private set; } = 8;
    }
}