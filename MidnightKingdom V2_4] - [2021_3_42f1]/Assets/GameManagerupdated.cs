//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class GameManagerupdated : MonoBehaviour
//{
//    public static GameManager instance { get; private set; }

//    public GameObject[] hearts;
//    public int maxHealth = 3;
//    public int playerHealth;
//    public Image staminaBarImage;
//    public Sprite[] staminaSprites;
//    public int maxStamina = 5;
//    public int currentStamina;

//    public GameObject victoryScreen;
//    public GameObject gameOverScreen;
//    public GameObject pauseMenu; // Menú de pausa

//    private Vector3 playerStartPosition;
//    private bool isPaused = false; // Estado de pausa

//    private void Awake()
//    {
//        if (instance != null && instance != this)
//        {
//            Destroy(this.gameObject);
//        }
//        else
//        {
//            instance = this;
//            // DontDestroyOnLoad(gameObject); 
//        }
//    }

//    void Start()
//    {
//        ResetGameValues();
//        InitializePlayer();
//        DeactivatePauseMenu(); // Asegurarse de que el menú de pausa esté desactivado al inicio
//    }

//    void ResetGameValues()
//    {
//        UpdateLivesUI();

//        DeactivateAllScreens();
//    }

//    void InitializePlayer()
//    {
//        hearts = GameObject.FindGameObjectsWithTag("Heart");

//        if (hearts == null || hearts.Length == 0)
//        {
//            Debug.LogWarning("No hearts found. Please make sure that all hearts are tagged correctly in the scene.");
//        }

//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            playerStartPosition = player.transform.position;
//        }

//        playerHealth = maxHealth;
//        currentStamina = maxStamina;
//        UpdateHealthUI();
//        UpdateStaminaUI();
//    }


//    void Update()
//    {
//        // Verificar si se presionó la tecla ESC para pausar o reanudar el juego
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (isPaused)
//            {
//                ResumeGame();
//            }
//            else
//            {
//                PauseGame();
//            }
//        }
//    }

//    public void PlayerDied()
//    {
//        playerHealth -= damage;
//        if (playerHealth <= 0)

//            if (playerLives > 0)
//        {
//            RespawnPlayer();
//        }
//        else
//        {
//            ShowGameOverScreen();
//        }
//    }

//    void RespawnPlayer()
//    {
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            player.transform.position = playerStartPosition;
//        }
//    }



//    //void CheckVictoryCondition()
//    //{
//    //    enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
//    //    if (enemiesCount == 0 && playerLives > 0)
//    //    {
//    //        ShowVictoryScreen();
//    //    }
//    //}

//    void ShowVictoryScreen()
//    {
//        if (victoryScreen != null)
//        {
//            victoryScreen.SetActive(true);
//            Time.timeScale = 0f;
//        }
//    }

//    void ShowGameOverScreen()
//    {
//        if (gameOverScreen != null)
//        {
//            gameOverScreen.SetActive(true);
//            Time.timeScale = 0f;
//        }
//    }

//    public void RestartLevel()
//    {
//        ResetGameValues();
//        Time.timeScale = 1f;
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//        Invoke(nameof(InitializePlayer), 0.1f);
//    }

//    void DeactivateAllScreens()
//    {
//        if (victoryScreen != null && victoryScreen.activeSelf)
//        {
//            Debug.Log("Desactivando pantalla de victoria");
//            victoryScreen.SetActive(false);
//        }

//        if (gameOverScreen != null && gameOverScreen.activeSelf)
//        {
//            Debug.Log("Desactivando pantalla de game over");
//            gameOverScreen.SetActive(false);
//        }
//    }

//    public void ReturnToMainMenu()
//    {
//        DeactivateAllScreens();
//        Time.timeScale = 1f;
//        SceneManager.LoadScene("MainMenu");
//    }

//    // Métodos para pausar y reanudar el juego osea el paused

//    void PauseGame()
//    {
//        pauseMenu.SetActive(true); // Mostrar el menú de pausa
//        Time.timeScale = 0f;       // Detener el tiempo del juego
//        isPaused = true;           // Establecer el estado de pausa
//    }

//    public void ResumeGame()
//    {
//        pauseMenu.SetActive(false); // Ocultar el menú de pausa
//        Time.timeScale = 1f;        // Reanudar el tiempo del juego
//        isPaused = false;           // Desactivar el estado de pausa
//    }

//    void DeactivatePauseMenu()
//    {
//        if (pauseMenu != null)
//        {
//            pauseMenu.SetActive(false);
//        }
//    }
//}
