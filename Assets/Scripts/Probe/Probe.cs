using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour
{
    public float Angle;

    private Probe[] borderers;
    public bool SetBorderer(int _position, Probe _value)
    {
        if(_position < 2 && _position > -1)
        {
            borderers[_position] = _value;
            return true;
        }
        throw new Exception("Wrong position in Probe setter");
    }

    public Probe GetBorderer(int _position)
    {
        if (_position < 2 && _position > -1)
        {
            return borderers[_position];
        }
        throw new Exception("Wrong position in Probe getter");
    }

    private float timeCreated;
    private ProbeDisplay drawer;

    public void Init(float _timeCreated, GameObject linePrefab, float distanceDiffeence, Material _lineMaterial)
    {
        timeCreated = _timeCreated;
        borderers = new Probe[2];
        drawer = new ProbeDisplay(this.GetComponent<Rigidbody>(), linePrefab, distanceDiffeence, _lineMaterial);
    }

    public void DisplayProbe()
    {
        drawer.Display(this.transform.position);
        
        if(borderers[0] != null && borderers[1] != null)
        {
            drawer.Hide();
        }
    }
}
