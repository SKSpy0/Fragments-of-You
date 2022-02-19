using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Vector2 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respawn()
    {
        this.transform.position = spawnPoint;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
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
