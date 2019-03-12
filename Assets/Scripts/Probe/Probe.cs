using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour
{
    public float Angle;

    private float timeCreated;

    private int connections;
    public int Connections
    {
        get
        {
            return connections;
        }
        set
        {
            connections = value;
        }
    }

    private ProbeDisplay drawer;

    public void Init(float _timeCreated, GameObject linePrefab, float distanceDiffeence, Material _lineMaterial)
    {
        timeCreated = _timeCreated;
        drawer = new ProbeDisplay(this.GetComponent<Rigidbody>(), linePrefab, distanceDiffeence, _lineMaterial);
    }

    public void DisplayProbe()
    {
        drawer.Display(this.transform.position);
        
        if(connections > 1)
        {
            drawer.Hide();
        }
    }
}
