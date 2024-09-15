using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public int damage = 1; // Daño que la bala hace al jugador
    private bool hasDamaged = false; // Para evitar aplicar daño múltiples veces

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasDamaged)
        {
            hasDamaged = true; // Marca que el daño ha sido aplicado
            // Obtener la instancia del GameManager
            GameManager gameManager = GameManager.instance;
            if (gameManager != null)
            {
                // Aplicar daño al jugador a través del GameManager
                gameManager.TakeDamage(damage);
            }
            // Destruir la bala después de impactar
            Destroy(gameObject);
        }
    }
}


