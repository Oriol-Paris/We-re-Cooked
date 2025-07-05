using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class Timer : MonoBehaviour
{
    [SerializeField] private Vector2Int maxTime = new Vector2Int(1, 0);
    [SerializeField] private float min, sec;

    private bool stopTime;
    void Start()
    {
        min = maxTime.x;
        sec = maxTime.y;
    }

    void Update()
    {
        if (!stopTime)
            sec -= Time.deltaTime;

        if (min <= 0 && sec <= 0)
            EndTime();

        if (sec <= 0)
            EndMinute();
    }

    void EndTime()
    {
        stopTime = true;
        min = 0;
        sec = 0;
        //Change Scene
    }

    void EndMinute()
    {
        if (min > 0)
            min--;
        sec = 60;
    }

    public void SetMaxTime(int min, int sec) { maxTime = new Vector2Int(min, sec); }

    public void SetCurrentTime(int m, int s)
    {
        min = m;
        sec = s;
    }

    public void StopTime(bool stop) { stopTime = stop; }

    public (int, int) GetTime() { return ((int)min, (int)sec); }
    public (int, int) GetMaxTime() { return (maxTime.x, maxTime.y); }
}
