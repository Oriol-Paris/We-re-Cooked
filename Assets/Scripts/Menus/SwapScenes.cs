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

    // Método para salir del juego
    public void QuitGame()
    {
        Application.Quit();  // Cierra la aplicación en un build real
    }

}
