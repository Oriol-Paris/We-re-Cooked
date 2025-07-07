using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum ReceipeTreatement {
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
    public Receipe GenRandomReceipe(int maxTypes, int maxColors) {
        Receipe receipe = new Receipe();

        for (int i = 0; i < maxTypes; i++) {
            receipe.ingredientTypes.Add(IngredientManager.instance.GetRandomType());
        }

        for (int i = 0; i < maxColors; i++) {
            receipe.ingredientColors.Add(IngredientManager.instance.GetRandomColor());
        }

        receipe.ingredientTreat = GetRandomTreat();

        return receipe;
    }

    public ReceipeTreatement GetRandomTreat() {
        return (ReceipeTreatement)UnityEngine.Random.Range(0, (int)Enum.GetValues(typeof(ReceipeTreatement)).Cast<ReceipeTreatement>().Max());
    }

}
