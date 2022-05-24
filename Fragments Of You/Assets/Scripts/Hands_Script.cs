using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands_Script : MonoBehaviour
{
    public bool isHit = false;
    public bool isReset = false;
    public ParticleSystem grappleParticles;
    
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
            Debug.Log("Anchor Collided!");
            isHit = true;
            CreateGrappleParticles();
        }
        
        if(other.tag != "Player" && other.tag != "Anchor" && other.tag != "CameraBound")
        {
            isReset = true;
            Debug.Log("name: " + other.name);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Anchor")
        {
            isHit = false;
            
        }

        if(other.tag != "Player" && other.tag != "Anchor")
        {
            isReset = false;
        }
    }

    public void CreateGrappleParticles()
    {
        grappleParticles.Play();
    }
}
