using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    Collider colllider;
    
    void OnDrawGizmos()
    {
        colllider = GetComponent<Collider>();
        if (colllider == null) return;
        if (colllider is BoxCollider)
        {
            colllider = GetComponent<BoxCollider>();
            
            var bounds = colllider.bounds;
            Debug.Log(bounds.extents.x);
            Debug.Log(bounds.size.x);
            Gizmos.DrawCube(transform.position ,  bounds.size / 2);
        }
        else if (colllider is SphereCollider)
        {
            
        }
    }
}
