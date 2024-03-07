using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    [SerializeField] float x1;
    [SerializeField] float x2;

    Vector3 pointA;
    Vector3 pointB;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Vector3.zero, 1f);
        
        float y1 = GetY(x1);
        float y2 = GetY(x2);

        pointA = new Vector3(x1, y1, 0);
        pointB = new Vector3(x2, y2, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointA, 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointB, 1f);
        
        Debug.DrawLine(pointA, pointB);
    }

    float GetY(float x)
    {
        return 0.6666667F * x + 3;
    }

    bool IsApproximatelyEqual(float a, float b, float epsilon = 0.001f)
    {
        return Mathf.Abs(a - b) < epsilon;
    }
}
