using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTexts : MonoBehaviour
{
    public List<GameObject> tutoTexts;

    public GameObject scoreBar;
    public float clientTxtWaitTime = 1f;

    [SerializeField] bool tutorialDone = false;

    private void Start()
    {
        StartCoroutine(ShowClientText());
    }

    IEnumerator ShowClientText()
    {
        yield return new WaitForSeconds(clientTxtWaitTime);
        tutoTexts[tutoTexts.Count - 1].SetActive(true);
    }
    private void Update()
    {
        tutorialDone = scoreBar.activeInHierarchy;
        if(tutorialDone)
            HideTutorial();
    }

    void HideTutorial()
    {
        this.gameObject.SetActive(false);
    }
}
