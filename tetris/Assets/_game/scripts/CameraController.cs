using UnityEngine;

namespace Tetris
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] SpriteRenderer innerFrame;
        [SerializeField] Transform outerFrame;

        public void SetPositionAndOrthographicSize(Vector3 gridPos, Vector3 gridLocalCenter, Bounds bounds, Bounds boundsBeforeBuffer)
        {
            Debug.Log(bounds);
            var vertical = bounds.size.y;
            var horizontal = bounds.size.x * ((float)cam.pixelHeight / cam.pixelWidth);
            var size = Mathf.Max(horizontal, vertical) * .5f;
            cam.orthographicSize = size;

            // cam.transform.position = new Vector3(gridPos.x, gridPos.y, -10);
            cam.transform.position  = new Vector3( bounds.center.x,  bounds.center.y, -10);
            
            innerFrame.size = new Vector2(boundsBeforeBuffer.extents.x  * 2, boundsBeforeBuffer.extents.y  * 2);
            outerFrame.localScale = new Vector2(boundsBeforeBuffer.extents.x  * 4, boundsBeforeBuffer.extents.y  * 4);
        }
    }
}