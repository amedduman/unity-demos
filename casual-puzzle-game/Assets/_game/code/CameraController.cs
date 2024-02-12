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

        void SetPositionAndOrthographicSize(GridCreatedEventData gridCreatedEventData)
        {
            var vertical = gridCreatedEventData.bounds.size.y;
            var horizontal = gridCreatedEventData.bounds.size.x * ((float)cam.pixelHeight / cam.pixelWidth);
            var size = Mathf.Max(horizontal, vertical) * .5f;
            cam.transform.position = new Vector3(gridCreatedEventData.center.x, gridCreatedEventData.center.y, -10);
            cam.orthographicSize = size;
            
            frame.size = new Vector2(gridCreatedEventData.boundsBeforeBuffer.extents.x  * 2, gridCreatedEventData.boundsBeforeBuffer.extents.y  * 2);
            outerFrame.localScale = new Vector2(gridCreatedEventData.boundsBeforeBuffer.extents.x  * 4, gridCreatedEventData.boundsBeforeBuffer.extents.y  * 4);
        }
    }
}
