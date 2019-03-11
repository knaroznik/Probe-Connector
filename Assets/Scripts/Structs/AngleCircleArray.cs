using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AngleCircleArray
{
    private List<Probe> probes;
    private CircleDrawer drawer;

    public AngleCircleArray(CircleDrawer _drawer)
    {
        probes = new List<Probe>();
        drawer = _drawer;
    }

    public void Add(Probe _newValue)
    {
        probes.Add(_newValue);
        probes = probes.OrderBy(i => i.Angle).ToList();
    }

    public float Calculate()
    {
        float circuit = 0;

        if (probes.Count < 2)
        {
            DisplayProbes();
            return circuit;
        }

        for (int i = 0; i < probes.Count; i++)
        {
            probes[i].Connections = 0;
        }

        drawer.Hide();

        for (int i = 1; i < probes.Count; i++)
        {
            if (drawer.DrawArch(probes[i - 1].Angle, probes[i].Angle, true))
            {
                probes[i - 1].Connections++;
                probes[i].Connections++;
                circuit += (Mathf.Abs(probes[i].Angle - probes[i - 1].Angle));
            }
        }

        if (drawer.DrawArch(probes[probes.Count - 1].Angle, probes[0].Angle, false))
        {
            probes[probes.Count - 1].Connections++;
            probes[0].Connections++;
            circuit += Mathf.Abs((probes[0].Angle + 360) - probes[probes.Count - 1].Angle);
        }

        DisplayProbes();

        return circuit;
    }

    private void DisplayProbes()
    {
        for (int i = 0; i < probes.Count; i++)
        {
            probes[i].DisplayProbe();
        }
    }
}
