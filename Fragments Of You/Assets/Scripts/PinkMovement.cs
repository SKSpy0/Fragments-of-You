using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private void Start()
    {
        // get component
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        // freeze rotation
        rb.freezeRotation = true;
    }

    private void Update()
    {
        Move();
        FlipPlayer();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }
    
    private void Move() 
    {
        // get input
        dirX = Input.GetAxisRaw("Horizontal");
        // modify velocity based on input
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void FlipPlayer()
    {
        if (rb.velocity.x < -0.1f)
        {
            sprite.flipX = true;
        } else if (rb.velocity.x > 0.1f) {
            sprite.flipX = false;
        }
    }
}