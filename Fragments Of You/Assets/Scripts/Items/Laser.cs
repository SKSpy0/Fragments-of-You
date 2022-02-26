using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public SpriteRenderer LaserSprite;
    public AudioSource LaserSFX;
    public BoxCollider2D LaserCollider;

    void Start(){

        LaserTimer();

    }

    private void LaserTimer()
    {
        Debug.Log("Laser is on");
        LaserSFX.Play();
        LaserSprite.enabled = true;
        LaserCollider.enabled = true;
        StartCoroutine(TurnOffLaser(9.0f));
    }

    // every 9 seconds turn off laser
    IEnumerator TurnOffLaser(float waitTime)
    {
           yield return new WaitForSeconds(waitTime);
             LaserSprite.enabled = false;
             LaserCollider.enabled = false;
               Debug.Log("Laser is off");
               StartCoroutine(TurnOnLaser(9.0f));       
    }

    // every 9 seconds turn on laser
    IEnumerator TurnOnLaser(float waitTime)
    {
           yield return new WaitForSeconds(waitTime);
            LaserTimer();
    }
}
