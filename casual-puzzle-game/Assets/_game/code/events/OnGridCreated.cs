using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/events/OnGridCreated")]
    public class OnGridCreated : EventSO<GridCreatedEventData>
    {
        
    }

    public struct GridCreatedEventData
    {
        public Vector3 center;
        public Bounds bounds;
        public Bounds boundsBeforeBuffer;
        
        public GridCreatedEventData(Vector3 center, Bounds bounds, Bounds boundsBeforeBuffer)
        {
            this.center = center;
            this.bounds = bounds;
            this.boundsBeforeBuffer = boundsBeforeBuffer;
        }
    }
}


