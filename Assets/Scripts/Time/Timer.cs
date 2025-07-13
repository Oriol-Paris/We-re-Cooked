using System.Collections;
using UnityEditor;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private Vector2Int maxTime = new Vector2Int(1, 0);
    [SerializeField] private float min, sec;

    public bool stopTime;

    public AudioSource audio;
    void Start()
    {
        min = maxTime.x;
        sec = maxTime.y;
        StartCoroutine(StopWatch());
    }

    IEnumerator StopWatch()
    {
        yield return null;
        if (!stopTime)
            sec -= Time.deltaTime;

        if (min == 0 && sec <= 0)
            EndTime();
        else if (sec <= 0)
        {
            sec = 60;
            min -= 1;
        }
        
        StartCoroutine(StopWatch());
    }

    void EndTime()
    {
        stopTime = true;
        min = 0;
        sec = 0;
        StopAllCoroutines();
        audio.Play();
        SavedContent.instance.SaveContents();
    }

    public void SetMaxTime(int min, int sec) { maxTime = new Vector2Int(min, sec); }

    public void SetCurrentTime(int m, int s)
    {
        min = m;
        sec = s;
    }

    public void StopTime(bool stop) { stopTime = stop; }

    public (float, float) GetTime() { return (min, sec); }
    public (int, int) GetMaxTime() { return (maxTime.x, maxTime.y); }
}
