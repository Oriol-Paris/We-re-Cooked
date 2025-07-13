using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScenes : MonoBehaviour
{
    public string sceneName;

    public void SwapScene()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Application.Quit();  // Cierra la aplicaci�n en un build real
    }

}
