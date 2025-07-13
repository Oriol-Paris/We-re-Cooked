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

    void Start()
    {
        fillImg.fillAmount = 0.1f;
        SetScoreState(false);
    }

    public void UpdateBar(float currAmount, float maxAmount)
    {
        float ratio = Mathf.Clamp01(currAmount / maxAmount);
        Debug.Log(ratio);
        fillImg.fillAmount = Mathf.Lerp(0.1f, 1, ratio);
        ClientResponse(ratio);
        CalculateColor();
    }

    void CalculateColor()
    {
        fillImg.color = Color.Lerp(Color.red, Color.green, fillImg.fillAmount);
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
            txt.text = clientResponses[0];
        }
        else
        {
            txt.text = clientResponses[0];
        }
    }
}
