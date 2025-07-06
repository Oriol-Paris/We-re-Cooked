using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneTutoName;
    public string sceneName;

    public void SwapScene()
    {
        if (!PlayerPrefs.HasKey("TutorialComplete")) {
            SceneManager.LoadScene(sceneTutoName, LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Application.Quit();  // Cierra la aplicación en un build real
    }

}
