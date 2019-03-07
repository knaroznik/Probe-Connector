using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuaternionUtil
{
    public static Quaternion GetOppositeDirection(GameObject start, GameObject end)
    {
        return Quaternion.LookRotation(start.transform.position - end.transform.position);
    }
}
