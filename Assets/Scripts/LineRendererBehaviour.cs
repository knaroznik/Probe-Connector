using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererBehaviour : MonoBehaviour
{
    public void Initialize(float _width, Color _color, Material _material)
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.startWidth = _width;
        lr.endWidth = _width;

        lr.startColor = _color;
        lr.endColor = _color;

        if (_material != null)
        {
            lr.material = _material;
        }
    }
}
