using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class IngredientMeshes {
    public List<MeshRenderer> meshR;
}

public class Ingredient : MonoBehaviour {
    public List<IngredientType> ingredientTypes;
    public int maxTypes = 1;

    public List<IngredientColor> ingredientColors;
    public int maxColors = 1;

    public List<IngredientMeshes> meshRendL;


    void Start() {
        maxColors = meshRendL.Count;

        ingredientTypes = new List<IngredientType>();
        ingredientColors = new List<IngredientColor>();

        for (int i = 0; i < ingredientTypes.Count; i++) {
            ingredientTypes.Add(IngredientManager.instance.GetRandomType());
        }

        for (int i = 0; i < maxColors; i++) {//randomColors
            ingredientColors.Add(IngredientManager.instance.GetRandomColor());
            for (int j = 0; j < meshRendL[i].meshR.Count; j++) {
                meshRendL[i].meshR[j].material.color =
                    IngredientManager.instance.GetColorByIndex((int)ingredientColors[i]);
            }
        }
    }

    public List<IngredientType> GetIngredientTypes() {
        return ingredientTypes;
    }

    public List<IngredientColor> GetIngredientColors() {
        return ingredientColors;
    }

}
