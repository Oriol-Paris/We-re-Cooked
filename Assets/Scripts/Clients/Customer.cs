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
    public AudioClip[] audioClips; //0 - mad, 1 - appear

    void Awake()
    {
        this.waitTime = 90;
        this.maxWaitTime = 90;
        this.isAngry = false;
        this.plateServed = false;

        client.SetTrigger("NewClient");
        audio.clip = audioClips[1];
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

            if (waitTime <= 0 && !isAngry)
            {
                isAngry = true;
                waitTime = 0;
                audio.Stop();
                audio.clip = audioClips[0];
                audio.Play();
                Gone(4);
            }
        }
        else
        {
            StartCoroutine(WaitForScore());
        }
    }

    IEnumerator WaitForDestroy(float time = 1f)
    {
        yield return new WaitForSeconds(time);
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


    public void Gone(float time = 1f)
    {
        client.SetTrigger("ClientLeft");
        StartCoroutine(WaitForDestroy(time));
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