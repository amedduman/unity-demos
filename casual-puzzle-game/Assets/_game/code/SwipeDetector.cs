using UnityEngine;

namespace CasualPuzzle
{
    public class SwipeDetector : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler;
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
            if     (GetAngle(Vector2.right) < degree  && GetAngle(Vector2.right) > 0) Debug.Log("right ");
            else if(GetAngle(Vector2.left)  < degree  && GetAngle(Vector2.left)  > 0) Debug.Log("left ");
            else if(GetAngle(Vector2.up)    < degree  && GetAngle(Vector2.up)    > 0) Debug.Log("up ");
            else if(GetAngle(Vector2.down)  < degree  && GetAngle(Vector2.down)  > 0) Debug.Log("down ");
            
            // Vector2 toEnd = touchEndPos - touchStarPos;
            // Vector2 toRight = (touchStarPos + new Vector2(1, 0)) - touchStarPos;
            // toEnd.Normalize();
            // toRight.Normalize();
            // float angle = Vector2.Angle(toEnd, toRight);
            // Vector3 touchStartWorld = Camera.main.ScreenToWorldPoint(touchStarPos);
            // Debug.DrawLine(touchStartWorld, touchStartWorld + new Vector3(1,0,0), Color.red, 100);
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
