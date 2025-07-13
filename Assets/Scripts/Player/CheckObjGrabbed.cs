using UnityEngine;
using UnityEngine.UI;

public class CheckObjGrabbed : MonoBehaviour
{
    public Image mira;

    public ObjectGrabber grabber;
    private void Update()
    {
        if(grabber.objGrabbed)
            mira.color = Color.black;
        else
            mira.color = Color.white;
    }
}
