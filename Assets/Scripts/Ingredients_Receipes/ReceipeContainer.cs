using TMPro;
using UnityEngine;


public class ReceipeContainer : MonoBehaviour {
    public Receipe currentR;

    public int maxTypes = 1;
    public int maxColors = 1;

    public GameObject ingredientPart;

    public Transform listAxis;


    void Start() {
        /*ResetReceipe();
        RefreshHudList();*/
    }

    public void ResetReceipe() {
        currentR = ReceipeGenerator.instance.GenRandomReceipe(maxTypes, maxColors);
        RefreshHudList();
    }

    public void RefreshHudList() {
        for (int i = listAxis.childCount - 1; i >= 0; i--) {
            Destroy(listAxis.GetChild(i).gameObject);
        }

        for (int i = 0; i < currentR.ingredientTypes.Count; i++) {
            var tmp = Instantiate(ingredientPart);
            tmp.transform.parent = listAxis;
            RectTransform rt = tmp.GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            tmp.transform.localEulerAngles = Vector3.zero;
            tmp.transform.localScale = Vector3.one;
            tmp.GetComponent<TextMeshProUGUI>().text = "Type";
            tmp.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite =
                IngredientManager.instance.GetTypeSpriteByIndex((int)currentR.ingredientTypes[i]);
            tmp.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color =
                IngredientManager.instance.GetTypeColorByIndex((int)currentR.ingredientTypes[i]);
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
        tmpT.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite =
            ReceipeGenerator.instance.GetTreatSpriteByIndex((int)currentR.ingredientTreat);

        tmpT.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color =
            new Color((float)252 / 255, (float)144 / 255, (float)3 / 255);

    }

    public void SetReceipe(Receipe r) {
        currentR = r;
        RefreshHudList();
    }


    void OnTriggerEnter(Collider col) {
        //COMPROBAR RECETA (seguramente en otro script)
    }

}
