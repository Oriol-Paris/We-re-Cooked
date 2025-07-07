using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum IngredientType {
    ORGANIC,
    METALLIC,
    FIBER,//madera
    SWEET,
    SALTY
}
public enum IngredientColor {
    WHITE,
    RED,
    GREEN,
    BLUE,
    YELLOW,
    CYAN,
    MAGENTA,
    GRAY,
    BLACK
}

public class IngredientManager : MonoBehaviour {
    public static IngredientManager instance;

    public List<GameObject> ingredientL;

    public List<GameObject> currIngredient;//ingredientes en escena

    public Dictionary<int, Color> ingredientColDic;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
            return;
        }

        ingredientColDic = new Dictionary<int, Color>();
        ingredientColDic.Add((int)IngredientColor.WHITE, Color.white);
        ingredientColDic.Add((int)IngredientColor.RED, Color.red);
        ingredientColDic.Add((int)IngredientColor.GREEN, Color.green);
        ingredientColDic.Add((int)IngredientColor.BLUE, Color.blue);
        ingredientColDic.Add((int)IngredientColor.YELLOW, Color.yellow);
        ingredientColDic.Add((int)IngredientColor.CYAN, Color.cyan);
        ingredientColDic.Add((int)IngredientColor.MAGENTA, Color.magenta);
        ingredientColDic.Add((int)IngredientColor.GRAY, Color.gray);
        ingredientColDic.Add((int)IngredientColor.BLACK, Color.black);
    }


    public void AddIngredient(GameObject o) {
        currIngredient.Add(o);
    }

    public GameObject GetIngredient(int index) {
        return ingredientL[index];
    }

    public GameObject GetRandomIngredient() {
        int index = UnityEngine.Random.Range(0, ingredientL.Count);
        return ingredientL[index];
    }

    public Color GetColorByIndex(int index) {
        return ingredientColDic[index];
    }

    public IngredientType GetRandomType() {
        return (IngredientType)UnityEngine.Random.Range(0, (int)Enum.GetValues(typeof(IngredientType)).Cast<IngredientType>().Max() + 1);
    }
    public IngredientColor GetRandomColor() {
        return (IngredientColor)UnityEngine.Random.Range(0, (int)Enum.GetValues(typeof(IngredientColor)).Cast<IngredientColor>().Max() + 1);
    }

}
