using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashSpeed = 20f;
    public int dashStaminaCost = 1;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.1f;
    public int staminaCost = 1;
    public float fallThreshold = -5f;

    public GameObject bulletPrefab1; // Bala 1
    public GameObject bulletPrefab2; // Bala 2
    public Transform firePoint1; // Punto de disparo 1
    public Transform firePoint2; // Punto de disparo 2
    public float projectileSpeed = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool facingRight = true;
    private bool isDashing = false;
    private bool isWallClinging = false;
    private Collider2D playerCollider;

    private int jumpCount = 0;
    public int maxJumps = 2;
    private bool canRechargeStamina = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        ResetPlayerState();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        CheckFall();

        if (isGrounded && canRechargeStamina)
        {
            RechargeStamina();
        }
    }

    void HandleShooting()
    {
        // Disparo de la bala 1 con F
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot(bulletPrefab1, firePoint1);
        }

        // Disparo de la bala 2 con G
        if (Input.GetKeyDown(KeyCode.G))
        {
            Shoot(bulletPrefab2, firePoint2);
        }
    }

    void Shoot(GameObject bulletPrefab, Transform firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, bulletCollider);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        BulletScript bulletScript = bullet.GetComponent<BulletScript>(); // Acceder a BulletScript

        if (facingRight)
        {
            rb.velocity = transform.right * bulletScript.speed; // Usar la velocidad desde BulletScript
            bullet.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            rb.velocity = -transform.right * bulletScript.speed;
            bullet.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


    void HandleMovement()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.LeftShift) && !isDashing && GameManager.instance.HasEnoughStamina(dashStaminaCost))
        {
            StartCoroutine(Dash());
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -1f;
                if (facingRight)
                {
                    Flip();
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveX = 1f;
                if (!facingRight)
                {
                    Flip();
                }
            }

            if (isWallClinging && moveX != 0)
            {
                isWallClinging = false;
            }

            if (!isDashing && !isWallClinging)
            {
                Vector2 movement = new Vector2(moveX * moveSpeed, rb.velocity.y);
                rb.velocity = movement;
            }

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && jumpCount < maxJumps)
            {
                if (GameManager.instance.HasEnoughStamina(staminaCost))
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    GameManager.instance.UseStamina(staminaCost);
                    jumpCount++;
                    canRechargeStamina = false;
                }
            }
            else if (isTouchingWall && !isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && GameManager.instance.HasEnoughStamina(staminaCost))
            {
                PerformWallJump();
            }
        }

        if (isTouchingWall && !isGrounded && moveX == 0)
        {
            WallCling();
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        Vector2 dashDirection = new Vector2(facingRight ? 1 : -1, 0);
        rb.velocity = dashDirection * dashSpeed;

        GameManager.instance.UseStamina(dashStaminaCost);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;

        yield return new WaitForSeconds(dashCooldown);

        isDashing = false;
    }

    void PerformWallJump()
    {
        Flip();
        rb.velocity = new Vector2(facingRight ? moveSpeed : -moveSpeed, jumpForce);
        jumpCount = 0;
        canRechargeStamina = false;
        isWallClinging = false;
        isTouchingWall = false;
    }

    void WallCling()
    {
        isWallClinging = true;
        rb.velocity = new Vector2(0, 0);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CheckFall()
    {
        if (transform.position.y < fallThreshold)
        {
            GameManager.instance.TakeDamage(1);
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        transform.position = GameManager.instance.GetPlayerStartPosition();
    }

    void RechargeStamina()
    {
        if (GameManager.instance.currentStamina < GameManager.instance.maxStamina)
        {
            GameManager.instance.currentStamina += 1;
            GameManager.instance.UpdateStaminaUI();
        }
    }

    void ResetPlayerState()
    {
        isGrounded = false;
        isTouchingWall = false;
        facingRight = true;
        isDashing = false;
        isWallClinging = false;
        jumpCount = 0;
        canRechargeStamina = true;

        transform.position = GameManager.instance.GetPlayerStartPosition();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 3f;

        Time.timeScale = 1f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            canRechargeStamina = true;
            isWallClinging = false;
        }
        else if (collision.gameObject.CompareTag("Hazards") || collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.TakeDamage(1);
            RespawnPlayer();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
            isWallClinging = false;
        }
    }
}

