using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public List<GameObject> tabs;

    public void SelectTab(int _id)
    {
        for(int i = 0; i < tabs.Count; i++)
        {
            if (i == _id)
                tabs[i].SetActive(true);
            else
                tabs[i].SetActive(false);
        }
    }
}
