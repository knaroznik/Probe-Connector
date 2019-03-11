using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeDisplay {

    private Rigidbody probeRigidbody;
    Vector2 position;
    BaseCircleDrawer c;
    GameObject linePrefab;
    float distanceDifference;

    bool displayed = false;

    public ProbeDisplay(Rigidbody _r, GameObject _linePrefab, float _distanceDifference)
    {
        probeRigidbody = _r;
        linePrefab = _linePrefab;
        distanceDifference = _distanceDifference;
    }

    public void Display(Vector2 _position)
    {
        if (!displayed)
        {
            position = _position;
            probeRigidbody.isKinematic = true;
            if (c == null)
            {
                c = new BaseCircleDrawer(linePrefab, new Circle(position.x, position.y, distanceDifference), 0.1f);
            }
            c.Draw();
            displayed = true;
        }
    }


    public void Hide()
    {
        if (displayed)
        {
            c.Hide();
            displayed = false;
        }
        
    }
}
