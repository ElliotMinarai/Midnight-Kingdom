using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene 2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        // Destruir el GameManager actual para evitar problemas de instancias duplicadas
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        SceneManager.LoadScene("MainMenu");  // Cambia "MainMenu" al nombre de la escena del menú principal
    }
}
