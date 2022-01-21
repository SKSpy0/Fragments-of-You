using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMovement : MonoBehaviour
{
    public float speed = 10;
    public float jumpAmount = 10;
    public Rigidbody2D rb;

    private Vector2 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Chracter move left and right using arrow keys
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Jump when space is pressed
        if (Input.GetButton("Jump"))
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }
    }

    public void setRespawn(Vector2 newPoint)
    {
        respawnPoint = newPoint;
    }

    public Vector2 getRespawnPoint()
    {
        return respawnPoint;
    }

    public void respawnPlayer()
    {
        this.transform.SetPositionAndRotation(respawnPoint,new Quaternion(0,0,0,0));
    }
}

