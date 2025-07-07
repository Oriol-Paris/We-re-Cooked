using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;


public class ReceipeContainer : MonoBehaviour {
    public Receipe currentR;

    public int maxTypes = 1;
    public int maxColors = 1;

    public GameObject ingredientPart;

    public Transform listAxis;


    void Start() {
        ResetReceipe();
        RefreshHudList();
    }

    public void ResetReceipe() {
        currentR = ReceipeGenerator.instance.GenRandomReceipe(maxTypes, maxColors);
    }

    public void RefreshHudList() {
        while (listAxis.childCount > 0) {
            Destroy(listAxis.GetChild(0));
        }

        for (int i = 0; i < currentR.ingredientTypes.Count; i++) {
            var tmp = Instantiate(ingredientPart);
            tmp.transform.parent = listAxis;
            RectTransform rt = tmp.GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            tmp.transform.localEulerAngles = Vector3.zero;
            tmp.transform.localScale = Vector3.one;
            tmp.GetComponent<TextMeshProUGUI>().text = "Type";
            //tmp.transform.GetChild(0).GetComponent<Image>().sprite = ;
        }

        for (int i = 0; i < currentR.ingredientColors.Count; i++) {
            var tmp = Instantiate(ingredientPart);
            tmp.transform.parent = listAxis;
            RectTransform rt = tmp.GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            tmp.transform.localEulerAngles = Vector3.zero;
            tmp.transform.localScale = Vector3.one;
            tmp.GetComponent<TextMeshProUGUI>().text = "Color";
            tmp.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = IngredientManager.instance.GetColorByIndex((int)currentR.ingredientColors[i]);
        }

        var tmpT = Instantiate(ingredientPart);
        tmpT.transform.parent = listAxis;
        RectTransform rtT = tmpT.GetComponent<RectTransform>();
        rtT.localPosition = Vector3.zero;
        tmpT.transform.localEulerAngles = Vector3.zero;
        tmpT.transform.localScale = Vector3.one;
        tmpT.GetComponent<TextMeshProUGUI>().text = "Treatement";
        //tmp.transform.GetChild(0).GetComponent<Image>().sprite = ;

    }

    void OnTriggerEnter(Collider col) {
        //COMPROBAR RECETA (seguramente en otro script)
    }

}
