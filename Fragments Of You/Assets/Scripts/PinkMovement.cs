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
    private Vector2 respawnPoint;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    //private bool hasArms = false;
    private bool hasLegs = true;

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
        respawnPoint = this.transform.position;
    }


    private void Update()
    {
        // basic left/right movement
        // player should always have this
        Move();
        FlipPlayer();
        
        // start of abilities that can be lost
        if (Input.GetButtonDown("Jump"))
        {
            if(IsGrounded() && hasLegs)
            {
                Jump();
            }
        }

        // if player falls off level respawn
        if(this.transform.position.y < -8)
        {
            respawnPlayer();
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
        rb.AddForce(Vector2.right * dirX * moveSpeed, ForceMode2D.Force);
        // Play walk Animation
        animator.SetFloat("Speed", Mathf.Abs(dirX));
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

    // Respawn functions start --------------------------------------------------------------------
    // sets repawnpoint taking vector 2 as input
    // vector 3 can be converted to vector 2 implicitly
    // Note: not set by default.
    public void setRespawn(Vector2 newPoint)
    {
        respawnPoint = newPoint;
    }

    // gets repawnpoint returning vector 2
    public Vector2 getRespawnPoint()
    {
        return respawnPoint;
    }

    // teleports player back to respawnpoint and corrects any orientation
    public void respawnPlayer()
    {
        this.transform.SetPositionAndRotation(respawnPoint,new Quaternion(0,0,0,0));
    }
    // Respawn functions end ----------------------------------------------------------------------

    // State functions start ----------------------------------------------------------------------
    // function to lose arms
    public void loseArms()
    {
        //hasArms = false;
        // preform sprite switch here

    }

    // function to lose legs
    public void loseLegs()
    {
        hasLegs = false;
        // preform sprite switch here

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

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            respawnPlayer();
        }
    }
}