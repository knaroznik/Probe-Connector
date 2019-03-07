using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        RocketBehaviour r = c.GetComponent<RocketBehaviour>();
        if (r != null)
        {
            r.ChangeMovement(false);
            Destroy(c.gameObject);
        }
    }
}
