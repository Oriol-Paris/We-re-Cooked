using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Image fillImg;

    public TextMeshProUGUI txt;
    public string[] clientResponses;

    public float fillColorSaturation, fillColorBrightness;
    void Start()
    {
        fillImg.fillAmount = 0.1f;
        SetScoreState(false);
    }

    public void UpdateBar(float currAmount, float maxAmount)
    {
        float ratio = Mathf.Clamp01(currAmount / maxAmount);
        fillImg.fillAmount = Mathf.Lerp(0.1f, 1, ratio);
        ClientResponse(ratio);
        CalculateColor();
    }

    void CalculateColor()
    {
        float hue = Mathf.Lerp(0f, 1f / 3f, fillImg.fillAmount); // 0f = vermell, 1/3 = verd
        Color saturatedColor = Color.HSVToRGB(hue, fillColorSaturation, fillColorBrightness);
        fillImg.color = saturatedColor;

    }

    public void SetScoreState(bool active)
    {
        transform.GetChild(0).gameObject.SetActive(active);
    }

    private void ClientResponse(float ratio)
    {
        if(ratio < 0.5f)
        {
            txt.text = clientResponses[0];
        }else if(ratio <= 0.75f)
        {
            txt.text = clientResponses[1];
        }
        else
        {
            txt.text = clientResponses[2];
        }
    }
}
