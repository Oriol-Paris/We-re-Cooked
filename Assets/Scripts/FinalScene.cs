using TMPro;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
    public TextMeshProUGUI amountTxt;
    //Script with amount saved

    void Start()
    {
        ShowOrdersCompleted(SavedContent.instance.GetRecipiesAmount());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ShowOrdersCompleted(int num)
    {
        amountTxt.text = num.ToString();
    }
}
