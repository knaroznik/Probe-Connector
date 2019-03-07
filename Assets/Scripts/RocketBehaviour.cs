using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{

    private Circle endCircle;
    private bool moving = false;
    private float speed = 7.32f;
    public Vector2 circlePosition;

    public void Init(Circle c)
    {
        endCircle = c;
    }

    public void ChangeMovement(bool _value)
    {
        moving = _value;
    }

    public bool IsMoving()
    {
        return moving;
    }

    private void Update()
    {
        if (moving)
        {

            if (HandleStop())
            {
                circlePosition = endCircle.GetPointClosestToVector2(new Vector2(this.transform.position.x, this.transform.position.y));
                moving = false;
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                return;
            }
            this.transform.SetParent(null);
            this.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
        }
    }

    private bool HandleStop()
    {
        return !endCircle.Contains(this.transform.position.x, this.transform.position.y);
    }
}
