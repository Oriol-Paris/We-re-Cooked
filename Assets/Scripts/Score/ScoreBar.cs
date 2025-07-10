using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Image fillImg;

    void Start()
    {
        fillImg.fillAmount = 0;
    }

    public void UpdateBar(float currAmount, float maxAmount)
    {
        float ratio = Mathf.Clamp01(currAmount / maxAmount);
        Debug.Log(ratio);
        fillImg.fillAmount = Mathf.Lerp(0, 1, ratio);
        CalculateColor();
    }

    void CalculateColor()
    {
        fillImg.color = Color.Lerp(Color.red, Color.green, fillImg.fillAmount);
    }
}
