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

    private Transform target;
    private Vector2 originalPosition;
    private float collectibleSpeed;

   

    // Start is called before the first frame update
    void Start()
    {
        // five second timer for collectible to be collected
        coroutine = CollectibleCollected(5.0f);
        collected = false;
        CollectibleOnDeath = false;
        // get original position of collectible and store it to reset collectible
        originalPosition = this.transform.position;
        // private Transform originalPosition = this.gameObject.GetComponent<Transform>();
        // set target to player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        collectibleSpeed = 5f;
    }

    void Update(){
       if(Vector2.Distance(transform.position, target.position) > 1.5 && collected){
           transform.position = Vector2.MoveTowards(transform.position, target.position, collectibleSpeed * Time.deltaTime);
       }
       else if(!collected){
         this.transform.position = originalPosition;
       }
    }

    IEnumerator CollectibleCollected(float waitTime)
    {
       
          // waits five seconds and then play font effect
          yield return new WaitForSeconds(waitTime);
        /*  
        * If player dies within five seconds, reset collectibles 
        * position "CollectibleOnDeath" boolean in PinkMovement.cs will 
        * change to true if player dies. 
        */
           if(CollectibleOnDeath){
            collected = false;
            // boolean check
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
             // wait 2.2 seconds and then destroy the object
            yield return new WaitForSeconds(2.2f);
            Destroy(this.gameObject);
           }
            yield break;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            // boolean check
            collected = true;
            Debug.Log("Collectible collected!");
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
