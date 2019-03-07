using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotateable : MonoBehaviour {

    public Vector3 rotation;
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(rotation);
	}
}
