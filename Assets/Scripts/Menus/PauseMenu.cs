using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public static PauseMenu instance;

    //public StarterAssetsInputs inputs;
    //public InventoryInteract invInteract;
    //public FirstPersonController fpc;
    public GameObject menuObj;

    public float playerRotSpeed = 0;

    [SerializeField] GameObject optionsMenu, buttons;

    [SerializeField] bool optionsOpen = false;
    [SerializeField] public bool menuOpen = false;

    [SerializeField] private Button main;


    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Open();
        }
    }

    void Start() {
        //inputs = GameObject.Find("Player").GetComponent<StarterAssetsInputs>();
        //fpc = GameObject.Find("Player").GetComponent<FirstPersonController>();
        //invInteract = GameObject.Find("Inventory").GetComponent<InventoryInteract>();

        //if (inputs != null)
        //    inputs.Exit += Open;
        menuObj.SetActive(false);
        optionsMenu.SetActive(false);
        buttons.SetActive(true);
        //playerRotSpeed = fpc.RotationSpeed;
    }
    void OnDisable() {
        //if (inputs != null)
        //    inputs.Exit -= Open;
    }

    public void UpdateRefs() {
        /*if (inputs != null)
            inputs.Exit -= Open;

        if (SceneChecker.instance.actualSceneIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (GameObject.Find("Player"))
            {
                inputs = GameObject.Find("Player").GetComponent<StarterAssetsInputs>();
                fpc = GameObject.Find("Player").GetComponent<FirstPersonController>();
            }
            if (GameObject.Find("Inventory"))
                invInteract = GameObject.Find("Inventory").GetComponent<InventoryInteract>();

            if (!GameObject.Find("Player"))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                return;
            }

            if (inputs != null)
                inputs.Exit += Open;
        }*/

        optionsMenu.SetActive(false);
        buttons.SetActive(true);
        optionsOpen = false;

    }

    public void Open()
    {
        //if (fpc.isInteracting || invInteract.inventoryOpen) return;
        //Choose between open option or close menu
        if (!optionsOpen)
        {
            menuObj.SetActive(!menuObj.activeSelf);
            menuOpen = menuObj.activeSelf;
            //fpc.SetCursorLock(false);
        }
        else
            OpenOptions(!optionsOpen);

        //Behavior when menu is active or inactive
        /*if (!menuObj.activeSelf)
        {
            fpc.canMove = true;
            fpc.SetCursorLock(false);
            fpc.RotationSpeed = playerRotSpeed;
        }
        else
        {
            fpc.canMove = false;
            fpc.SetCursorLock(true);
            fpc.RotationSpeed = 0;

            if (main != null)
                main.Select();
        }*/
        
        Time.timeScale = menuObj.activeSelf ? 0 : 1;
    }

    public void OpenOptions(bool open)
    {
        optionsOpen = open;
        if (open)
        {
            buttons.SetActive(false);
            optionsMenu.SetActive(true);
        }
        else
        {
            buttons.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }
}
