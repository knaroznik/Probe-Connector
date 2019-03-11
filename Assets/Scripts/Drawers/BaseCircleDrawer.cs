using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCircleDrawer
{
    protected List<GameObject> lineRenderers;
    protected GameObject linePrefab;
    protected Circle endCircle;

    protected float lineWidth;

    public BaseCircleDrawer(GameObject _linePrefab, Circle _endCircle, float _lineWidth)
    {
        linePrefab = _linePrefab;
        lineWidth = _lineWidth;
        endCircle = _endCircle;
        lineRenderers = new List<GameObject>();
    }

    public void Draw()
    {
        Hide();

        GameObject line = MonoBehaviour.Instantiate(linePrefab);
        line.GetComponent<LineRendererBehaviour>().Initialize(lineWidth, Color.green, null);
        lineRenderers.Add(line);
        List<Vector2> points = new List<Vector2>();

        for (float interval = 0; interval <= 360; interval += 1)
        {

            points.Add(endCircle.Vector2FromAngle(interval));
        }

        LineRenderer l = line.GetComponent<LineRenderer>();
        l.positionCount = points.Count;
        for (int j = 0; j < points.Count; j++)
        {
            l.SetPosition(j, points[j]);
        }
    }

    public void Hide()
    {
        for (int i = 0; i < lineRenderers.Count; i++)
        {
            MonoBehaviour.Destroy(lineRenderers[i]);
        }
        lineRenderers.Clear();
    }
}
