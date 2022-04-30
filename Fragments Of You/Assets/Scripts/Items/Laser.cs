using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer LaserBeam;
    public SpriteRenderer LaserSprite;
    public AudioSource LaserSFX;
    public BoxCollider2D LaserCollider;
    public float TimerInSeconds;
    public ParticleSystem StartParticles;
    public ParticleSystem EndParticles;
    public ParticleSystem StartBeam;
    public ParticleSystem EndBeam;
    bool EndTimer = false;
   

    private float startingVolume = 1f;

    void Start(){
        // Starting volume for LaserSFX
        var startEm = StartParticles.emission;
        var endEm = EndParticles.emission;
        var startBeam = StartBeam.emission;
        var endBeam = EndBeam.emission;

        startEm.enabled = false;
        endEm.enabled = false;
        startBeam.enabled = false;
        endBeam.enabled = false;

        LaserSFX.volume = startingVolume;
    }

     public void LaserTimer()
    {
     //  Debug.Log("Laser is on");
        LaserSFX.Play();

        var startEm = StartParticles.emission;
        var endEm = EndParticles.emission;
        var startBeam = StartBeam.emission;
        var endBeam = EndBeam.emission;

        startEm.enabled = true;
        endEm.enabled = true;
        startBeam.enabled = true;
        endBeam.enabled = true;

        LaserBeam.enabled = true;
        LaserSprite.enabled = true;
        LaserCollider.enabled = true;
        StartCoroutine(TurnOffLaser(TimerInSeconds));
    }

    public void LaserStop()
    {
        var startEm = StartParticles.emission;
        var endEm = EndParticles.emission;
        var startBeam = StartBeam.emission;
        var endBeam = EndBeam.emission;

        startEm.enabled = false;
        endEm.enabled = false;
        startBeam.enabled = false;
        endBeam.enabled = false;

        LaserBeam.enabled = false;
        LaserSprite.enabled = false;
        LaserCollider.enabled = false;
        EndTimer = true;
    }

    // every certain amount of seconds turn off laser
    IEnumerator TurnOffLaser(float waitTime)
    {
        waitTime = TimerInSeconds;
        yield return new WaitForSeconds(waitTime);
        var startEm = StartParticles.emission;
        var endEm = EndParticles.emission;
        var startBeam = StartBeam.emission;
        var endBeam = EndBeam.emission;

        startEm.enabled = false;
        endEm.enabled = false;
        startBeam.enabled = false;
        endBeam.enabled = false;

        LaserBeam.enabled = false;
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