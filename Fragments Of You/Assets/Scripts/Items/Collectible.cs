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

    // Update is called once per frame
    void Update()
    {
         if(collected){
            Debug.Log("Collectible collected!");
            CollectibleSprite.enabled = false;
            CollectibleCollider.enabled = false;
            collected = false;
        }
        
    }

    // every certain amount of seconds turn off laser
    IEnumerator CollectibleCollected(float waitTime)
    {
        // waits three seconds and then font effect plays
         yield return new WaitForSeconds(waitTime);
           if(CollectibleOnDeath){
            CollectibleSprite.enabled = true;
            CollectibleCollider.enabled = true;
            TotalCollectibles.collectiblesCollected -= 1;
            Debug.Log("Resetting Collectible.");
            CollectibleOnDeath = false; 
         }
           else{
            Debug.Log("The player survived for five seconds.");
            // destroy's object
            Destroy(this.gameObject);
           }
            CollectibleOnDeath = false; 
            coroutine = CollectibleCollected(5.0f);
            yield break;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            collected = true;
            StartCoroutine(coroutine);
            // collectible items increased and sound effect plays
            collectiblePickup.Play();
            CollectiblePickedUp();
        }
    }
}
