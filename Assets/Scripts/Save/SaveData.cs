using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int maxAngleDifference;
    public int levelNumber;

    public SaveData(int _levelNumber, int _maxAngleDifference)
    {
        maxAngleDifference = _maxAngleDifference;
        levelNumber = _levelNumber;
    }
}
