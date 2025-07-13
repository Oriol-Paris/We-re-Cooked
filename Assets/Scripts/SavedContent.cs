using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedContent : MonoBehaviour
{
    public static SavedContent instance;

    [SerializeField] private int amountRecipies;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveContents()
    {
        //PlayerPrefs.SetFloat("Recipies", CustomerManager.instance.GetRecipesDone());
        this.amountRecipies = CustomerManager.instance.GetRecipesDone();
        SceneManager.LoadScene("FinalScene", LoadSceneMode.Single);
    }


    public void SaveRecipies(int amountRecipies)
    {
        this.amountRecipies = amountRecipies;
    }

    public int GetRecipiesAmount() { return amountRecipies; }
}
