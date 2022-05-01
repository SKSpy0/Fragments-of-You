using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGrapple : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private LineRenderer lr;
    private CapsuleCollider2D handColl;
    private SpringJoint2D armJoint;
    private Animator animator;
    public GameObject handsGameObject;
    //public GameObject firstArm;
    //public GameObject armPrefab;
    public AudioSource grappleSFX;
    public AudioSource anchorhitSFX;
    public Hands_Script handsScript;
    public SFXPrompt sfxPrompt;
    private SpriteRenderer handsSprite;
    private Vector3 targetPos;
    private Quaternion targetRotat;
    private float currentLength;
    public PointClass[] points;
    public StickClass[] sticks;
    [SerializeField] private bool isAnchored = false;
    public bool isFired = false;
    private bool isRelaxed = false;
    public bool generateRope = true;
    

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float grappleJumpForceVertical = 7f;
    //[SerializeField] private float grappleJumpForceHorizontal = 7f;

    [SerializeField] private float gravity = 9.81f;
    [SerializeField] public float numIterationOfRopeSimulation = 10f;
    [SerializeField] public float lengthOfArm = 3f;
    [SerializeField] public float lengthOfJoint = 1.7f;
    private float lengthOfSticks;
    [SerializeField] private float armSpeed = 7f;
    [SerializeField] private float anchorableDis = 14f;
    [SerializeField] private int numberOfLinks = 7;

    private PinkMovement pm;

    public GameObject lockOn;

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
        armJoint = GetComponent<SpringJoint2D>();

        handColl = handsGameObject.GetComponent<CapsuleCollider2D>();
        handsSprite = handsGameObject.GetComponent<SpriteRenderer>();

        // disableanchor
        isAnchored = false;
        armJoint.enabled = false;
        handsSprite.enabled = false;
        lr.enabled = false;

        armJoint.distance = lengthOfJoint;

        GenerateRope();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(!IsGrounded() && IsAnyValidAnchor() && !isAnchored && pm.hasArms() && !isRelaxed && !pm.isFacingWall())
            {
                ShootHands();
            // Sound effect for arm extensions should be here (It's a good idea).
            } 
            else if(handsScript.checkHit())
            {
                isFired = false;
                Debug.Log("Line 96");
            }
        }

        AnchorRadar();
        //Debug.Log(IsGrounded());

        // Hand and Arm Rope Conditionals
        if (!isFired && !isRelaxed) 
        {
            handsGameObject.transform.position = transform.position;
        }
        else if (!isRelaxed)
        {
            handsGameObject.transform.position = Vector2.MoveTowards(handsGameObject.transform.position, targetPos, armSpeed * Time.deltaTime);
        }
        else if (isRelaxed)
        {
            handsGameObject.transform.position = points[0].position;
            targetRotat = GetRotation(transform.position);
            handsGameObject.transform.rotation = targetRotat;
            handsSprite.flipX = true;
        }

        if(handsScript.checkHit() && generateRope)
        {
            Grapple();
            sfxPrompt.NewSfxPrompt("Anchor hit");
            anchorhitSFX.Play();
        } 
        else if (!handsScript.checkHit() && !generateRope && isAnchored)
        {
            ReleaseArms();
            if(!IsGrounded())
            {
                GrappleJump();
            }
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
          //  Debug.Log("Line 150");
        }

        SimulateRope();
        UpdateEndPoint();

        if(IsAnyValidAnchor() && !isAnchored)
        {
            lockOn.SetActive(true);
            GameObject nearbyAnchor;
            nearbyAnchor = FindValidAnchor();
            lockOn.transform.position = nearbyAnchor.transform.position;
            lockOn.transform.Rotate(0f, 0f, 1f, Space.Self);
        }
        else
        {
            lockOn.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if(isRelaxed) {
            if(currentLength > 0.5f) {
                SetArmLength(currentLength - 0.15f);
            }
            else
            {
                isRelaxed = false;
                ArmReset();
                //points[0].locked = true;
            }
            
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
        Debug.Log("Grapple");
        if(generateRope){
            //GenerateRope();
            armJoint.enabled = true;
            SetArmLength(lengthOfJoint);
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
        lr.enabled = true;
        sfxPrompt.NewSfxPrompt("Grapple Fired");
        grappleSFX.Play();
        SetArmLength(lengthOfArm);
        isRelaxed = false;
        //points[0].locked = true;
    }
    public void ReleaseArms()
    {
        Debug.Log("ReleaseArms");
        //DestoryArmByTag();
        handsSprite.enabled = false;
        lr.enabled = false;
        armJoint.enabled = false;
        isAnchored = false;
        isRelaxed = true;
        //points[0].locked = false;
        //SetArmLength(lengthOfArm);
    }

    private void ArmReset()
    {
        Debug.Log("reset");
        //Debug.Log("Line 219");
        generateRope = true;
        isFired = false;
        animator.SetBool("isArmless", false);
        handsSprite.flipX = false;
        handsSprite.enabled = false;
        lr.enabled = false;
    }

    private void GrappleJump()
    {
        rb.AddForce(Vector2.up * grappleJumpForceVertical, ForceMode2D.Impulse);
        // Generate Particle effects
        pm.CreateFeetParticles();

        /*if(rb.velocity.x < 0.1 || rb.velocity.x>-0.1)
        {
            rb.AddForce(Vector2.right * grappleJumpForceHorizontal, ForceMode2D.Impulse);
        }*/
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

        //points[0].locked = true;
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
        currentLength = length;
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
        
        if(!isRelaxed) {
            points[0].position = handsGameObject.transform.position;
        }
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
