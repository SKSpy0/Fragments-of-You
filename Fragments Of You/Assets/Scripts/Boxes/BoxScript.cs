using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Vector2 spawnPoint;
    private bool isPickup;
    private bool couldPickup;
    private GameObject[] players;
    private GameObject player;

    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject p in players) {
            player = p;
        }

        rb = GetComponent<Rigidbody2D>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        spawnPoint = this.transform.position;
        isPickup = false;
        couldPickup = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(isPickup) {
                isPickup = false;
                couldPickup = false;
                rb.simulated  = true;

                if(playerSprite.flipX == false)
                {
                    rb.AddForce(Vector2.right * 2f, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
                }
            } else {
                isPickup = true;
            }
            
        }
        if (couldPickup == true && isPickup)
        { 
            Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y + 1f);
            this.transform.position = target;
            rb.simulated  = false;
        }
    }

    public void respawn()
    {
        this.transform.position = spawnPoint;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Enter!");
            couldPickup = true;
        }
        // if interaction is below then do the thing else don't
        if(other.transform.position.y < this.transform.position.y)
        {
            // Fix box postion on the movingplatforms
            if (other.gameObject.CompareTag("movingPlatform"))
            {
                this.gameObject.transform.parent = other.gameObject.transform;
            }
            // Fix box postion on other boxes
            if (other.gameObject.CompareTag("Box"))
            {
                if(other.transform.position.y < this.transform.position.y &&
                other.transform.position.x < this.transform.position.x + 0.6f &&
                other.transform.position.x > this.transform.position.x - 0.6f)
                {
                    this.gameObject.transform.parent = other.gameObject.transform;
                }
            }
            
        }
        
    }

    

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Exist!");
            if(!isPickup)
            {
                couldPickup = false;
            }
        }
        //Debug.Log("BoxLeave: " + other.tag);
        // if interaction is below then do the thing else don't
        // When box leave moving platform, move freely
        if (other.gameObject.CompareTag("movingPlatform"))
        {
            //Debug.Log("Leaving platform");
            this.gameObject.transform.parent = null;
        }
        // When box leave other boxes, move freely
        if (other.gameObject.CompareTag("Box"))
        {
            if(other.transform.position.y > this.transform.position.y)
            {
                //Debug.Log("Leaving box, msg from lower box");
                other.gameObject.transform.parent = null;
                if(other.gameObject.transform.position.x>this.gameObject.transform.position.x)
                {
                    other.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x + 0.1f,other.gameObject.transform.position.y);
                }
                if(other.gameObject.transform.position.x<this.gameObject.transform.position.x)
                {
                    other.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x - 0.1f,other.gameObject.transform.position.y);
                }
            }
            else if(other.transform.position.y < this.transform.position.y)
            {
                //Debug.Log("Leaving box, msg from upper box");
                this.gameObject.transform.parent = null;
                // if(this.gameObject.transform.position.x>other.gameObject.transform.position.x)
                // {
                //     this.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x + 0.82f,this.gameObject.transform.position.y);
                // }
                // if(this.gameObject.transform.position.x<other.gameObject.transform.position.x)
                // {
                //     this.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x - 0.82f,this.gameObject.transform.position.y);
                // }
            }
        }
    }
}
