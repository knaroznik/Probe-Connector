using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDispay : MonoBehaviour
{
    public Text maxProbeText;

    public void MaxProbeTextChanged(string _value)
    {
        maxProbeText.text = "Probes left : " + _value;
    }
}
