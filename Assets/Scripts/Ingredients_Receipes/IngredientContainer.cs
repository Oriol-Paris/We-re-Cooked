using System.Collections.Generic;
using UnityEngine;


public class IngredientContainer : MonoBehaviour {
    public List<IngredientSerializable> ingredientL;
    public ReceipeTreatement receivingTreatement;

    public int currIng = 0;
    public int maxIng = 5;


    public bool CheckContainsIngredient(IngredientSerializable i) {
        return ingredientL.Contains(i);
    }
    public void AddIngredient(Ingredient i) {
        if (currIng >= maxIng) return;
        currIng++;
        IngredientSerializable newI = i.GetNewIngredientFromThis();
        ingredientL.Add(newI);
    }

}
