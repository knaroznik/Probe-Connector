using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDrawer
{
    private GameObject linePrefab;
    private Circle endCircle;
    private float radius;
    private float maxAngleDifference;

    private List<GameObject> lineRenderers;

    public CircleDrawer(GameObject _linePrefab, Circle _endCircle, float _maxAngleDifference)
    {
        linePrefab = _linePrefab;
        endCircle = _endCircle;
        maxAngleDifference = _maxAngleDifference;

        lineRenderers = new List<GameObject>();
    }

    public void Draw(List<float> _angles)
    {
        if (_angles.Count < 2)
            return;

        for(int i=0; i<lineRenderers.Count; i++)
        {
            MonoBehaviour.Destroy(lineRenderers[i]);
        }
        lineRenderers.Clear();

        _angles.Sort();

        for(int i=1; i<_angles.Count; i++)
        {
            DrawArch(_angles[i - 1], _angles[i], true);
        }

        //Czasem nakłada się - chyba FIXED.
        DrawArch(_angles[_angles.Count - 1], _angles[0], false);

    }

    public void DrawArch(float lowValue, float highValue, bool sortingMethod)
    {
        if (!sortingMethod)
        {
            highValue += 360;
        }

        if (Mathf.Abs(highValue - lowValue) > maxAngleDifference)
            return;


        GameObject line = MonoBehaviour.Instantiate(linePrefab);
        lineRenderers.Add(line);
        List<Vector2>  points = endCircle.GetPointsBetweenAngles(lowValue, highValue, sortingMethod);
        LineRenderer l = line.GetComponent<LineRenderer>();
        l.positionCount = points.Count;
        for (int j = 0; j < points.Count; j++)
        {
            l.SetPosition(j, points[j]);
        }
    }
}
