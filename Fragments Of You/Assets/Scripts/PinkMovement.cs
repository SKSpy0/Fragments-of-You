using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private SpringJoint2D arm;

    [SerializeField] private LayerMask jumpableGround;

    private Vector2 respawnPoint;

    private float dirX = 0f;
    private bool isAnchored = false;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float grappleJumpForce = 7f;
    [SerializeField] private float anchorableDis = 14f;
    private void Start()
    {
        // get component
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        arm = GetComponent<SpringJoint2D>();

        // freeze rotation
        rb.freezeRotation = true;

        arm.enabled = false;
        isAnchored = false;
    }


    private void Update()
    {
        Move();
        FlipPlayer();

        if (Input.GetButtonDown("Jump"))
        {
            if(IsGrounded())
            {
                Jump();
            }
            else if(IsAnyValidAnchor() && !isAnchored)
            {
                Grapple();
            } else if(isAnchored)
            {
                GrappleOff();
            }
        }

        if(IsAnyValidAnchor())
        {
            arm.connectedBody = FindValidAnchor().GetComponent<Rigidbody2D>();
        }
        AnchorRadar();
    }
    
    private void FixedUpdate() 
    {
        
    }

    private void Move() 
    {
        // get input
        dirX = Input.GetAxisRaw("Horizontal");
        // modify velocity based on input
        rb.AddForce(Vector2.right * dirX * moveSpeed, ForceMode2D.Force);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Grapple
    private void Grapple() {
        arm.enabled = true;
        isAnchored = true;
    }

    // Disengage Grapple
    private void GrappleOff() {
        arm.enabled = false;
        isAnchored = false;
        rb.AddForce(Vector2.up * grappleJumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

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

    private void FlipPlayer()
    {
        if (rb.velocity.x < -0.1f)
        {
            sprite.flipX = true;
        } else if (rb.velocity.x > 0.1f) {
            sprite.flipX = false;
        }
    }
    
    // Find Valid Anchor within anchorableDis
    private GameObject FindValidAnchor() 
    {
        GameObject[] anchors;
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
        GameObject valid = null;
        float distance = anchorableDis;
        Vector3 position = transform.position;
        foreach (GameObject an in anchors)
        {
            Vector3 diff = an.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                valid = an;
                distance = curDistance;
            }
        }
        return valid;
    }
    // Return true if an anchor within range
    private bool IsAnyValidAnchor() 
    {
        GameObject[] anchors;
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
        float distance = anchorableDis;
        Vector3 position = transform.position;
        foreach (GameObject an in anchors)
        {
            Vector3 diff = an.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                return true;
            }
        }
        return false;
    }
    
    private void AnchorRadar()
    {
        GameObject an = FindValidAnchor();
        if(IsAnyValidAnchor()) {
            if(isAnchored) {
                Debug.DrawLine(transform.position, an.transform.position, Color.green);
            } else 
            {
                Debug.DrawLine(transform.position, an.transform.position, Color.red);
            }
            
        }
    }
}