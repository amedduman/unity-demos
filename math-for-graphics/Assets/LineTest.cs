using System;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    [SerializeField] float xA;
    [SerializeField] float xB;

    void OnDrawGizmos()
    {
        MarkCenter();

        float m = 2;
        float b = 7;

        float n = 0.6666667F;
        float c =  3;
        
        float crossPointX = (c - b) / (m - n);
        float crossPointY = m * crossPointX + b;
        var crossPoint = new Vector3(crossPointX, crossPointY, 0);
        DrawPoint(crossPoint);
        
        DrawLine(xA, xB, m, b);
        DrawLine(xA, xB, n, c);
    }

    void DrawLine(float x1, float x2, float m, float b)
    {
        float y1 = GetY(x1, m, b);
        float y2 = GetY(x2, m, b);
        var pointA = new Vector3(x1, y1, 0);
        var pointB = new Vector3(x2, y2, 0);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointA, 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointB, 1f);
        
        Debug.DrawLine(pointA, pointB);
        Gizmos.color = Color.white;
    }

    void DrawPoint(Vector3 point, float m, float k)
    {
        if (IsPointOnLine(point, m, k))
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(point, 1f);
        Gizmos.color = Color.white;
    }
    
    void DrawPoint(Vector3 point)
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(point, 1f);
        Gizmos.color = Color.white;
    }

    bool IsPointOnLine(Vector3 point, float m, float k)
    {
        return IsApproximatelyEqual(point.y, GetY(point.x, m, k));
    }

    float GetY(float x, float m, float b)
    {
        return m * x + b;
    }

    bool IsApproximatelyEqual(float a, float b, float epsilon = 0.001f)
    {
        if (epsilon <= 0)
            throw new ArgumentOutOfRangeException(nameof(epsilon));

        return Mathf.Abs(a - b) < epsilon;
    }
    
    static void MarkCenter()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Vector3.zero, 1f);
    }
}
