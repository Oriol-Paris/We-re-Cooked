using UnityEngine;

public class IngredientContainerAdder : MonoBehaviour {
    public IngredientContainer ingredientC;


    void OnTriggerEnter(Collider col) {
        if (col.GetComponent<Ingredient>()) {
            ingredientC.AddIngredient(col.GetComponent<Ingredient>());
            Destroy(col.GetComponent<Ingredient>());
            //col.GetComponent<Collider>().enabled = false;
            Destroy(col.GetComponent<Collider>());
            col.GetComponent<Rigidbody>().useGravity = false;
            col.GetComponent<Rigidbody>().isKinematic = true;
            col.transform.parent = transform;
        }
    }

}
