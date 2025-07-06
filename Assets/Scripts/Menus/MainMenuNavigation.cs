using UnityEngine;
using UnityEngine.UI;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private Button main;
    [SerializeField] private Button credits;
    [SerializeField] private Button options;
    [SerializeField] private Button newGame;

    void Start()
    {
        main.Select();
    }

    public void SelectButton(int id)
    {
        switch(id)
        {
            case 1:
                if (main != null)
                    main.Select();
                break;
            case 2:
                if (credits != null)
                    credits.Select();
                break;
            case 3:
                if (options != null)
                    options.Select();
                break;
            case 4:
                if (newGame != null)
                    newGame.Select();
                break;
        }
    }
}
