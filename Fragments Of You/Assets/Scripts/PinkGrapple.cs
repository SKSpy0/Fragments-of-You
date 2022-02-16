using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGrapple : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private LineRenderer lr;
    private CapsuleCollider2D handColl;
    private Animator animator;
    public GameObject handsGameObject;
    //public GameObject firstArm;
    //public GameObject armPrefab;
    public AudioSource grappleSFX;
    public AudioSource anchorhitSFX;
    public Hands_Script handsScript;
    private SpriteRenderer handsSprite;
    private Vector3 targetPos;
    private Quaternion targetRotat;
    public PointClass[] points;
    public StickClass[] sticks;
    public GameObject pointObject;
    [SerializeField] private bool isAnchored = false;
    [SerializeField] private bool isFired = false;
    [SerializeField] private bool generateRope = true;
    

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float grappleJumpForce = 7f;

    [SerializeField] private float gravity = 9.81f;
    [SerializeField] public float numIterationOfRopeSimulation = 10f;
    [SerializeField] public float lengthOfArm = 3f;
    private float lengthOfSticks;
    [SerializeField] private float armSpeed = 7f;
    [SerializeField] private float anchorableDis = 14f;
    [SerializeField] private int numberOfLinks = 7;

    private PinkMovement pm;

    //points between segments of rope
    public class PointClass {
        public Vector2 position, prevPosition;
        public bool locked;
    }

    //connections between points
    public class StickClass {
        public PointClass pointA, pointB;
        public float length;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        pm = GetComponent<PinkMovement>();
        lr = GetComponent<LineRenderer>();

        handColl = handsGameObject.GetComponent<CapsuleCollider2D>();
        handsSprite = handsGameObject.GetComponent<SpriteRenderer>();

        // disableanchor
        isAnchored = false;

        handsSprite.enabled = false;

        GenerateRope();

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
            else if(handsScript.checkHit())
            {
                isFired = false;
            }
        }

        AnchorRadar();

        // Hand and Arm Rope Conditionals
        if (!isFired) 
        {
            handsGameObject.transform.position = transform.position;
        }
        else
        {
            handsGameObject.transform.position = Vector2.MoveTowards(handsGameObject.transform.position, targetPos, armSpeed * Time.deltaTime);
        }

        if(handsScript.checkHit() && generateRope)
        {
            Grapple();
            anchorhitSFX.Play();
        } else if (!handsScript.checkHit() && !generateRope && isAnchored)
        {
            ReleaseArms();
        }

        if(isFired && !isAnchored)
        {
            handsGameObject.transform.rotation = targetRotat;
        }

        if(isAnchored)
        {
            targetRotat = GetRotation(transform.position);
            handsGameObject.transform.rotation = targetRotat;
            handsSprite.flipX = true;
            //Swing();
        }

        if(handsScript.checkReset())
        {
            isFired = false;
        }

        SimulateRope();
        UpdateEndPoint();
    }

    // Ground Check
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    // Grapple
    private void Grapple()
    {
        Debug.Log("Grapple");
        if(generateRope){
            //GenerateRope();
            generateRope = false;
        }
        isAnchored = true;
    }

    private void ShootHands()
    {
        targetPos = FindValidAnchor().transform.position;
        targetRotat = GetRotation(targetPos);
        isFired = true;
        animator.SetBool("isArmless", true);
        handsSprite.enabled = true;
        grappleSFX.Play();
        SetArmLength(lengthOfArm);
    }
    private void ReleaseArms()
    {
        Debug.Log("ReleaseArms");
        //DestoryArmByTag();
        rb.AddForce(Vector2.up * grappleJumpForce, ForceMode2D.Impulse);
        generateRope = true;
        isAnchored = false;
        isFired = false;
        animator.SetBool("isArmless", false);
        handsSprite.flipX = false;
        handsSprite.enabled = false;
        SetArmLength(0.5f);
    }

    private void GrappleJump()
    {
        rb.AddForce(Vector2.up * grappleJumpForce, ForceMode2D.Impulse);
        if(rb.velocity.x < 0.1 || rb.velocity.x>-0.1)
        {
            rb.AddForce(Vector2.right * rb.velocity.x, ForceMode2D.Impulse);
        }
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
        //create a new set of arrays for points and sticks
        points = new PointClass[numberOfLinks];
        sticks = new StickClass[numberOfLinks - 1];

        

        // Init points and sticks
        for(int i = 0; i < numberOfLinks; i++)
        {
            var p = new PointClass();
            p.locked = false;
            points[i] = p;
        }

        for(int i = 0; i < numberOfLinks-1; i++)
        {
            var s = new StickClass();
            //s.length = lengthOfSticks;
            sticks[i] = s;
        }

        SetArmLength(0.5f);

        //assign the 2 locked points (player and hand positions)
        foreach(PointClass p in points) 
        {
            p.position = new Vector2(handsGameObject.transform.position.x, handsGameObject.transform.position.y);
        }

        points[0].locked = true;
        points[numberOfLinks - 1].locked = true;  
        points[numberOfLinks - 1].position = transform.position;
              
        //assign points to the stick array
        for(int i = 0; i < numberOfLinks - 1; i++){
            sticks[i].pointA = points[i];
            sticks[i].pointB = points[i + 1];
        }

        SetUpRope();
    }

    void SetArmLength(float length)
    {
        lengthOfSticks = length / numberOfLinks;
        foreach(StickClass s in sticks)
        {
            s.length = lengthOfSticks;
        }
    }
    
    //this will simulate the rope physics
    void SimulateRope(){
        // each point will be moving to the position it needs to go to to keep the rope intact
        foreach(PointClass p in points){
            if(!p.locked){
                Vector2 positionBeforeUpdate = p.position;
                p.position += p.position - p.prevPosition;
                p.position += Vector2.down * gravity * Time.deltaTime * Time.deltaTime;
                p.prevPosition = positionBeforeUpdate;
            }
        }

        for(int i = 0; i < numIterationOfRopeSimulation; i++){
            foreach(StickClass stick in sticks){
                Vector2 stickCenter = (stick.pointA.position + stick.pointB.position) / 2;
                Vector2 stickDir = (stick.pointA.position - stick.pointB.position).normalized;
                if(!stick.pointA.locked)
                    stick.pointA.position = stickCenter + stickDir * stick.length / 2;
                if(!stick.pointB.locked)
                    stick.pointB.position = stickCenter - stickDir  * stick.length / 2;
            }
        }

        RenderRope();
    }

    void UpdateEndPoint()
    {
        points[0].position = handsGameObject.transform.position;
        points[numberOfLinks - 1].position = transform.position;
    }
    void SetUpRope()
    {
        lr.positionCount = numberOfLinks;
    }
    void RenderRope()
    {
        for(int i = 0; i < numberOfLinks; i++){
            lr.SetPosition(i, points[i].position);
        }
    }

    //destroy rope
    void DestoryArmByTag()
    {
        GameObject[] arms = GameObject.FindGameObjectsWithTag("Arm");
        foreach(GameObject arm in arms)
            GameObject.Destroy(arm);
    }

    public Quaternion GetRotation(Vector3 position)
    {
        Vector3 difference = position - handsGameObject.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rotation_z + 0.1f);
    }

    public bool getAnchored()
    {
        return isAnchored;
    }
}
