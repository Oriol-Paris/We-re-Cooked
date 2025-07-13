using System;
using System.Collections;
using System.Net.Mail;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Customer : MonoBehaviour
{
    public Receipe requestedReceipe;
    public float waitTime = 30;
    public float maxWaitTime = 30;
    public bool isAngry, isReady, plateServed;

    public float scoreTime;

    public Image patienceBar;
    public Animator client;
    public AudioSource audio;

    void Awake()
    {
        this.waitTime = 30;
        this.maxWaitTime = 30;
        this.isAngry = false;
        this.plateServed = false;

        client.SetTrigger("NewClient");
        audio.Play();
        patienceBar.fillAmount = 0;
        StartCoroutine(WaitToBeReady());
    }

    private void FixedUpdate()
    {
        if (!plateServed)
        {
            if (isReady)
                waitTime -= Time.deltaTime;

            UpdateBar();

            if (waitTime <= 0)
            {
                waitTime = 0;
                Gone();
            }
        }
        else
        {
            CustomerManager.instance.AddRecipeDone();
            StartCoroutine(WaitForScore());
        }
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    IEnumerator WaitToBeReady()
    {
        yield return new WaitForSeconds(1f);
        isReady = true;
        patienceBar.gameObject.transform.parent.gameObject.SetActive(true);
    }

    IEnumerator WaitForScore()
    {
        yield return new WaitForSeconds(scoreTime);
        Gone();
    }


    public void Gone()
    {
        client.SetTrigger("ClientLeft");
        StartCoroutine(WaitForDestroy());
    }

    void UpdateBar()
    {
        float ratio = Mathf.Clamp01(waitTime / maxWaitTime);
        patienceBar.fillAmount = Mathf.Lerp(0.1f, 1, 1 - ratio);
    }

    public void SetRecipie(Receipe r)
    {
        this.requestedReceipe = r;
    }
}