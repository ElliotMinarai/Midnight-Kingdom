using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject victoryScreen;

    public void ShowVictoryScreen()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
            Time.timeScale = 0f;  // Pausar el juego cuando se muestre la pantalla de victoria
        }
    }

    public void LoadMainMenu()
    {
        Debug.Log("LoadMainMenu button clicked");
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        SceneManager.LoadScene("MainMenu");  // Cargar el menú principal
    }


    public void RestartLevel()
    {
        // Destruir el GameManager actual para reiniciar el estado del juego
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Cargar la escena actual
    }
}