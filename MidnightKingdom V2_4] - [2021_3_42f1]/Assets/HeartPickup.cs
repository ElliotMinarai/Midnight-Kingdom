using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int healthAmount = 1; // Cu�ntos corazones recupera este �tem

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.HealPlayer(healthAmount);
            Destroy(gameObject); // Destruye el �tem despu�s de ser recogido
        }
    }
}

