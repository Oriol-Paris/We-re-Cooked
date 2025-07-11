using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class IngredientColorTransition : MonoBehaviour {
    public IngredientContainer ingredientC;

    public List<Color> colorL;

    public float colorTransitionSpeed = 15;
    [SerializeField]
    private Color currCol;
    [SerializeField]
    private int currColIndex = 0;

    public MeshRenderer meshR;


    void Start() {
        StartCoroutine(SetColors());
    }

    void FixedUpdate() {
        if (colorL.Count <= 1) return;

        currCol = Color.Lerp(currCol, colorL[currColIndex], colorTransitionSpeed * Time.deltaTime);

        if (currCol == colorL[currColIndex]) {
            currColIndex = (currColIndex + 1) % colorL.Count;
        }

        meshR.material.color = currCol;

    }

    IEnumerator SetColors() {
        yield return new WaitForSeconds(0.1f);

        colorL = new List<Color>();
        for (int i = 0; i < ingredientC.ingredientL.Count; i++) {
            for (int j = 0; j < ingredientC.ingredientL[i].ingredientColors.Count; j++)
                colorL.Add(IngredientManager.instance.GetColorByIndex((int)ingredientC.ingredientL[i].ingredientColors[j]));
        }

        meshR.material.color = colorL[0];
    }

}
