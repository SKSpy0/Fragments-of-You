using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGrapple : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpringJoint2D arm;
    private Animator animator;

    private bool isAnchored = false;
    public bool hasArms = true;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float grappleJumpForce = 7f;
    [SerializeField] private float anchorableDis = 14f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        arm = GetComponent<SpringJoint2D>();
        animator = GetComponent<Animator>();

        // disable arm and anchor
        arm.enabled = false;
        isAnchored = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(!IsGrounded() && IsAnyValidAnchor() && !isAnchored && hasArms)
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

    // Ground Check
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    // Grapple
    private void Grapple()
    {
        arm.enabled = true;
        isAnchored = true;
        animator.SetBool("isJumping", true);
    }
    // Disengage Grapple
    private void GrappleOff()
    {
        arm.enabled = false;
        isAnchored = false;
        rb.AddForce(Vector2.up * grappleJumpForce, ForceMode2D.Impulse);
    }
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
