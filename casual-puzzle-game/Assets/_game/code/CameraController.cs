using UnityEngine;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] Camera cam;
        [SerializeField] SpriteRenderer frame;
        [SerializeField] Transform outerFrame;

        void OnEnable()
        {
            onGridCreated.AddListener(SetPositionAndOrthographicSize);
        }
        
        void OnDisable()
        {
            onGridCreated.RemoveListener(SetPositionAndOrthographicSize);
        }

        void SetPositionAndOrthographicSize(GridData gridData)
        {
            var vertical = gridData.bounds.size.y;
            var horizontal = gridData.bounds.size.x * ((float)cam.pixelHeight / cam.pixelWidth);
            var size = Mathf.Max(horizontal, vertical) * .5f;
            cam.transform.position = new Vector3(gridData.center.x, gridData.center.y, -10);
            cam.orthographicSize = size;
            
            frame.size = new Vector2(gridData.boundsBeforeBuffer.extents.x  * 2, gridData.boundsBeforeBuffer.extents.y  * 2);
            outerFrame.localScale = new Vector2(gridData.boundsBeforeBuffer.extents.x  * 4, gridData.boundsBeforeBuffer.extents.y  * 4);
        }
    }
}
