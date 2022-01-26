using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator animator;

    public GameOver_Condition gameOver_Condition;

    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    private float moveSpeed;
    [SerializeField] private float inAirMoveSpeed = 5f;
    [SerializeField] private float groundedMoveSpeed = 7f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 9.81f;

    private bool Arms = true;
    private bool Legs = true;

    private void Start()
    {
        // get component
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // freeze rotation
        rb.freezeRotation = true;

        // set respawn to starting position
    }


    private void Update()
    {
        // basic left/right movement
        // player should always have this
        if(!IsGrounded())
        {
            moveSpeed = inAirMoveSpeed;
        }
        else
        {
            moveSpeed = groundedMoveSpeed;
        }
        Move();
        FlipPlayer();
        
        
        // start of abilities that can be lost
        if (Input.GetButtonDown("Jump"))
        {
            if(IsGrounded() && hasLegs())
            {
                Jump();
            }
        }

        if(!IsGrounded())
        {
            rb.AddForce(Vector2.down * gravity, ForceMode2D.Force);
        }
    }
    
    private void FixedUpdate() 
    {
        
    }

    // Movement functions start -------------------------------------------------------------------
    private void Move() 
    {
        // get input
        dirX = Input.GetAxisRaw("Horizontal");
        // modify velocity based on input
        rb.AddForce(Vector2.right * dirX * moveSpeed * Time.deltaTime * 100, ForceMode2D.Force);
        // Play walk Animation
        animator.SetFloat("Speed", Mathf.Abs(dirX));
        if(dirX==0 && IsGrounded())
        {
            rb.velocity *= 0.5f;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // Play Jump Animation
        animator.SetBool("isJumping", true);
    }

    // GroundCheck
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    // Grapple functions end ----------------------------------------------------------------------

    // State functions start ----------------------------------------------------------------------
    // function to lose arms
    public void loseArms()
    {
        Arms = false;
        // preform sprite switch here

    }

    // function to lose legs
    public void loseLegs()
    {
        Legs = false;
        // preform sprite switch here

    }

    public bool hasArms()
    {
        return Arms;
    }

    public bool hasLegs()
    {
        return Legs;
    }
    // State functions end ------------------------------------------------------------------------

    // Other helper functions start ---------------------------------------------------------------
    // flips player sprit to maintain correct orientation.
    private void FlipPlayer()
    {
        if (rb.velocity.x < -0.1f)
        {
            sprite.flipX = true;
        } else if (rb.velocity.x > 0.1f) {
            sprite.flipX = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // When player land on the ground, stop the jumping animation
        if (other.gameObject.CompareTag("Ground")){
            animator.SetBool("isJumping", false);
        }
    }
}