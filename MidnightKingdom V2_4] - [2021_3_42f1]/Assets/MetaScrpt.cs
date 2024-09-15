using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaScrpt : MonoBehaviour
{
    public class MetaScript : MonoBehaviour
    {
        public GameObject victoryScreen;  // Asigna la pantalla de victoria en el Inspector

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // Muestra la pantalla de victoria
                victoryScreen.SetActive(true);
                // Opcional: Pausar el juego
                Time.timeScale = 0f;
            }
        }
    }
}
