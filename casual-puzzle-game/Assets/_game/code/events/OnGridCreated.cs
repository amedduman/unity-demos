using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/OnGridCreated")]
    public class OnGridCreated : EventSO<GridData>
    {
        
    }

    public struct GridData
    {
        public GridData(Vector3 center, Bounds bounds)
        {
            this.center = center;
            this.bounds = bounds;
        }
        
        public Vector3 center;
        public Bounds bounds;
    }
}


