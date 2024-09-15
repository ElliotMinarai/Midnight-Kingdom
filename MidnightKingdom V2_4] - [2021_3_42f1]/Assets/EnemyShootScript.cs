using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Punto desde donde se dispara la bala
    public float fireRate = 1f; // Frecuencia de disparo (en segundos)
    public float bulletSpeed = 5f; // Velocidad de la bala

    private Transform player; // Referencia al jugador
    private Vector2 direction;

    void Start()
    {
        // Busca al jugador por su tag "Player"
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Solo comienza a disparar si se encuentra al jugador
        if (player != null)
        {
            StartCoroutine(ShootAtPlayer());
        }
    }

    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            if (player != null)
            {
                // Calcula la dirección hacia el jugador
                direction = (player.position - firePoint.position).normalized;

                // Instancia la bala en el punto de disparo
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                // Ignora las colisiones entre el enemigo y su propia bala
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());

                // Asigna la dirección y velocidad a la bala
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                // Destruye la bala después de 2 segundos
                Destroy(bullet, 2f);
            }
            else
            {
                // Detén la corrutina si el jugador ha sido destruido
                yield break;
            }

            // Espera el tiempo especificado antes de disparar de nuevo
            yield return new WaitForSeconds(fireRate);
        }
    }
}




