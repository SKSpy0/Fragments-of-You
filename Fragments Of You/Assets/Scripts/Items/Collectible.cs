using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Collectible : MonoBehaviour
{
    bool collected;
    public AudioSource collectiblePickup;

    // Start is called before the first frame update
    void Start()
    {
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
         if(collected){
            collectiblePickup.Play();
              Debug.Log("Collectible collected!");
            // destroy's object
            Destroy(this.gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            collected = true;
              // collectible items increased
             TotalCollectibles.collectible += 1;
        }
    }
}
