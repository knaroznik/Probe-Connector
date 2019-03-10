using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeDisplay {

    Rigidbody r;
    Vector2 position;
    CircleDrawer c;
    GameObject linePrefab;
    float distanceDifference;

    bool displayed = false;

    public ProbeDisplay(Rigidbody _r, GameObject _linePrefab, float _distanceDifference)
    {
        r = _r;
        linePrefab = _linePrefab;
        distanceDifference = _distanceDifference;
    }

    public void Display(Vector2 _position)
    {
        if (!displayed)
        {
            position = _position;
            r.isKinematic = true;
            if (c == null)
            {
                c = new CircleDrawer(linePrefab, new Circle(position.x, position.y, distanceDifference), 99999);
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
