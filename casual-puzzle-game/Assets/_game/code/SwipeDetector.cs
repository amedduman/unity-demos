using UnityEngine;

namespace CasualPuzzle
{
    public class SwipeDetector : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] OnSwipeInput onSwipeInput;
        [SerializeField] float deadZoneRange = 50;
        Vector2 touchStarPos;
        Vector2 touchEndPos;

        void OnEnable()
        {
            inputHandler.TouchStart += OnTouchStart;
            inputHandler.TouchEnd += OnTouchEnd;
        }

        void OnDisable()
        {
            inputHandler.TouchStart -= OnTouchStart;
            inputHandler.TouchEnd -= OnTouchEnd;
        }
        
        void OnTouchStart()
        {
            touchStarPos = inputHandler.mousePos;
        }

        void OnTouchEnd()
        {
            touchEndPos = inputHandler.mousePos;

            float distance = Vector2.Distance(touchStarPos, touchEndPos);
            if(distance < deadZoneRange) return;

            float degree = 30;
            if     (GetAngle(Vector2.right) < degree  && GetAngle(Vector2.right) > 0) onSwipeInput.Invoke(new SwipeData(SwipeE.right, touchStarPos));
            else if(GetAngle(Vector2.left)  < degree  && GetAngle(Vector2.left)  > 0) onSwipeInput.Invoke(new SwipeData(SwipeE.left, touchStarPos));
            else if(GetAngle(Vector2.up)    < degree  && GetAngle(Vector2.up)    > 0) onSwipeInput.Invoke(new SwipeData(SwipeE.up, touchStarPos));
            else if(GetAngle(Vector2.down)  < degree  && GetAngle(Vector2.down)  > 0) onSwipeInput.Invoke(new SwipeData(SwipeE.down, touchStarPos));
        }

        float GetAngle(Vector2 dir)
        {
            Vector2 vecToEndPos = touchEndPos - touchStarPos;
            Vector2 vecToDir = (touchStarPos + dir) - touchStarPos;
            vecToEndPos.Normalize();
            vecToDir.Normalize();

            return Vector2.Angle(vecToEndPos, vecToDir);
        }
    }
}
