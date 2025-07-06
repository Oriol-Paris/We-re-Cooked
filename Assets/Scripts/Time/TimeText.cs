using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public Timer timer;

    float minutes, seconds;
    int min, sec;

    public List<Vector2Int> timeAlert;
    public Animator anim;

    private void Start()
    {
        txt.text = "0:00";
        StartCoroutine(WriteTime());
    }

    IEnumerator WriteTime()
    {
        yield return null;

        (minutes, seconds) = timer.GetTime();
        min = (int)minutes;
        sec = (int)seconds;
        txt.text = sec > 10 ? min + ":" + sec : min + ":0" + sec;

        ActivateAlert();

        if (timer.stopTime)
            StopAllCoroutines();

        StartCoroutine(WriteTime());
    }

    void ActivateAlert()
    {
        foreach (var alert in timeAlert)
        {
            if (alert.x == min && alert.y+1 == sec)
                anim.SetTrigger("TimeAlert");
        }
    }
}
