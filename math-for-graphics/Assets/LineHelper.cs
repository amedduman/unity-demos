using UnityEngine;

public class LineHelper : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Camera cam;

    static readonly int PointB = Shader.PropertyToID("_PointB");

    void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo))
        {
            mat.SetVector(PointB, hitInfo.textureCoord);
        }
    }
}
