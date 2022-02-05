using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator animator;
    private PinkGrapple grapple;
    public AudioSource jumpSFX;
    public AudioSource landedSFX;

    public GameOver_Condition gameOver_Condition;

    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    [SerializeField] private float inAirMoveSpeed = 5f;
    [SerializeField] private float groundedMoveSpeed = 7f;
    [SerializeField] private float grappledMoveSpeed = 10f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = 9.81f;

    private bool Arms = true;
    private bool Legs = true;
    private bool wallJumped = false;

    private Respawn resp;

    private void Start()
    {
        // get component
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        resp = GetComponent<Respawn>();
        grapple = GetComponent<PinkGrapple>();

        // freeze rotation
        rb.freezeRotation = true;
        wallJumped = false;

        resp.setRespawn(this.transform.position);
    }


    private void Update()
    {
        // start of abilities that can be lost
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() && hasLegs())
            {
                Jump();
            }
            if(isFacingWall() && !IsGrounded() && !wallJumped)
            {
                WallJump();
                wallJumped = true;
            }
        }

        // if player falls off level respawn
        if (this.transform.position.y < -8)
        {
            // resp.respawnPlayer();
            StartCoroutine(PlayDeathAnim());
        }

        
        if (IsGrounded())
        {
            wallJumped = false;
        }

        if(!IsGrounded() && !grapple.getAnchored())
        {
            rb.AddForce(Vector2.down * gravity, ForceMode2D.Force);
        }

    }

    private void FixedUpdate()
    {
        // basic left/right movement
        // player should always have this
        if(IsGrounded())
        {
            moveSpeed = groundedMoveSpeed;
        }
        else if(!IsGrounded()&&grapple.getAnchored())
        {
            moveSpeed = grappledMoveSpeed;
        }
        else
        {
            moveSpeed = inAirMoveSpeed;
        }
        Move();

        FlipPlayer();
    }

    // Movement functions start -------------------------------------------------------------------
    private void Move()
    {
        // get input
        dirX = Input.GetAxisRaw("Horizontal");
        if(dirX > 0.1 || dirX < -0.1)
        {
            // modify velocity based on input
            rb.AddForce(Vector2.right * dirX * moveSpeed, ForceMode2D.Force);
            // Play walk Animation
            animator.SetFloat("Speed", Mathf.Abs(dirX));
        }
        else if(IsGrounded())
        {
            animator.SetFloat("Speed", Mathf.Abs(dirX));
            rb.velocity = new Vector2(rb.velocity.x/4,rb.velocity.y);
            if(rb.velocity.x < 0.1 || rb.velocity.x>-0.1)
            {
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
        }

    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // Play Jump Animation
        animator.SetBool("isJumping", true);
        // Jump Sound Effect here
        jumpSFX.Play();
    }

    private void WallJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if(sprite.flipX == false)
        {
            rb.AddForce(Vector2.left * (jumpForce / 1.5f), ForceMode2D.Impulse);
        }
        else 
        {
            rb.AddForce(Vector2.right * (jumpForce / 1.5f), ForceMode2D.Impulse);
        }

        // Play Jump Animation
        animator.SetBool("isJumping", true);
        // Jump Sound Effect here
        jumpSFX.Play();
    }

    // GroundCheck
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool isFacingWall()
    {
        if(sprite.flipX == false)
        {
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, .1f, jumpableGround);
        }
        else 
        {
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, .1f, jumpableGround);
        }
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
        }
        else if (rb.velocity.x > 0.1f)
        {
            sprite.flipX = false;
        }
    }
    // Environmental Collison's with player - Death by spike, Landing after jump, etc.
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            PlayerDeath();
        }
        // When player land on the ground, stop the jumping animation
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
             // landed sound effect
              landedSFX.Play();
        }
    }

    private void PlayerDeath()
    {
        StartCoroutine(PlayDeathAnim());
    }

    IEnumerator PlayDeathAnim()
    {
        animator.SetBool("Dead", true);
        animator.SetBool("Respawn", false);
        yield return new WaitForSeconds(1f);
        resp.respawnPlayer();
    }
}