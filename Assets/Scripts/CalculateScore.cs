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

    [Header("Points x Aspects")]
    public int typePt;
    public int colorPt;
    public int objMultiplier;

    [Header("Scores")]
    public int recipeScore;
    public int ingScore;

    private int maxIngAmount;
    private int currIngAmount;

    bool recipieReady;

    private void Start()
    {
        recipieReady = false;
        maxIngAmount = 3;

        r = recipe.currentR;

        if (r.ingredientTypes.Count == 0 && r.ingredientColors.Count == 0)
            StartCoroutine(UpdateRecipie());
        else
            r = recipe.currentR;
    }

    IEnumerator UpdateRecipie()
    {
        yield return null;
        r = recipe.currentR;
        if (r.ingredientTypes.Count == 0 && r.ingredientColors.Count == 0)
            StartCoroutine(UpdateRecipie());
        else
            StopCoroutine(UpdateRecipie());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ing Detected");
        ingScore = 0;

        var ing = other.GetComponent<Ingredient>();
        if (ing == null) return;

        CheckIngredientTypes(ing);
        CheckIngredientColors(ing);

        if (currIngAmount < maxIngAmount)
            Calculate();
    }

    void CheckIngredientTypes(Ingredient ing)
    {
        foreach (var t in ing.ingredientTypes)
        {
            Debug.Log("ing type: " + t.ToString());
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
            Debug.Log("ing color: " + c.ToString());
            if (recipe.currentR.ingredientColors.Contains(c))
                ingScore += colorPt;
            else
                ingScore -= colorPt;
        }
    }

    void Calculate()
    {
        Debug.Log("ing score: " + ingScore);
        ingScore *= objMultiplier;
        Debug.Log("ing score: " + ingScore);
        recipeScore += ingScore;
        currIngAmount++;
        if (currIngAmount == maxIngAmount)
            recipieReady = true;
    }

    public bool RecipieReady() { return recipieReady; }

    public void SetMaxIngAmount(int maxIngAmount) { this.maxIngAmount = maxIngAmount; }

    public int GetRecipieScore() { return recipeScore; }
}
