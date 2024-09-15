using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyscript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity;

    public Transform groundCheck;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(velocity, rb.velocity.y);
        if (Overlap() == false)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1,
                                                transform.localScale.y,
                                                transform.localScale.z);
            velocity *= -1;
        }
    }

    bool Overlap()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, .1f, groundMask))
            return true;
        return false;
    }
}
