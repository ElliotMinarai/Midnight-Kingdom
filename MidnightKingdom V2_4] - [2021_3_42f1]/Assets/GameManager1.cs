//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class GameManager1 : MonoBehaviour
//{
//    public static GameManager instance { get; private set; }

//    public GameObject[] hearts;  // Array para los GameObjects de los corazones
//    public int maxHealth = 3;  // M�ximo de corazones
//    public int playerHealth;

//    // Barra de stamina
//    public Image staminaBarImage;  // Referencia al Image de la barra de stamina en el Canvas
//    public Sprite[] staminaSprites;  // Array de sprites para los diferentes estados de la barra de stamina
//    public int maxStamina = 5;
//    public int currentStamina;

//    // UI para pantallas de victoria, derrota y pausa
//    public GameObject defeatScreen;
//    public GameObject victoryScreen;
//    public GameObject pauseScreen;

//    // Sistema de inventario
//    private Dictionary<string, int> inventory = new Dictionary<string, int>();  // Inventario para �tems recolectados

//    private Vector3 playerStartPosition;

//    // Contador de monedas
//    public Text coinText;  // Texto en la UI que mostrar� las monedas
//    private int coinCount = 0;  // Contador de monedas

//    private void Awake()
//    {
//        if (instance != null && instance != this)
//        {
//            Destroy(gameObject);
//        }
//        else
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject); // Persiste entre escenas
//        }
//    }

//    void Start()
//    {
//        InitializePlayer();
//        UpdateCoinText();  // Aseg�rate de actualizar el texto de las monedas al inicio
//    }

//    void InitializePlayer()
//    {
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            playerStartPosition = player.transform.position;
//        }

//        playerHealth = maxHealth; // Restablece la salud al inicio
//        currentStamina = maxStamina; // Restablece la stamina al inicio
//        UpdateHealthUI();
//        UpdateStaminaUI();

//        if (defeatScreen != null) defeatScreen.SetActive(false);
//        if (victoryScreen != null) victoryScreen.SetActive(false);
//        if (pauseScreen != null) pauseScreen.SetActive(false);

//        Time.timeScale = 1f;
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (pauseScreen != null)
//            {
//                if (pauseScreen.activeSelf)
//                {
//                    ResumeGame();
//                }
//                else
//                {
//                    PauseGame();
//                }
//            }
//        }
//    }

//    // M�todos de monedas
//    public void AddCoin(int amount)
//    {
//        coinCount += amount;  // Suma la cantidad de monedas recogidas
//        UpdateCoinText();  // Actualiza el texto en la pantalla
//    }

//    void UpdateCoinText()
//    {
//        if (coinText != null)
//        {
//            coinText.text = "Coins: " + coinCount.ToString();  // Muestra la cantidad de monedas
//        }
//    }

//    // M�todos de salud
//    public void TakeDamage(int damage)
//    {
//        playerHealth -= damage;
//        UpdateHealthUI();

//        if (playerHealth <= 0)
//        {
//            playerHealth = 0;
//            ShowDefeatScreen();
//        }
//    }

//    void UpdateHealthUI()
//    {
//        for (int i = 0; i < hearts.Length; i++)
//        {
//            if (hearts[i] != null)
//            {
//                Image heartImage = hearts[i].GetComponent<Image>();

//                if (heartImage != null)
//                {
//                    heartImage.enabled = i < playerHealth;
//                }
//            }
//        }
//    }

//    public void HealPlayer(int healAmount)
//    {
//        playerHealth += healAmount;
//        if (playerHealth > maxHealth)
//        {
//            playerHealth = maxHealth; // Aseg�rate de no exceder la salud m�xima
//        }
//        UpdateHealthUI(); // Actualiza la UI de los corazones
//    }

//    void ShowDefeatScreen()
//    {
//        if (defeatScreen != null)
//        {
//            defeatScreen.SetActive(true);
//            Time.timeScale = 0f;
//        }
//    }

//    void ShowVictoryScreen()
//    {
//        if (victoryScreen != null)
//        {
//            victoryScreen.SetActive(true);
//            Time.timeScale = 0f;
//        }
//    }

//    public void PlayerReachedEndOfLevel()
//    {
//        ShowVictoryScreen();
//    }

//    // M�todos de Stamina
//    public bool HasEnoughStamina(int amount)
//    {
//        return currentStamina >= amount;
//    }

//    public void UseStamina(int amount)
//    {
//        currentStamina -= amount;
//        if (currentStamina < 0)
//        {
//            currentStamina = 0;
//        }
//        UpdateStaminaUI();
//    }

//    public void UpdateStaminaUI()
//    {
//        if (staminaBarImage != null && currentStamina >= 0 && currentStamina <= maxStamina)
//        {
//            staminaBarImage.sprite = staminaSprites[currentStamina];
//        }
//    }

//    // M�todos de Inventario
//    public void AddItem(string itemName, int amount)
//    {
//        if (inventory.ContainsKey(itemName))
//        {
//            inventory[itemName] += amount;
//        }
//        else
//        {
//            inventory.Add(itemName, amount);
//        }

//        Debug.Log("Item recogido: " + itemName + " (x" + amount + ")");
//        ShowInventory(); // Muestra el inventario actualizado en consola
//    }

//    public void ShowInventory()
//    {
//        Debug.Log("Inventario actual:");
//        foreach (var item in inventory)
//        {
//            Debug.Log(item.Key + ": " + item.Value);
//        }
//    }

//    public bool HasItem(string itemName)
//    {
//        return inventory.ContainsKey(itemName);
//    }

//    // M�todos de control del juego
//    public void RestartLevel()
//    {
//        Destroy(gameObject);
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//    }

//    public void ResetGameState()
//    {
//        playerHealth = maxHealth;
//        currentStamina = maxStamina;
//        UpdateHealthUI();
//        UpdateStaminaUI();

//        if (defeatScreen != null)
//        {
//            defeatScreen.SetActive(false);
//        }
//    }

//    public void PauseGame()
//    {
//        if (pauseScreen != null)
//        {
//            pauseScreen.SetActive(true);
//            Time.timeScale = 0f;
//        }
//    }

//    public void ResumeGame()
//    {
//        if (pauseScreen != null)
//        {
//            pauseScreen.SetActive(false);
//            Time.timeScale = 1f;
//        }
//    }
//}
