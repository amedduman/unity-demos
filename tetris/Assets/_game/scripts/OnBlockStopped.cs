using UnityEngine;

namespace Tetris
{
    [CreateAssetMenu(menuName = "_game/on block stopped")]
    public class OnBlockStopped : EventBase<OnBlockStopped.Data>
    {
        public struct Data
        {
            
        }
    }
}