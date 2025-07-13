using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CalculateScore : MonoBehaviour
{
    public ScoreBar score;

    public ReceipeContainer recipe;
    public Receipe r;

    //public Customer customer;
    public int customerIndex;

    [Header("Points x Aspects")]
    public int typePt;
    public int colorPt;
    public int treatmentPt;
    public int objMultiplier;

    [Header("Scores")]
    public int maxScore;
    public int recipeScore;
    public int ingScore;

    [Header("Other")]
    public float timeBeforeResetScore;

    public GameObject lastPlate;

    public AudioSource audio;

    private void Start()
    {
        StartCoroutine(UpdateRecipie());
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
        if (!other.GetComponent<IngredientContainer>()) return;

        var plate = other.GetComponent<IngredientContainer>();

        if (plate.receivingTreatement == ReceipeTreatement.NONE) { return; }
        
        audio.Play();

        CustomerManager.instance.AddRecipeDone();

        recipeScore = 0;

        lastPlate = plate.gameObject;


        score.SetScoreState(true); //Activate scorebar

        CalculateMaxScore();

        if (plate.receivingTreatement != recipe.currentR.ingredientTreat)
            treatmentPt = 0;

        CompareIngredientVsRecipe(plate.ingredientL);

        StartCoroutine(CleanWindow());
    }

    public void CompareIngredientVsRecipe(List<IngredientSerializable> ingredients)
    {
        
        foreach (var ing in ingredients)
        {
            ingScore = 0;
            CheckIngredientTypes(ing);
            CheckIngredientColors(ing);
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
        if (recipeScore < 0) recipeScore = 0;

        score.UpdateBar(recipeScore, maxScore);

    }

    void CalculateMaxScore() //Case all ingredients are perfect
    {
        maxScore = r.ingredientTypes.Count * typePt //pts for matching type
            + r.ingredientColors.Count * colorPt //pts for matching colors
            + treatmentPt; //pts for treatment
    }

    IEnumerator CleanWindow()
    {
        if (lastPlate != null) Destroy(lastPlate);

        PlateManager.instance.SpawnPlate();

        CustomerManager.instance.spawnPoints[customerIndex].customerO.GetComponent<Customer>().plateServed = true;

        r = new Receipe();
        StartCoroutine(UpdateRecipie());

        yield return new WaitForSeconds(timeBeforeResetScore);

        score.UpdateBar(0, 0);
        score.SetScoreState(false);
    }

    public int GetRecipieScore() { return recipeScore; }

    public void SetCurrentRecipie(Receipe r) { this.r = r; }
}
