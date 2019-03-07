using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDrawer
{
    private GameObject linePrefab;
    private Circle endCircle;
    private float radius;

    private List<GameObject> lineRenderers;

    public CircleDrawer(GameObject _linePrefab, Circle _endCircle)
    {
        linePrefab = _linePrefab;
        endCircle = _endCircle;

        lineRenderers = new List<GameObject>();
    }

    public void Draw(List<float> _values)
    {
        if (_values.Count < 2)
            return;

        for(int i=0; i<lineRenderers.Count; i++)
        {
            MonoBehaviour.Destroy(lineRenderers[i]);
        }
        lineRenderers.Clear();

        _values.Sort();
        for(int i=1; i<_values.Count; i++)
        {
            GameObject line = MonoBehaviour.Instantiate(linePrefab);
            lineRenderers.Add(line);
            List<Vector2> points = endCircle.GetPointsBetweenAngles(_values[i-1], _values[i]);
            LineRenderer l = line.GetComponent<LineRenderer>();
            l.positionCount = points.Count;
            for (int j = 0; j < points.Count; j++)
            {
                l.SetPosition(j, points[j]);
            }
        }
    }
}
