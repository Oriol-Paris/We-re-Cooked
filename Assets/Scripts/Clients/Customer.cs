using System;
using UnityEngine;

[Serializable]
public class Customer : MonoBehaviour
{
    public Receipe requestedReceipe;
    public float waitTime = 30;
    public float maxWaitTime = 30;
    public bool isAngry;

    void Awake()
    {
        this.requestedReceipe = ReceipeGenerator.instance.GenRandomReceipe(4, 3);
        this.waitTime = 30;
        this.maxWaitTime = 30;
        this.isAngry = false;

        if (CustomerManager.instance != null)
        {
            CustomerManager.instance.RegisterCustomer(this);
        }
        else
        {
            Debug.Log("No se donde esta el manager");
        }
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
