﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle
{

    private float x_Position;
    private float y_Position;
    private float circleRadius;

    public Circle(float _x_Position, float _y_Position, float _radius)
    {
        x_Position = _x_Position;
        y_Position = _y_Position;
        circleRadius = _radius;
    }

    public void ChangeRadius(float _newRadiusValue)
    {
        circleRadius = _newRadiusValue;
    }

    public void DecreaseRadius(float _changeValue)
    {
        circleRadius -= _changeValue;
    }

    public float CircleRadius()
    {
        return circleRadius;
    }

    public Vector2 GetRandomPoint()
    {
        float randomAngle = Random.value * 360;

        float X = (float)(x_Position + (circleRadius * Mathf.Cos(randomAngle)));
        float Y = (float)(y_Position + (circleRadius * Mathf.Sin(randomAngle)));

        return new Vector2(X, Y);
    }

    public bool Contains(float _xValue, float _yValue)
    {
        return ((Mathf.Pow(_xValue - x_Position, 2) + Mathf.Pow(_yValue - y_Position, 2)) < circleRadius * circleRadius);
    }

    public Vector2 GetPointClosestToVector2(Vector2 _value)
    {
        if (Contains(_value.x, _value.y))
        {
            return _value;
        }

        float vX = _value.x - x_Position;
        float vY = _value.y - y_Position;
        float magV = Mathf.Sqrt(vX * vX + vY * vY);
        return new Vector2(x_Position + vX / magV * circleRadius, y_Position + vY / magV * circleRadius);
    }

    public List<Vector2> GetPointsBetween(Vector2 x, Vector2 y)
    {
        float angleX = Angle(x.x, x.y);
        float angleY = Angle(y.x, y.y);

        return GetPointsBetweenAngles(angleX, angleY);
    }

    /// <summary>
    /// Function calculates points between two angles on Circle.
    /// </summary>
    /// <param name="angleX"></param>
    /// <param name="angleY"></param>
    /// <param name="sortingMethod"> Sorting method is responsible for choosing how to handle situation when angleX > angleY. Default value is true.
    ///     If is true the values are simply replaced angleX = angleY and angleY = angleX. 
    ///     Additionaly in this method program checks if another way between points is not shorter. For example (0->270) is changing into (270 -> 360).
    ///     If is false to lower value is added 360 degrees (full circle).
    /// </param>
    /// <returns>Points between two angle. Interval is one degree.</returns>
    public List<Vector2> GetPointsBetweenAngles(float angleX, float angleY, bool sortingMethod = true)
    {
        List<Vector2> output = new List<Vector2>();

        if (sortingMethod)
        {
            if (angleX > angleY)
            {
                float temp = angleX;
                angleX = angleY;
                angleY = temp;
            }

            CheckAnotherWay(ref angleX, ref angleY);
        }else{
            if (angleX > angleY)
            {
                angleY += 360;
            }
        }

        for (float interval = angleX; interval < angleY; interval += 1)
        {

            output.Add(Vector2FromAngle(interval));
        }
        return output;
    }

    private void CheckAnotherWay(ref float angleX, ref float angleY)
    {
        float angleXChanged = angleX + 360;
        if(Mathf.Abs(angleY - angleX) > Mathf.Abs(angleXChanged - angleY))
        {
            float temp = angleY;
            angleY = angleXChanged;
            angleX = temp;
        }
    }

    public float Angle(float x, float y)
    {
        float angle = (Mathf.Rad2Deg * Mathf.Atan2(y_Position - y, x - x_Position));
        if(angle < 0)
        {
            angle += 360;
        }
        return angle <= 360 ? angle : angle - 360;
    }

    public Vector2 Vector2FromAngle(float angle)
    {
        float xPos = circleRadius * Mathf.Sin(Mathf.Deg2Rad * (angle + 90)) + x_Position;
        float yPos = circleRadius * Mathf.Cos(Mathf.Deg2Rad * (angle + 90)) + y_Position;
        return new Vector2(xPos, yPos);
    }
}
