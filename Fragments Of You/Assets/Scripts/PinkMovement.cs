using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator animator;
    public AudioSource jumpSFX;
    public AudioSource landedSFX;
    public AudioSource walkingSFX;
    public AudioSource DeathSFX;

    private float startingPitch = 2.8f;
    private float startingVolume = 0.1f;

    public GameOver_Condition gameOver_Condition;

    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;

    private bool Arms = true;
    private bool Legs = true;
    private bool wallJumped = false;
    // Detect when you use the toggle, ensures music isn’t played multiple times
    private bool movementChange;

    private Respawn resp;

    private void Start()
    {
        // get component
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        resp = GetComponent<Respawn>();

        // freeze rotation
        rb.freezeRotation = true;
        wallJumped = false;
        movementChange = false;
        //Initialize the pitch of walkingSFX
        walkingSFX.pitch = startingPitch;
        //Starting volume for walkingSFX
        walkingSFX.volume = startingVolume;

        resp.setRespawn(this.transform.position);
    }


    private void Update()
    {
        /*** Basic characater movement and start of the abilities that can be lost ***/
        
        // Jump input is set to - spacebar 
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

        // Good note*: Input.GetButtonDown() only returns true for the frame in which the button was pressed.
        // Input.GetButton returns true always while holding down the key.
         if (Input.GetButton("Horizontal") && movementChange){
            if (IsGrounded() && hasLegs()){
                if(!walkingSFX.isPlaying){
              // Walking Sound Effect here
                 walkingSFX.Play();
                }
            }
            movementChange = false;
        }

        /***player falls off level respawn boundary ***/
        if (this.transform.position.y < -7)
        {
            // resp.respawnPlayer();
            StartCoroutine(PlayDeathAnim());
        }

        
        if (IsGrounded())
        {
            wallJumped = false;
         
        }

    }

    private void FixedUpdate()
    {
        // basic main character movement left/right
        // player should always have this
        Move();
        FlipPlayer();
    }

    // Movement functions start -------------------------------------------------------------------
    private void Move()
    {
        // get input - Horizontal is set to 'a' and 'd' / left and right arrow keys.
        dirX = Input.GetAxisRaw("Horizontal");
        // modify velocity based on input
        rb.AddForce(Vector2.right * dirX * moveSpeed, ForceMode2D.Force);
        // Play walk Animation
        animator.SetFloat("Speed", Mathf.Abs(dirX));
        // Play walking Sound Effect
        movementChange = true;
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
        FlipPlayer();
         DeathSFX.Play();
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