using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public Timer timer;

    private void Update()
    {
        int min, sec;
        (min,sec) = timer.GetTime();
        txt.text = min + ":" + sec;
    }
}
