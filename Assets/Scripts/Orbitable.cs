using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitable : MonoBehaviour {

    public Transform OrbitObject;
    private Vector3 axis = Vector3.forward;
    public float rotationSpeed = 100f;
    public float Mass;

    public Transform[] moons;

    private Vector3 prevPos;

	
	// Update is called once per frame
	void Update () {
        prevPos = transform.position;
        transform.RotateAround(OrbitObject.position, axis, rotationSpeed / Mass * Time.deltaTime);
        for(int i=0; i<moons.Length; i++)
        {
            moons[i].position += transform.position - prevPos;
        }
    }
}
