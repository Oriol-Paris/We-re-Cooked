using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum ReceipeTreatement {
    NONE,
    HEAT,
    DRY,
    FRY
}

[Serializable]
public class Receipe {
    public List<IngredientType> ingredientTypes;
    public List<IngredientColor> ingredientColors;
    public ReceipeTreatement ingredientTreat;
}

public class ReceipeGenerator : MonoBehaviour {
    public static ReceipeGenerator instance;

    public List<Sprite> ingTreatS;
    public Dictionary<int, Sprite> ingredientTreatSpriteDic;


    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
            return;
        }

        ingredientTreatSpriteDic = new Dictionary<int, Sprite>();
        for (int i = 0; i < ingTreatS.Count; i++) {
            ingredientTreatSpriteDic.Add(i, ingTreatS[i]);
        }
    }

    public Receipe GenRandomReceipe(int maxTypes, int maxColors) {
        Receipe receipe = new Receipe();
        receipe.ingredientTypes = new List<IngredientType>();
        receipe.ingredientColors = new List<IngredientColor>();

        for (int i = 0; i < maxTypes; i++) {
            receipe.ingredientTypes.Add(IngredientManager.instance.GetRandomType());
        }

        for (int i = 0; i < maxColors; i++) {
            receipe.ingredientColors.Add(IngredientManager.instance.GetRandomColor());
        }

        receipe.ingredientTreat = GetRandomTreatForCook();

        return receipe;
    }

    public ReceipeTreatement GetRandomTreatForCook() {
        return (ReceipeTreatement)UnityEngine.Random.Range(1, (int)Enum.GetValues(typeof(ReceipeTreatement)).Cast<ReceipeTreatement>().Max());
    }

    public Sprite GetTreatSpriteByIndex(int index) {
        return ingredientTreatSpriteDic[index];
    }

}
