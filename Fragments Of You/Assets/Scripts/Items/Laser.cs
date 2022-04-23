using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public SpriteRenderer LaserSprite;
    public AudioSource LaserSFX;
    public BoxCollider2D LaserCollider;
    public float TimerInSeconds;
    bool EndTimer = false;
   

    private float startingVolume = 1f;

    void Start(){
        // Starting volume for LaserSFX
        LaserSFX.volume = startingVolume;
    }

     public void LaserTimer()
    {
     //  Debug.Log("Laser is on");
        LaserSFX.Play();
        LaserSprite.enabled = true;
        LaserCollider.enabled = true;
        StartCoroutine(TurnOffLaser(TimerInSeconds));
    }

    public void LaserStop()
    {
        LaserSprite.enabled = false;
        LaserCollider.enabled = false;
        EndTimer = true;
    }

    // every certain amount of seconds turn off laser
    IEnumerator TurnOffLaser(float waitTime)
    {
        waitTime = TimerInSeconds;
           yield return new WaitForSeconds(waitTime);
             LaserSprite.enabled = false;
             LaserCollider.enabled = false;
           // Debug.Log("Laser is off");
           LaserSFX.Stop();
               StartCoroutine(TurnOnLaser(waitTime));       
    }

    // every certain amount of seconds turn on laser
    IEnumerator TurnOnLaser(float waitTime)
    {
           waitTime = TimerInSeconds;

           if(EndTimer){
               yield break;
           }
           else{
              yield return new WaitForSeconds(waitTime);
              LaserTimer();
           }
    }

}