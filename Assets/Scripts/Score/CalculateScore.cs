using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateScore : MonoBehaviour
{
    public ScoreBar score;

    public ReceipeContainer recipe;
    public Receipe r;

    [Header("Points x Aspects")]
    public int typePt;
    public int colorPt;
    public int treatmentPt;
    public int objMultiplier;

    [Header("Scores")]
    public int maxScore;
    public int recipeScore;
    public int ingScore;

    [Header("Ingredients In Plate")]
    private int maxIngAmount;
    private int currIngAmount;

    bool recipieReady;

    private void Start()
    {
        recipieReady = false;
    }

    /*IEnumerator UpdateRecipie()
    {
        yield return null;
        r = recipe.currentR;
        if (r.ingredientTypes.Count == 0 && r.ingredientColors.Count == 0)
            StartCoroutine(UpdateRecipie());
        else
            StopCoroutine(UpdateRecipie());
    }*/

    private void OnTriggerEnter(Collider other)
    {
        var plate = other.GetComponent<IngredientContainer>();

        if(plate.receivingTreatement == ReceipeTreatement.NONE) { return; }

        CalculateMaxScore();
        if (plate.receivingTreatement != recipe.currentR.ingredientTreat)
            treatmentPt = 0;

        CompareIngredientVsRecipe(plate.ingredientL);
    }

    public void CompareIngredientVsRecipe(List<IngredientSerializable> ingredients)
    {
        
        foreach (var ing in ingredients)
        {
            ingScore = 0;
            CheckIngredientTypes(ing);
            CheckIngredientColors(ing);
            if (currIngAmount < maxIngAmount)
                Calculate();
        }
    }

    void CheckIngredientTypes(IngredientSerializable ing)
    {
        foreach (var t in ing.ingredientTypes)
        {
            if (recipe.currentR.ingredientTypes.Contains(t))
                ingScore += typePt;
            else
                ingScore -= typePt;
        }
    }

    void CheckIngredientColors(IngredientSerializable ing)
    {
        foreach (var c in ing.ingredientColors)
        {
            if (recipe.currentR.ingredientColors.Contains(c))
                ingScore += colorPt;
            else
                ingScore -= colorPt;
        }
    }

    //Calculate the points of the plate
    void Calculate()
    {
        //ingScore *= objMultiplier;
        recipeScore += ingScore + treatmentPt;

        score.UpdateBar(recipeScore, maxScore);
    }

    void CalculateMaxScore() //Case all ingredients are perfect
    {
        maxScore = r.ingredientTypes.Count * typePt //pts for matching type
            + r.ingredientColors.Count * colorPt //pts for matching colors
            + treatmentPt; //pts for treatment
    }

    public bool RecipieReady() { return recipieReady; }

    public void SetMaxIngAmount(int maxIngAmount) { this.maxIngAmount = maxIngAmount; }

    public int GetRecipieScore() { return recipeScore; }

    public void SetCurrentRecipie(Receipe r) { this.r = r; }
}
