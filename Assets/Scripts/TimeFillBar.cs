using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeFillBar : MonoBehaviour
{
    public Image img;

    public Timer timer;

    public float min, sec, calculateMaxTime, calculateTime;

    private void Start()
    {
        calculateMaxTime = timer.GetMaxTime().Item1 * 60 + timer.GetMaxTime().Item2;
        StartCoroutine(Fill());
    }

    IEnumerator Fill()
    {
        yield return null;
        (min,sec) = timer.GetTime();

        calculateTime = min * 60 + sec;

        float ratio = Mathf.Clamp01(calculateTime / calculateMaxTime);
        img.fillAmount = Mathf.Lerp(img.fillAmount, 1-ratio, Time.deltaTime * 5f);

        if (min + sec == 0)
        {
            img.fillAmount = 1;
            StopAllCoroutines();
        }
        else
            StartCoroutine(Fill());
    }
}
