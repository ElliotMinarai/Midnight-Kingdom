using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f; // Tiempo antes de que la bala se destruya automáticamente

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        // Mueve la bala hacia adelante
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la bala toca cualquier objeto que no sea otro proyectil
        if (!collision.CompareTag("Player") && !collision.CompareTag("Bullet"))
        {
            Destroy(gameObject); // Destruye la bala
        }

        // Si toca un enemigo, destruye también al enemigo
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyShooter"))
        {
            Destroy(collision.gameObject); // Destruye el enemigo o el enemigo que dispara
            Destroy(gameObject); // Destruye la bala
        }
    }

}

