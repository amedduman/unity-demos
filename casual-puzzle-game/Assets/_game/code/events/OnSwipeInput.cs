using UnityEngine;

namespace CasualPuzzle
{
    [CreateAssetMenu(menuName = "_game/events/OnSwipeInput")]
    public class OnSwipeInput : EventSO<SwipeData>
    {
        
    }

    public enum SwipeE
    {
        none = 0,
        right = 10,
        left = 20,
        up = 30,
        down = 40,
    }

    public struct SwipeData
    {
        public SwipeE swipe;
        public SwipeData(SwipeE swipeType)
        {
            swipe = swipeType;
        }
    }
}