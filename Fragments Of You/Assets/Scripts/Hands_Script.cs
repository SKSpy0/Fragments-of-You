using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands_Script : MonoBehaviour
{
    public Vector2 target;
    public Vector2 lockedPos;
    public Quaternion rotation;
    public GameObject player;
    public float armLength = 14;
    public bool isFired = false;
    public bool isHit = false;
    [SerializeField] private float armSpeed = 7f;
    [SerializeField] private GameObject bullet_head;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Fire1"))
        {
            if(!isFired)
            {
                SetTarget(GetMousePosition());
                SetRotation(GetRotation(target));
                Fire();
            }
            else
            {
                Reset();
            }
            
        }
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Anchor")
        {
            Debug.Log("Anchor attached");
            Hit();
        }
        else if(other.tag != "Player")
        {
             Debug.Log("Reset needed");
             Reset();
        }
    }
}
