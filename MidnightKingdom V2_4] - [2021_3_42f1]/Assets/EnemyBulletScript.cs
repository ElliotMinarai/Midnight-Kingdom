using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public int damage = 1; // Da�o que la bala hace al jugador
    private bool hasDamaged = false; // Para evitar aplicar da�o m�ltiples veces

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasDamaged)
        {
            hasDamaged = true; // Marca que el da�o ha sido aplicado
            // Obtener la instancia del GameManager
            GameManager gameManager = GameManager.instance;
            if (gameManager != null)
            {
                // Aplicar da�o al jugador a trav�s del GameManager
                gameManager.TakeDamage(damage);
            }
            // Destruir la bala despu�s de impactar
            Destroy(gameObject);
        }
    }
}


