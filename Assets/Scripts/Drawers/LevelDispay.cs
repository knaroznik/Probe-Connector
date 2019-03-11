using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDispay : MonoBehaviour
{
    public Text maxProbeText;
    public Image endPanel;
    public Text endText;
    public Image[] starSprites;
    public string[] levelMessages;
    public Sprite goldStar;

    public void MaxProbeTextChanged(string _value)
    {
        maxProbeText.text = "Probes left : " + _value;
    }

    public void DisplayResult(LevelDegrees levelScore)
    {
        StartCoroutine(IDisplayResult(levelScore));
    }

    public IEnumerator LerpColor(Color _start, Color _end, Image _image, float _time)
    {
        float i = 0.0f;
        float rate = 1.0f / _time;
        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            _image.color = Color.Lerp(_start, _end, i);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator IDisplayResult(LevelDegrees levelScore)
    {
        endPanel.gameObject.SetActive(true);
        yield return StartCoroutine(LerpColor(Color.clear, Color.black, endPanel, 4f));
        endText.gameObject.SetActive(true);
        endText.text = levelMessages[levelScore.GetLevelDegree()];

        for(int i=0; i<starSprites.Length; i++)
        {
            starSprites[i].gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        int score = levelScore.GetLevelDegree();
        for (int i=0; i<starSprites.Length; i++)
        {
            if(score >= i + 1)
            {
                starSprites[i].sprite = goldStar;
                yield return new WaitForSeconds(1f);
            }
        }
        yield return null;
    }
}
