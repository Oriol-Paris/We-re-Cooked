using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Image fillImg;

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
}
