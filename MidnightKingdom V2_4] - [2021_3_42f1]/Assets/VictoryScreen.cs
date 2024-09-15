using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public string Level2;  // Nombre del próximo nivel

    public void LoadNextLevel()
    {
        // Destruir el GameManager actual para evitar problemas de instancias duplicadas
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        SceneManager.LoadScene(Level2);  // Cargar la siguiente escena
    }

    public void ReturnToMainMenu()
    {
        // Destruir el GameManager actual para evitar problemas de instancias duplicadas
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        SceneManager.LoadScene("MainMenu");  // Cargar el menú principal
    }
}
