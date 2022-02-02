using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGrapple : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator animator;
    public GameObject handsGameObject;
    public GameObject firstArm;
    public GameObject armPrefab;
    private bool isAnchored = false;
    public bool advanceGrapple = true;
    public bool generateRope = true;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float grappleJumpForce = 7f;
    [SerializeField] private float anchorableDis = 14f;
    [SerializeField] private int numberOfLinks = 7;

    private PinkMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        pm = GetComponent<PinkMovement>();

        // disableanchor
        isAnchored = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!advanceGrapple) {

            if (Input.GetButtonDown("Jump"))
            {
                if(!IsGrounded() && IsAnyValidAnchor() && !isAnchored && pm.hasArms())
                {
                    Grapple();
                } else if(isAnchored)
                {
                    GrappleOff();
                    handsGameObject.GetComponent<Hands_Script>().Reset();
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                if(!IsGrounded() && IsAnyValidAnchor() && !isAnchored && pm.hasArms())
                {
                    ShootArms();
                    // Sound effect for arm extensions should be here (It's a good idea).
                } 
                else if(isAnchored)
                {
                    ReleaseArms();
                }
            }
            if (handsGameObject.GetComponent<Hands_Script>().isHit)
            {
                Grapple();
            }
            if (!handsGameObject.GetComponent<Hands_Script>().isFired)
            {
                isAnchored = false;
                DestoryArmByTag();
            }
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
        if(generateRope){
            GenerateRope();
            generateRope = false;
        }
        isAnchored = true;
    }
    // Disengage Grapple
    private void GrappleOff()
    {
        DestoryArmByTag();
        generateRope = true;
        isAnchored = false;
        rb.AddForce(Vector2.up * grappleJumpForce, ForceMode2D.Impulse);
    }
    private void ShootArms()
    {
        Vector3 target = FindValidAnchor().transform.position;
        Quaternion targetRotation = handsGameObject.GetComponent<Hands_Script>().GetRotation(target);
        handsGameObject.GetComponent<Hands_Script>().SetTarget(target);
        handsGameObject.GetComponent<Hands_Script>().SetRotation(targetRotation);
        handsGameObject.GetComponent<Hands_Script>().Fire();
        animator.SetBool("isArmless", true);
    }
    private void ReleaseArms()
    {
        DestoryArmByTag();
        generateRope = true;
        isAnchored = false;
        rb.AddForce(Vector2.up * grappleJumpForce, ForceMode2D.Impulse);
        handsGameObject.GetComponent<Hands_Script>().Reset();
        animator.SetBool("isArmless", false);
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
    
    // for debugging purposes to see valid anchors nearby player
    private void AnchorRadar()
    {
        GameObject an = FindValidAnchor();
        if(IsAnyValidAnchor()) {
            if(isAnchored) {
                if(advanceGrapple) {
                    Debug.DrawLine(transform.position, handsGameObject.transform.position, Color.yellow);
                }
                else
                {
                    Debug.DrawLine(transform.position, an.transform.position, Color.green);
                }
            } else 
            {
                Debug.DrawLine(transform.position, an.transform.position, Color.red);
            }
            
        }
    }

    //generate rope from anchor to player
    void GenerateRope(){
        Rigidbody2D previous_arm = firstArm.GetComponent<Rigidbody2D>();
        for(int i = 0; i < numberOfLinks; i++){
            GameObject arm = Instantiate(armPrefab, transform);
            HingeJoint2D joint = arm.GetComponent<HingeJoint2D>();
            joint.connectedBody = previous_arm;
            
            if(i < numberOfLinks - 1){
                previous_arm = arm.GetComponent<Rigidbody2D>();
            } else {
                HingeJoint2D playerJoint = gameObject.AddComponent<HingeJoint2D>();
                playerJoint.autoConfigureConnectedAnchor = false;
                playerJoint.connectedBody = arm.GetComponent<Rigidbody2D>();
                playerJoint.anchor = Vector2.zero;
                playerJoint.connectedAnchor = new Vector2(0f, -0.3f);
            }
        }
    }

    //destroy rope
    void DestroyRope(){
        for(int i = 0; i < numberOfLinks; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
        Destroy(transform.GetComponent<HingeJoint2D>());
    }

    void DestoryArmByTag()
    {
        GameObject[] arms = GameObject.FindGameObjectsWithTag("Arm");
        foreach(GameObject arm in arms)
            GameObject.Destroy(arm);
    }
}
