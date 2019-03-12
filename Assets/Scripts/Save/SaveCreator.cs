using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveCreator : MonoBehaviour
{
    public int levelNumber;
    public int maxAngleDifference;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(CreateSave);
    }

    private void CreateSave()
    {
        SaveUtil.CreateSave(levelNumber, maxAngleDifference);

        SceneManager.LoadScene(1);
    }
}
