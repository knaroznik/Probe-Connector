using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitable : MonoBehaviour {

    private Vector3 orbitPosition;
    private Vector3 axis = Vector3.forward;
    private float rotationSpeed = 100f;
    public float Mass;

    public Transform[] moons;

    private Vector3 prevPos;

    public void Initialize(Vector3 _orbitPosition, float _rotationSpeed)
    {
        orbitPosition = _orbitPosition;
        rotationSpeed = _rotationSpeed;
    }

    void Update () {
        prevPos = transform.position;
        transform.RotateAround(orbitPosition, axis, rotationSpeed / Mass * Time.deltaTime);
        for(int i=0; i<moons.Length; i++)
        {
            moons[i].position += transform.position - prevPos;
        }
    }
}
