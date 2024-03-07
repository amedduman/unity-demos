using System;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    [SerializeField] float x1;
    [SerializeField] float x2;

    Vector3 pointA;
    Vector3 pointB;

    void OnDrawGizmos()
    {
        MarkCenter();

        float y1 = GetY(x1);
        float y2 = GetY(x2);

        pointA = new Vector3(x1, y1, 0);
        pointB = new Vector3(x2, y2, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointA, 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointB, 1f);
        
        Debug.DrawLine(pointA, pointB);

        var testPoint1 = new Vector3(-2, 1, 0);
        if (IsPointOnLine(testPoint1))
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(testPoint1, 1f);

        var testPoint2 = new Vector3(3, 5, 0);
        if (IsPointOnLine(testPoint2))
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(testPoint2, 1f);
    }

    bool IsPointOnLine(Vector3 point)
    {
        return IsApproximatelyEqual(point.y, GetY(point.x));
    }

    float GetY(float x)
    {
        return 0.6666667F * x + 3;
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
