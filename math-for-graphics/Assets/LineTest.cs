using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    [SerializeField] Vector2 _PointA;

    [SerializeField] Vector2 _PointB;

    [SerializeField] float x;
    [SerializeField] float y;

    [SerializeField] LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ContextMenu("draw line")]
    public void DrawLine()
    {
        float m = _PointA.y - _PointB.y / (_PointA.x - _PointB.x) + .1f;
        Debug.Log(_PointA.y - _PointB.y);
        Debug.Log(_PointA.x - _PointB.x);
        Debug.Log(m);
                
        float b = _PointB.y - m * _PointB.x;

        line.SetPosition(1, new Vector3(x, m*x + b, 0) );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
