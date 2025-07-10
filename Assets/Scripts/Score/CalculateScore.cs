using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class CalculateScore : MonoBehaviour
{
    public Receipe r;
    public ReceipeContainer recipe;

    public List<Ingredient> ingredients;

    public ScoreBar score;

    [Header("Points x Aspects")]
    public int typePt;
    public int colorPt;
    public int objMultiplier;

    [Header("Scores")]
    private int maxScore;
    public int recipeScore;
    public int ingScore;

    [Header("Ingredients In Plate")]
    private int maxIngAmount;
    private int currIngAmount;

    bool recipieReady;

    private void Start()
    {
        recipieReady = false;
        //maxIngAmount = 3;

        //r = recipe.currentR;
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
        var ing = other.GetComponent<Ingredient>();
        if (ing == null || recipieReady)
            return;
        else
        {
            //Add the ingredient to the plate and check if the plate is full
            ingredients.Add(ing);
            currIngAmount++;
            if (currIngAmount >= maxIngAmount)
                recipieReady = true;
        }
    }

    public void CompareIngredientVsRecipe()
    {
        CalculateMaxScore();
        foreach (var ing in ingredients)
        {
            ingScore = 0;
            CheckIngredientTypes(ing);
            CheckIngredientColors(ing);
            if (currIngAmount < maxIngAmount)
                Calculate();
        }
    }

    void CheckIngredientTypes(Ingredient ing)
    {
        foreach (var t in ing.ingredientTypes)
        {
            if (recipe.currentR.ingredientTypes.Contains(t))
                ingScore += typePt;
            else
                ingScore -= typePt;
        }
    }

    void CheckIngredientColors(Ingredient ing)
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
        recipeScore += ingScore;

        score.UpdateBar(recipeScore, maxScore);
    }

    void CalculateMaxScore() //Case all ingredients are perfect
    {
        maxScore = r.ingredientTypes.Count * typePt + //pts for matching type
            r.ingredientColors.Count * colorPt + //pts for matching colors
            ingredients.Count; //pts for treatment
    }

    public bool RecipieReady() { return recipieReady; }

    public void SetMaxIngAmount(int maxIngAmount) { this.maxIngAmount = maxIngAmount; }

    public int GetRecipieScore() { return recipeScore; }

    public void GetCurrentRecipie(Receipe r) { this.r = r; }
}
