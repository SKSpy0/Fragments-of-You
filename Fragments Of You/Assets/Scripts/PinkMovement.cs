using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMovement : MonoBehaviour
{

    [SerializeField] public int zone = 1;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator animator;
    private PinkGrapple grapple;
    public ParticleSystem feetParticles;
    public ParticleSystem rightSideParticles;
    public ParticleSystem leftSideParticles;
    public ParticleSystem landingParticles;
    public AudioSource jumpSFX;
    public AudioSource landedSFX;
    public AudioSource walkingSFX;
    public AudioSource hoppingSFX;
    public AudioSource DeathSFX;
    public SFXPrompt sfxPrompt;

    private float startingPitch = 2.8f;
    private float startingHoppingPitch = 1.5f;
    private float startingVolume = 0.3f;
    private float previousDir = 0f;

    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    [SerializeField] private float inAirMoveSpeed = 5f;
    [SerializeField] private float groundedMoveSpeed = 7f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float maxPlayerVelocity = 6f;
    /**set maxVelocity commented out for now**/
    // [SerializeField] private float maxXVelocity = 8.5f; does nothing!!!!!

    private float wallcoyote = 0f;

    private bool Arms = true;
    private bool Legs = true;
    // boolean check for movement input
    private bool movementChange;
    // Check for landing particle effect has been played or not
    private bool landingParticlePlay;
    // Check for sprite flipping particle effects
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

        //Initialize the pitch of hoppingSFX
        hoppingSFX.pitch =  startingHoppingPitch;

        //Start landing particle play as false
        landingParticlePlay = false;

        resp.setRespawn(this.transform.position);
        Debug.Log("pos: " + this.transform.position);

        if (zone == 2)
        {
            loseArms();
            animator.SetBool("isArmless", true);
            Debug.Log("NO ARMS");
        }
        if (zone == 3)
        {
            loseLegs();
            animator.SetBool("isLegless", true);
            Debug.Log("NO LEGS");
        }
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
            if (!IsGrounded() && isFacingWall() && hasLegs())
            {
                WallJump();
            }
            if (!IsGrounded() && wallcoyote > 0 && !isFacingWall() && hasLegs())
            {
                WallJump();
            }

        }

        // Good note*: Input.GetButtonDown() only returns true for the frame in which the button was pressed.
        // Input.GetButton returns true always while holding down the key.
        if (Input.GetButton("Horizontal") && movementChange)
        {
            if (IsGrounded() && hasLegs())
            {
                if (!walkingSFX.isPlaying)
                {
                    // Walking Sound Effect here
                    walkingSFX.Play();
                }
            }
            if (IsGrounded() && !hasLegs())
            {
                if (!hoppingSFX.isPlaying)
                {
                    // Hopping Sound Effect here
                    hoppingSFX.Play();
                }
            }
            movementChange = false;
        }
    }

    private void FixedUpdate()
    {
        // basic main character movement left/right
        // player should always have this
        if (IsGrounded())
        {
            if(landingParticlePlay){
                CreateLandingParticles();
                landingParticlePlay = false;
            }
            moveSpeed = groundedMoveSpeed;
        }
        else
        {
            landingParticlePlay = true;
            moveSpeed = inAirMoveSpeed;
        }
        if(grapple.getAnchored())
        {
            moveSpeed = groundedMoveSpeed;
        }
        Move();
        if (isFacingWall() && !IsGrounded() && rb.velocity.y < 0)
        {
            wallcoyote = 0.1f;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 1.3f);
            animator.SetBool("isWallSlide", true);
            //Create particles on side of player
            if(!sprite.flipX){
                CreateRightSideParticles();
            } else {
                CreateLeftSideParticles();
            }
        }
        else
        {
            animator.SetBool("isWallSlide", false);
        }

        if (!IsGrounded() && !grapple.getAnchored())
        {
            rb.AddForce(Vector2.down * gravity, ForceMode2D.Force);
        }

        FlipPlayer();
        if (wallcoyote > 0)
        {
            wallcoyote -= Time.deltaTime;
        }

    }

    // Movement functions start -------------------------------------------------------------------
    private void Move()
    {
        // get input - Horizontal is set to 'a' and 'd' / left and right arrow keys.
        dirX = Input.GetAxisRaw("Horizontal");
        //Debug.Log("x velocity: " + rb.velocity.x);

        if (dirX > 0.1 || dirX < -0.1)
        {
            if ((dirX > 0 && previousDir > 0) || (dirX < 0 && previousDir < 0) )
            {
                if(rb.velocity.x <= maxPlayerVelocity && rb.velocity.x >= -maxPlayerVelocity)
                {
                    // modify velocity based on input
                    rb.AddForce(Vector2.right * dirX * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
                    // Play walk Animation
                    animator.SetFloat("Speed", Mathf.Abs(dirX));
                    // Play walking Sound Effect
                    movementChange = true;
                }
            }
            else if (IsGrounded())
            {
                animator.SetFloat("Speed", Mathf.Abs(dirX));
                rb.velocity = new Vector2(rb.velocity.x / 4, rb.velocity.y);
                if (rb.velocity.x < 0.1 || rb.velocity.x > -0.1)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
        }
        else if (IsGrounded())
        {
            animator.SetFloat("Speed", Mathf.Abs(dirX));
            rb.velocity = new Vector2(rb.velocity.x / 4, rb.velocity.y);
            if (rb.velocity.x < 0.1 || rb.velocity.x > -0.1)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        previousDir = dirX;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // Play Jump Animation
        animator.SetBool("isJumping", true);
        // Jump Sound Effect here
        jumpSFX.Play();
        sfxPrompt.NewSfxPrompt("Jumping");
        // Generate Particle effects
        CreateFeetParticles();
    }

    private void WallJump()
    {

        //float wallJumpForce  = jumpForce / 1.5f;
        float wallJumpForce = jumpForce;

        rb.velocity = new Vector2(0, 0);

        rb.AddForce(Vector2.up * wallJumpForce, ForceMode2D.Impulse);

        if (sprite.flipX == false)
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

        // Generate Particle effects
        CreateFeetParticles();

        wallcoyote = 0;
    }

    // GroundCheck
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public bool isFacingWall()
    {
        if (sprite.flipX == false)
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
    }

    // function to lose legs
    public void loseLegs()
    {
        Legs = false;
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
        if (!(isFacingWall() && !IsGrounded() && rb.velocity.y < 0))
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

    }

    /*** Environmental Collison's with player ***/
    public void OnCollisionEnter2D(Collision2D other)
    {
        // Environmental Collison's with player - Death by spike, Landing after jump, etc.
        if (other.gameObject.CompareTag("Spike") && !GameManager.GodMode)
        {
            PlayerDeath();
        }

        // Environmental Collison with player - Death by laser.
        if (other.gameObject.CompareTag("LaserTrap") && !GameManager.GodMode)
        {
            PlayerDeath();
        }

         /***Player dies - falls off level respawn boundary ***/
        if (other.gameObject.CompareTag("FallDeath"))
        {
            Debug.Log("FallThreshold met");
            PlayerDeath();
        }

        // When player land on the ground, stop the jumping animation
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            // landed sound effect
            landedSFX.Play();
            sfxPrompt.NewSfxPrompt("Landing");
        }

        // When player land on box fix player to box
        if (other.gameObject.CompareTag("Box"))
        {
            if (other.transform.position.y < this.transform.position.y &&
                other.transform.position.x < this.transform.position.x + 0.6f &&
                other.transform.position.x > this.transform.position.x - 0.6f)
            {
                animator.SetBool("isJumping", false);
                // landed sound effect
                landedSFX.Play();
                sfxPrompt.NewSfxPrompt("Landing");
                //CreateLandingParticles();
                this.gameObject.transform.parent = other.gameObject.transform;
            }
        }

        // When player land on the ground, stop the jumping animation
        if (other.gameObject.CompareTag("Button"))
        {
            animator.SetBool("isJumping", false);
            // landed sound effect
            landedSFX.Play();
            sfxPrompt.NewSfxPrompt("Landing");
            CreateLandingParticles();
        }

        // When player land on the ground, stop the jumping animation
        if (other.gameObject.CompareTag("Door"))
        {
            animator.SetBool("isJumping", false);
            // landed sound effect
            landedSFX.Play();
            sfxPrompt.NewSfxPrompt("Landing");
            CreateLandingParticles();
        }

        // Fix player postion on the movingplatforms
        if (other.gameObject.CompareTag("movingPlatform"))
        {
            animator.SetBool("isJumping", false);
            // landed sound effect
            landedSFX.Play();
            sfxPrompt.NewSfxPrompt("Landing");
            CreateLandingParticles();
            this.gameObject.transform.parent = other.gameObject.transform;
        }
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        // When player land on box fix player to box
        if (other.gameObject.CompareTag("Box"))
        {
            if (other.transform.position.y < this.transform.position.y &&
                other.transform.position.x < this.transform.position.x + 0.6f &&
                other.transform.position.x > this.transform.position.x - 0.6f)
            {
                animator.SetBool("isJumping", false);
                // landed sound effect
                //landedSFX.Play();
                //sfxPrompt.NewSfxPrompt("Landing");
                //CreateLandingParticles();
                this.gameObject.transform.parent = other.gameObject.transform;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LaserTrigger"))
        {
            Debug.Log("Laser Triggered.");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("playerleave: " + other.tag);
        // When player leave moving platform, move freely
        if (other.gameObject.CompareTag("movingPlatform"))
        {
            Debug.Log("leaving platform");
            this.gameObject.transform.parent = null;
        }
        // When player no longer interacting with box
        if (other.gameObject.CompareTag("Box"))
        {
            if (other.transform.position.y < this.transform.position.y &&
                other.transform.position.x < this.transform.position.x + 0.8f &&
                other.transform.position.x > this.transform.position.x - 0.8f)
            {
                Debug.Log("leaving box");

            }
            this.gameObject.transform.parent = null;
        }
    }


    public void OnCollisionExit2D(Collision2D other)
    {
        // When player no longer interacting with box
        if (other.gameObject.CompareTag("Box"))
        {
            if (other.transform.position.y < this.transform.position.y &&
                other.transform.position.x < this.transform.position.x + 0.8f &&
                other.transform.position.x > this.transform.position.x - 0.8f)
            {
                Debug.Log("leaving box");

            }
            this.gameObject.transform.parent = null;
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Dead");
        FlipPlayer();
        DeathSFX.Play();
        Collectible.CollectibleOnDeath = true;
        sfxPrompt.NewSfxPrompt("Death");
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

    public void CreateFeetParticles()
    {
        feetParticles.Play();
    }

    public void CreateRightSideParticles()
    {
        rightSideParticles.Play();
    }

    public void CreateLeftSideParticles()
    {
        leftSideParticles.Play();
    }

    public void CreateLandingParticles()
    {
        landingParticles.Play();
    }

    public int GetPlayerStatus()
    {
        return zone;
    }
}