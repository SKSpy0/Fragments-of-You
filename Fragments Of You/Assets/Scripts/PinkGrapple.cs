using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGrapple : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private CapsuleCollider2D handColl;
    private Animator animator;
    public GameObject handsGameObject;
    public GameObject firstArm;
    public GameObject armPrefab;
    public bool isAnchored = false;
    private bool isFired = false;
    private Vector3 targetPos;
    private Quaternion targetRotat;
    private float armSpeed = 7f;
    private bool isHit = false;
    public bool generateRope = true;
    public Hands_Script handsScript;
    //

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

        handColl = handsGameObject.GetComponent<CapsuleCollider2D>();

        // disableanchor
        isAnchored = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(!IsGrounded() && IsAnyValidAnchor() && !isAnchored && pm.hasArms())
            {
                ShootHands();
                // Sound effect for arm extensions should be here (It's a good idea).
            } 
            else if(isAnchored)
            {
                ReleaseArms();

            }
        }

        AnchorRadar();

        // Hand
        if (!isFired) 
        {
            handsGameObject.transform.position = transform.position;
            isAnchored = false;
            DestoryArmByTag();
        }
        else
        {
            handsGameObject.transform.position = Vector2.MoveTowards(handsGameObject.transform.position, targetPos, armSpeed * Time.deltaTime);
        }

        isHit = handsScript.checkHit();

        if(isHit && generateRope)
        {
            Grapple();
            generateRope = false;
        }

        if(handsScript.checkReset())
        {
            isFired = false;
        }
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
            //generateRope = false;
        }
        isAnchored = true;
    }

    private void ShootHands()
    {
        targetPos = FindValidAnchor().transform.position;
        targetRotat = GetRotation(targetPos);

        //handsGameObject.GetComponent<Hands_Script>().Fire();
        isFired = true;
        animator.SetBool("isArmless", true);
    }
    private void ReleaseArms()
    {
        isHit = false;
        handsGameObject.transform.position = transform.position;
        DestoryArmByTag();
        generateRope = true;
        isAnchored = false;
        rb.AddForce(Vector2.up * grappleJumpForce, ForceMode2D.Impulse);
        isFired = false;
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
                Debug.DrawLine(transform.position, handsGameObject.transform.position, Color.yellow);
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

    public Quaternion GetRotation(Vector3 position)
    {
        Vector3 difference = position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rotation_z + 0.1f);
    }
}
