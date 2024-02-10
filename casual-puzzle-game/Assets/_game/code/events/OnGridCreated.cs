using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/OnGridCreated")]
    public class OnGridCreated : EventSO<GridData>
    {
        
    }

    public struct GridData
    {
        public Vector3 center;
        public Bounds bounds;
        
        public GridData(Vector3 center, Bounds bounds)
        {
            this.center = center;
            this.bounds = bounds;
        }
    }
}


