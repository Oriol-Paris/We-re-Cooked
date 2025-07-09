using TMPro;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
    public TextMeshProUGUI amountTxt;
    //Script with amount saved

    void Start()
    {
        //Change the 00 with the amount
        ShowOrdersCompleted(00);
    }

    void ShowOrdersCompleted(int num)
    {
        amountTxt.text = num.ToString();
    }
}
