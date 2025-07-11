using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class IngredientTypeColorTransitionOutline : MonoBehaviour {
    public IngredientContainer ingredientC;

    public List<Color> colorL;

    public float colorTransitionSpeed = 15;
    [SerializeField]
    private Color currCol;
    [SerializeField]
    private int currColIndex = 0;

    public Outline outline;


    void Start() {
        StartCoroutine(SetColors());
    }

    void FixedUpdate() {
        if (colorL.Count <= 1) return;

        currCol = Color.Lerp(currCol, colorL[currColIndex], colorTransitionSpeed * Time.deltaTime);

        if (currCol == colorL[currColIndex]) {
            currColIndex = (currColIndex + 1) % colorL.Count;
        }

        outline.OutlineColor = currCol;

    }

    IEnumerator SetColors() {
        yield return new WaitForSeconds(0.1f);

        colorL = new List<Color>();
        for (int i = 0; i < ingredientC.ingredientL.Count; i++) {
            for (int j = 0; j < ingredientC.ingredientL[i].ingredientTypes.Count; j++)
                colorL.Add(IngredientManager.instance.GetTypeColorByIndex((int)ingredientC.ingredientL[i].ingredientTypes[j]));
        }

        outline.OutlineColor = colorL[0];
    }

}
