using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class Collectible : MonoBehaviour
{

    public static event Action CollectiblePickedUp = delegate {};

    public SpriteRenderer CollectibleSprite;
    public CircleCollider2D CollectibleCollider;
    public AudioSource collectiblePickup;
    public AudioSource collectibleCaptured;
    private IEnumerator coroutine;
    public static bool CollectibleOnDeath;
    bool collected;

   

    // Start is called before the first frame update
    void Start()
    {
        // five second timer for collectible to be collected
        coroutine = CollectibleCollected(5.0f);
        collected = false;
        CollectibleOnDeath = false;

    }

    IEnumerator CollectibleCollected(float waitTime)
    {
        // waits five seconds and then play font effect
          yield return new WaitForSeconds(waitTime);

        /* If player dies within five seconds, reset collectibles position
        * "CollectibleOnDeath" boolean in PinkMovement.cs will 
        * change to true if player dies.
        */
           if(CollectibleOnDeath){
            CollectibleSprite.enabled = true;
            CollectibleCollider.enabled = true;
            TotalCollectibles.collectiblesCollected -= 1;
            // reset coroutine to five seconds
            coroutine = CollectibleCollected(5.0f);
            Debug.Log("Resetting Collectible.");
         }
           else{
            // Else, play captured effect and destroy the object   
            Debug.Log("The player survived for five seconds.");
            collectibleCaptured.Play();
             // wait 1 second and then destroy the object
            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
           }
            yield break;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){

            Debug.Log("Collectible collected!");
            CollectibleSprite.enabled = false;
            CollectibleCollider.enabled = false;
            // always set the boolean to false when player collides with collectible
            CollectibleOnDeath = false;
            // Start collectible timer    
            StartCoroutine(coroutine);
            // collectible items increased and sound effect plays
            collectiblePickup.Play();
            CollectiblePickedUp();
        }
    }
}
