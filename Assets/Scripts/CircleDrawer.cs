using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void Draw(List<Probe> _probes)
    {
        if (_probes.Count < 2)
        {
            for (int i = 0; i < _probes.Count; i++)
            {
                _probes[i].DisplayProbe();
            }
            return;
        }
            

        for (int i = 0; i < _probes.Count; i++)
        {
            _probes[i].Connections = 0;
        }

        for (int i=0; i<lineRenderers.Count; i++)
        {
            MonoBehaviour.Destroy(lineRenderers[i]);
        }
        lineRenderers.Clear();

        _probes = _probes.OrderBy(i => i.Angle).ToList();

        for(int i=1; i<_probes.Count; i++)
        {
            if(DrawArch(_probes[i - 1].Angle, _probes[i].Angle, true))
            {
                _probes[i - 1].Connections++;
                _probes[i].Connections++;
            }
        }

        //Czasem nakłada się - chyba FIXED.
        if(DrawArch(_probes[_probes.Count - 1].Angle, _probes[0].Angle, false))
        {
            _probes[_probes.Count - 1].Connections++;
            _probes[0].Connections++;
        }

        for (int i = 0; i < _probes.Count; i++)
        {
            _probes[i].DisplayProbe();
        }

    }

    public void Draw()
    {
        for (int i = 0; i < lineRenderers.Count; i++)
        {
            MonoBehaviour.Destroy(lineRenderers[i]);
        }
        lineRenderers.Clear();

        GameObject line = MonoBehaviour.Instantiate(linePrefab);
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

    public bool DrawArch(float lowValue, float highValue, bool sortingMethod)
    {
        if (!sortingMethod)
        {
            highValue += 360;
        }

        if (Mathf.Abs(highValue - lowValue) > maxAngleDifference)
        {
            //Debug.Log(lowValue + " " + highValue);
            return false;
        }

        

        GameObject line = MonoBehaviour.Instantiate(linePrefab);
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
}
