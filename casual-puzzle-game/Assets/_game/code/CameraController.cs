using UnityEngine;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Camera cam;

        public void SetPositionAndOrthographicSize(Vector3 pos, Bounds bounds)
        {
            var vertical = bounds.size.y;
            var horizontal = bounds.size.x * ((float)cam.pixelHeight / cam.pixelWidth);
            var size = Mathf.Max(horizontal, vertical) * .5f;
            cam.transform.position = pos;
            cam.orthographicSize = size;
        }
    }
}
