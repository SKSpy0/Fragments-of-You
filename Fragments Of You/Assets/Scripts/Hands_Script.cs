using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands_Script : MonoBehaviour
{
    /*
    public Vector2 target;
    public Vector2 lockedPos;
    public Quaternion rotation;
    public GameObject player;
    public float armLength = 14;
    public bool isFired = false;
    [SerializeField] private float armSpeed = 7f;
    [SerializeField] private GameObject bullet_head;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public AudioSource grappleSFX;
    public AudioSource anchorhitSFX;
    */
    public bool isHit = false;
    public bool isReset = false;

    // Start is called before the first frame update
    /*
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isFired && !isHit)
        {
            MoveToTarget();
            Rotate();
        } else if(!isHit)
        {
            SetRotation(GetRotation(GetMousePosition()));
            Rotate();
        }

        if(isHit && isFired)
        {
            LockPos(lockedPos);
            SetRotation(GetRotation(player.transform.position));
            Rotate();
            sprite.flipX = true;
        }
        
        if(!(isFired || isHit))
        {
            transform.position = player.transform.position;
        }

        if(!isInRange())
        {
            Reset();
        }
        
    }
    */

    /*
    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetTarget(Vector2 position)
    {
        target = position;
    }
    public void MoveToTarget() 
    {
        transform.position = Vector2.MoveTowards(transform.position, bullet_head.transform.position, armSpeed * Time.deltaTime);
    }
    public void Fire()
    {
        isFired = true;
        sprite.enabled = true;
        // Sound effect for grapple.
         grappleSFX.Play();
    }
    public Quaternion GetRotation(Vector3 position)
    {
        Vector3 difference = position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rotation_z + 0.1f);
    }
    public void SetRotation(Quaternion r)
    {
        rotation = r;
    }
    public void SetOppositeRotation(Quaternion r)
    {
        rotation = Quaternion.Inverse(r);
    }
    public void Rotate()
    {
        transform.rotation = rotation;
    }
    public void Reset()
    {
        sprite.enabled = false;
        isFired = false;
        transform.position = player.transform.position;
        isHit = false;
        sprite.flipX = false;
    }
    public void Hit()
    {
        if(!isHit)
        {
            lockedPos = transform.position;
        }
        isHit = true;
        // Sound effect for anchor-hit with grapple.
         anchorhitSFX.Play();
    }
    public void LockPos(Vector2 lp)
    {
        transform.position = lp;
    }

    public bool isInRange()
    {
        Vector2 diff = player.transform.position - transform.position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance < armLength)
        {
            return true;
        }
        return false;
    }
    */
    
    public bool checkHit()
    {
        return isHit;
    }

    public bool checkReset()
    {
        return isReset;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Anchor")
        {
            isHit = true;
            //Hit();
        }
        else 
        {
            isHit = false;
        }
        
        if(other.tag != "Player" && other.tag != "Anchor")
        {
            isReset = true;
        }
        else
        {
            isReset = false;
        }
    }
}
