using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CookZone : MonoBehaviour {
    public bool canCook = true;

    public float cookTime = 3;
    public float canCookCD = 0.5f;

    public GameObject cookedResult;
    public Transform cookedResultLaunchPosNDir;
    public float launchSpeed = 5;

    public List<IngredientSerializable> currIngredientL;

    public ReceipeTreatement receipeTreatementResult;

    public AudioSource audio;

    void Start() {
        canCook = true;
    }

    void OnTriggerEnter(Collider col) {
        if (!canCook) return;

        if (col.GetComponent<IngredientContainer>()) {
            IngredientContainer tmp = col.GetComponent<IngredientContainer>();
            if (tmp.receivingTreatement != ReceipeTreatement.NONE || tmp.ingredientL.Count == 0)
                return;

            canCook = false;

            currIngredientL.Clear();
            currIngredientL = tmp.ingredientL;

            Destroy(col.gameObject);
            if (audio != null)
                audio.Play();
            StartCoroutine(CookProcess());
        }

    }

    IEnumerator CookProcess() {

        yield return new WaitForSeconds(cookTime);
        audio.Stop();
        var tmp = Instantiate(cookedResult, cookedResultLaunchPosNDir.position, Quaternion.identity);
        tmp.GetComponent<IngredientContainer>().ingredientL = currIngredientL;
        tmp.GetComponent<IngredientContainer>().receivingTreatement = receipeTreatementResult;
        tmp.GetComponent<Rigidbody>().AddForce(
            cookedResultLaunchPosNDir.forward * launchSpeed,
            ForceMode.Impulse);

        yield return new WaitForSeconds(canCookCD);
        canCook = true;

    }

}
