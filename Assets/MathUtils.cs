using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils 
{
    public struct AbcDelta
    {
        public float A;
        public float B;
        public float C;
    }
    
    public static void StraightEquation(Vector3 start, Vector3 end, out float m, out float c)
    {   // y = m * x + c 
        var catetoOposto = end.y - start.y;
        var catetoAdjacente = end.x - start.x;
        m = catetoOposto / catetoAdjacente;
        c = -m * start.x + start.y;
    }

    public static float Delta(float a, float b, float c, out float x1, out float x2)
    {
        var delta = b * b - 4 * a * c;

        var sqrtDelta = Mathf.Sqrt(delta);
        x1 = (-b + sqrtDelta) / (2 * a);
        x2 = (-b - sqrtDelta) / (2 * a);

        return delta;
    }

    public static void ValuesAbcToDelta(float c, float m, Vector3 circle, float r, out AbcDelta abcDelta)
    {
        var k = c - circle.y;
        abcDelta = new AbcDelta()
        {
            A = m * m + 1,
            B = 2 * m * k - 2 * circle.x,
            C = k * k - r * r + circle.x * circle.x,
        };
    }

    public static bool IsPointInFiniteLine(Vector3 start, Vector3 end, Vector3 point)
    {
        var t = InverseLearp(start, end, point);
        return t >= 0 && t <= 1;
    }

    private static float InverseLearp(Vector3 a, Vector3 b, Vector3 point)
    {
        return (point.x - a.x) / (b.x - a.x);
    }
}
