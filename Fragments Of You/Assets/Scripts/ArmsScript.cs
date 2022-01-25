using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsScript : MonoBehaviour
{
    private Vector2 target;
    private Quaternion rotation;
    private bool isFired = false;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameObject bh;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Fire1") & !isFired)
        {
            SetTarget(GetMousePosition());
            SetRotation(GetRotation(target));
            Fire();
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

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SetTarget(Vector2 position)
    {
        target = position;
    }
    private void MoveToTarget() 
    {
        transform.position = Vector2.MoveTowards(transform.position, bh.transform.position, moveSpeed * Time.deltaTime);
    }
    private void Fire()
    {
        isFired = true;
    }
    private Quaternion GetRotation(Vector3 position)
    {
        Vector3 difference = position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rotation_z + 0.1f);
    }
    private void SetRotation(Quaternion r)
    {
        rotation = r;
    }
    private void Rotate()
    {
        transform.rotation = rotation;
    }
}
