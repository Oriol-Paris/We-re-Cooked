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
    public bool isAngry, isReady;

    public Image patienceBar;
    public Animator client;

    private Transform clientPos;

    void Awake()
    {
        this.waitTime = 30;
        this.maxWaitTime = 30;
        this.isAngry = false;

        //if (CustomerManager.instance != null)
        //{
        //    CustomerManager.instance.RegisterCustomer(this);
        //}
        //else
        //{
        //    Debug.Log("No se donde esta el manager");
        //}

        client.SetTrigger("NewClient");
        patienceBar.fillAmount = 0;
        clientPos = this.transform;
        StartCoroutine(WaitToBeReady());
    }

    private void FixedUpdate()
    {
        if (isReady)
            waitTime -= Time.deltaTime;

        UpdateBar();

        if (waitTime <= 0)
        {
            //CustomerManager.instance.RenewCustomer(this);
            waitTime = 0;
            client.SetTrigger("ClientLeft");
            StartCoroutine(WaitForDestroy());
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

    public float GetWaitPercentage()
    {
        return Mathf.Clamp01(waitTime / maxWaitTime);
    }

    void UpdateBar()
    {
        patienceBar.fillAmount = Mathf.Lerp(0.1f, 1, 1 - GetWaitPercentage());
    }

    public void SetPos(Transform pos)
    {
        this.clientPos = pos;
    }

    public Transform GetPos() { return this.clientPos; }

    public void SetRecipie(Receipe r)
    {
        this.requestedReceipe = r;
    }
}