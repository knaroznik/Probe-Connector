﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleDrawer : BaseCircleDrawer
{
    private float maxAngleDifference;

    public CircleDrawer(GameObject _linePrefab, Circle _endCircle, float _maxAngleDifference, float _lineWidth, Material _lineMaterial) : base(_linePrefab, _endCircle, _lineWidth, _lineMaterial)
    {
        maxAngleDifference = _maxAngleDifference;
    }

    public bool DrawArch(float lowValue, float highValue, bool sortingMethod)
    {
        if (!sortingMethod)
        {
            highValue += 360;
        }

        if (Mathf.Abs(highValue - lowValue) > maxAngleDifference)
        {
            return false;
        }

        GameObject line = MonoBehaviour.Instantiate(linePrefab);
        line.GetComponent<LineRendererBehaviour>().Initialize(lineWidth, Color.green, null);
        lineRenderers.Add(line);
        List<Vector2>  points = endCircle.GetPointsBetweenAngles(lowValue, highValue, sortingMethod);
        LineRenderer l = line.GetComponent<LineRenderer>();
        l.positionCount = points.Count;
        for (int j = 0; j < points.Count; j++)
        {
            l.SetPosition(j, points[j]);
        }
        return true;
    }

    //Delete this links that are not correct
    public bool DrawProbe(Probe lowProbe, Probe highProbe, bool sortingMethod)
    {
        float lowValue = lowProbe.Angle;
        float highValue = highProbe.Angle;
        if (!sortingMethod)
        {
            highValue += 360;
        }

        if (Mathf.Abs(highValue - lowValue) > maxAngleDifference)
        {
            return false;
        }

        if (lowProbe.GetBorderer(1) == highProbe)
        {
            return true;
        }

        DrawArch(lowProbe.Angle, highProbe.Angle, sortingMethod);
        return true;
    }
}
