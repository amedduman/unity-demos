using System;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    [SerializeField] float xA;
    [SerializeField] float xB;

    void OnDrawGizmos()
    {
        MarkCenter();

        DrawLine(.6f, 3, Color.green);
        DrawLine(1, 2, Color.red);
    }

    void TryMarkIntersection(float m1, float k1, float m2, float k2)
    {
        float x = (k2 - k1) / (m1 - m2);
        float y = GetY(x, m1, k1);
        
        if(IsApproximatelyEqual(m1 * x + k1, m2 * x + k2, .01f))
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(new Vector3(x,y,0), 1);    
            Gizmos.color = Color.white;
        }
    }

    void DrawLine(float m, float b, Color color)
    {
        float y1 = GetY(xA, m, b);
        float y2 = GetY(xB, m, b);
        var pointA = new Vector3(xA, y1, 0);
        var pointB = new Vector3(xB, y2, 0);
        
        Gizmos.color = color;
        Gizmos.DrawWireSphere(pointA, 1f);
        Gizmos.DrawWireSphere(pointB, 1f);
        
        Debug.DrawLine(pointA, pointB, color);
        Gizmos.color = Color.white;
    }

    void DrawLine(Vector2 a, Vector2 b)
    {
        Debug.DrawLine(a, b);
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
