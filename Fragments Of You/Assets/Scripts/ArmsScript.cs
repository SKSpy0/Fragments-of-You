using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsScript : MonoBehaviour
{
    public Vector2 target;
    public Vector2 home;
    public Quaternion rotation;
    public bool isFired = false;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameObject bh;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        home = transform.localPosition;
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
        if (isFired)
        {
            MoveToTarget();
            Rotate();
        } else {
            SetRotation(GetRotation(GetMousePosition()));
            Rotate();
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
        transform.position = Vector2.MoveTowards(transform.position, bh.transform.position, moveSpeed * Time.deltaTime);
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
    public void Rotate()
    {
        transform.rotation = rotation;
    }
    public void Reset()
    {
        isFired = false;
        transform.localPosition = home;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Anchor")
        {
            Debug.Log("Anchor attached");
            Reset();
        }
        else if(other.tag != "Player")
        {
             Debug.Log("Reset needed");
             Reset();
        }
    }
}
