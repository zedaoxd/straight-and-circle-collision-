using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleChalleger : MonoBehaviour
{
    [Header("Circle")]
    [SerializeField] private Transform positionCircle;
    [SerializeField] private float r;
    [Header("Straight")]
    [SerializeField] private Transform straightTo;
    [SerializeField] private Transform straightFrom;

    private Vector3 CirclePositionCenter => positionCircle.position;
    private Vector3 StartLine => straightFrom.position;
    private Vector3 EndLine => straightTo.position;
    private float _delta;

    private void OnDrawGizmos()
    {
        DrawCircle();
        DrawCrossStitch();
        DrawStraught();
    }

    private void DrawCircle()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(CirclePositionCenter, r);
    }

    private void DrawStraught()
    {
        Gizmos.color = _delta < 0 ? Color.white : Color.blue;
        Gizmos.DrawLine(StartLine, EndLine); 
    }

    private void DrawCrossStitch()
    {
        MathUtils.StraightEquation(StartLine, EndLine, out var m, out var c);
        MathUtils.ValuesAbcToDelta(c, m, CirclePositionCenter, r, out var abcDelta);

        _delta = MathUtils.Delta(abcDelta.A, abcDelta.B, abcDelta.C, out var x1, out var x2);
        if (_delta < 0)
        {
            return;
        }

        var y1 = m * x1 + c;
        var y2 = m * x2 + c;
        
        var point1 = new Vector3(x1, y1, 0);
        var point2 = new Vector3(x2, y2, 0);
        
        Gizmos.color = Color.green;

        var isPoint1Valid = MathUtils.IsPointInFiniteLine(StartLine, EndLine, point1);
        var isPoint2Valid = MathUtils.IsPointInFiniteLine(StartLine, EndLine, point2);

        if (isPoint1Valid)
        {
            Gizmos.DrawSphere(point1, 0.1f);
        }
        
        if (isPoint2Valid)
        {
            Gizmos.DrawSphere(point2, 0.1f);
        }
    }
}
