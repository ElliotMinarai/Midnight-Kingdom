using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int amount = 1;  // Cantidad de monedas que da este �tem

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.AddCoin(amount);  // A�ade monedas al GameManager
            Destroy(gameObject);  // Destruye la moneda tras recogerla
        }
    }
}

