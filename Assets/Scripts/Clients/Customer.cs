using System;
using UnityEngine;

[Serializable]
public class Customer : MonoBehaviour
{
    public Receipe requestedReceipe;
    public float waitTime;
    public float maxWaitTime;
    public bool isAngry;

    public Customer(Receipe receipe, float waitTime)
    {
        this.requestedReceipe = receipe;
        this.waitTime = waitTime;
        this.maxWaitTime = waitTime;
        this.isAngry = false;
    }

    public void UpdateWait(float deltaTime)
    {
        waitTime -= deltaTime;
        if (waitTime <= 0)
        {
            isAngry = true;
            waitTime = 0;
        }
    }

    public float GetWaitPercentage()
    {
        return Mathf.Clamp01(waitTime / maxWaitTime);
    }
}
