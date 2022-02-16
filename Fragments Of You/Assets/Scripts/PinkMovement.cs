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
    public AudioSource walkingSFX;
    public AudioSource DeathSFX;

    private float startingPitch = 2.8f;
    private float startingVolume = 0.1f;

    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    [SerializeField] private float inAirMoveSpeed = 5f;
    [SerializeField] private float groundedMoveSpeed = 7f;
    [SerializeField] private float swingSpeed = 100f;
    [SerializeField] private float swingTension = 200f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = 9.81f;
    /**set maxVelocity commented out for now**/
    // [SerializeField] private float maxXVelocity = 8.5f; does nothing!!!!!

    private float wallcoyote = 0f;

    private bool Arms = true;
    private bool Legs = true;
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
        grapple = GetComponent<PinkGrapple>();

        // freeze rotation
        rb.freezeRotation = true;
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
            if(!IsGrounded() && isFacingWall())
            {
                WallJump();
            }
            if(!IsGrounded() && wallcoyote>0 && !isFacingWall())
            {
                if(sprite.flipX)
                {
                    sprite.flipX = false;
                }
                else
                {
                    sprite.flipX = true;
                }
                WallJump();
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

    }

    private void FixedUpdate()
    {
        // basic main character movement left/right
        // player should always have this
        if(IsGrounded())
        {
            moveSpeed = groundedMoveSpeed;
        }
        else if(!IsGrounded() && grapple.getAnchored())
        {
            moveSpeed = swingSpeed;
            rb.AddForce(Vector2.down * swingTension, ForceMode2D.Force);

        }
        else
        {
            moveSpeed = inAirMoveSpeed;
        }
        Move();
        if(isFacingWall() && !IsGrounded() && rb.velocity.y < 0)
        {
            wallcoyote = 0.3f;
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y/1.8f);
        }

        if(!IsGrounded() && !grapple.getAnchored())
        {
            rb.AddForce(Vector2.down * gravity, ForceMode2D.Force);
        }

        FlipPlayer();
        if(wallcoyote>0)
        {
            wallcoyote -= Time.deltaTime;
        }

    }

    // Movement functions start -------------------------------------------------------------------
    private void Move()
    {
        // get input - Horizontal is set to 'a' and 'd' / left and right arrow keys.
        dirX = Input.GetAxisRaw("Horizontal");



        if(dirX > 0.1 || dirX < -0.1)
        {
            // modify velocity based on input
            rb.AddForce(Vector2.right * dirX * moveSpeed, ForceMode2D.Force);
            // Play walk Animation
            animator.SetFloat("Speed", Mathf.Abs(dirX));
            // Play walking Sound Effect
            movementChange = true;
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

        //float wallJumpForce  = jumpForce / 1.5f;
        float wallJumpForce  = jumpForce;

        rb.velocity = new Vector2(0,0);

        rb.AddForce(Vector2.up * wallJumpForce, ForceMode2D.Impulse);

        if(sprite.flipX == false)
        {
            rb.AddForce(Vector2.left * wallJumpForce / 1.5f, ForceMode2D.Impulse);
        }
        else 
        {
            rb.AddForce(Vector2.right * wallJumpForce / 1.5f, ForceMode2D.Impulse);
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

/*** Environmental Collison's with player ***/ 
    public void OnCollisionEnter2D(Collision2D other)
    {
         // Environmental Collison's with player - Death by spike, Landing after jump, etc.
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

        // Fix player postion on the movingplatforms
        if (other.gameObject.CompareTag("movingPlatform"))
        {
            animator.SetBool("isJumping", false);
             // landed sound effect
              landedSFX.Play();
            this.gameObject.transform.parent = other.gameObject.transform;
        }

        /***Player dies - falls off level respawn boundary ***/
        if (other.gameObject.CompareTag("FallDeath")){
            Debug.Log("FallThreshold met");
            PlayerDeath();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // When player leave moving platform, move freely
        if (other.gameObject.CompareTag("movingPlatform"))
        {
            this.gameObject.transform.parent = null;
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Dead");
        FlipPlayer();
        DeathSFX.Play();
        StartCoroutine(PlayDeathAnim());
    }

    IEnumerator PlayDeathAnim()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("Dead", true);
        animator.SetBool("Respawn", false);
        yield return new WaitForSeconds(1f);
        resp.respawnPlayer();
    }
}