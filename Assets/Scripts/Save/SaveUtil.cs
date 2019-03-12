using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveUtil
{
    private static readonly string directory = Application.persistentDataPath + "/levelData.save";

    public static bool CreateSave(int _levelNumber, int _maxAngle)
    {
        SaveData data = new SaveData(_levelNumber, _maxAngle);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(directory);
        bf.Serialize(file, data);
        file.Close();
        return true;
    }

    public static SaveData LoadSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(directory, FileMode.Open);
        SaveData save = (SaveData)bf.Deserialize(file);
        file.Close();
        return save;
    }
}
