using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class IngredientSerializable {
    public List<IngredientType> ingredientTypes;
    public List<IngredientColor> ingredientColors;
}

[Serializable]
public class IngredientMeshes {
    public List<MeshRenderer> meshR;
    public List<Outline> outlines;
}

public class Ingredient : MonoBehaviour {
    public List<IngredientType> ingredientTypes;
    public int maxTypes = 1;

    public List<IngredientColor> ingredientColors;
    public int maxColors = 1;

    public List<IngredientMeshes> meshRendL;

    public float colorTransitionSpeed = 15;
    [SerializeField]
    private Color currTypeCol;
    [SerializeField]
    private int currTypeColIndex = 0;


    void Start() {
        maxColors = meshRendL.Count;

        ingredientTypes = new List<IngredientType>();
        ingredientColors = new List<IngredientColor>();

        for (int i = 0; i < maxTypes; i++) {
            ingredientTypes.Add(IngredientManager.instance.GetRandomType());
        }

        for (int i = 0; i < maxColors; i++) {//randomColors
            ingredientColors.Add(IngredientManager.instance.GetRandomColor());
            for (int j = 0; j < meshRendL[i].meshR.Count; j++) {
                meshRendL[i].meshR[j].material.color =
                    IngredientManager.instance.GetColorByIndex((int)ingredientColors[i]);
            }
            for (int j = 0; j < meshRendL[i].outlines.Count; j++) {
                meshRendL[i].outlines[j].OutlineColor = IngredientManager.instance.GetTypeColorByIndex((int)ingredientTypes[0]);
            }
        }
        currTypeCol = IngredientManager.instance.GetTypeColorByIndex((int)ingredientTypes[0]);

        if (!IngredientManager.instance.currIngredient.Contains(gameObject))
            IngredientManager.instance.currIngredient.Add(gameObject);
    }

    void OnEnable() {
        if (IngredientManager.instance != null)
            if (!IngredientManager.instance.currIngredient.Contains(gameObject))
                IngredientManager.instance.currIngredient.Add(gameObject);
    }

    void OnDisable() {
        if (IngredientManager.instance.currIngredient.Contains(gameObject))
            IngredientManager.instance.currIngredient.Remove(gameObject);
    }

    void FixedUpdate() {
        if (ingredientTypes.Count <= 1) return;

        currTypeCol = Color.Lerp(currTypeCol, IngredientManager.instance.GetTypeColorByIndex((int)ingredientTypes[currTypeColIndex]), colorTransitionSpeed * Time.deltaTime);

        if (currTypeCol == IngredientManager.instance.GetTypeColorByIndex((int)ingredientTypes[currTypeColIndex])) {
            currTypeColIndex = (currTypeColIndex + 1) % ingredientTypes.Count;
        }

        for (int i = 0; i < meshRendL.Count; i++) {
            for (int j = 0; j < meshRendL[i].outlines.Count; j++) {
                meshRendL[i].outlines[j].OutlineColor = currTypeCol;
            }
        }
    }

    public List<IngredientType> GetIngredientTypes() {
        return ingredientTypes;
    }

    public List<IngredientColor> GetIngredientColors() {
        return ingredientColors;
    }

    public IngredientSerializable GetNewIngredientFromThis() {
        IngredientSerializable newI = new IngredientSerializable();

        newI.ingredientTypes = new List<IngredientType>();
        for (int i = 0; i < ingredientTypes.Count; i++) {
            newI.ingredientTypes.Add(ingredientTypes[i]);
        }
        newI.ingredientColors = new List<IngredientColor>();
        for (int i = 0; i < ingredientColors.Count; i++) {//randomColors
            newI.ingredientColors.Add(ingredientColors[i]);
        }

        return newI;
    }

}
