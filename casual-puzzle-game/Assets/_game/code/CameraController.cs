using UnityEngine;

namespace CasualPuzzle
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] OnGridCreated onGridCreated;
        [SerializeField] Camera cam;

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
        }
    }
}
