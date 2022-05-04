using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer LaserBeam;
    public AudioSource LaserSFX;
    public BoxCollider2D LaserCollider;
    public float TimerInSeconds;
    public float ChargeInSeconds = 1;
    public ParticleSystem StartParticles;
    public ParticleSystem EndParticles;
    public ParticleSystem StartBeam;
    public ParticleSystem EndBeam;
    bool EndTimer = false;
   

    private float startingVolume = 1f;

    void Start(){

       // Start beam effects
        var startEm = StartParticles.emission;
        var endEm = EndParticles.emission;
        var startBeam = StartBeam.emission;
        var endBeam = EndBeam.emission;

        startEm.enabled = false;
        endEm.enabled = false;
        startBeam.enabled = false;
        endBeam.enabled = false;
        // Starting volume for LaserSFX
         LaserSFX.volume = startingVolume;
    }

     public void LaserTimer()
    {
        LaserSFX.Play();

        var startEm = StartParticles.emission;
        var endEm = EndParticles.emission;
        var startBeam = StartBeam.emission;
        var endBeam = EndBeam.emission;

        startEm.enabled = true;
        endEm.enabled = false;
        startBeam.enabled = true;
        endBeam.enabled = false;

        LaserBeam.enabled = false;
        LaserCollider.enabled = false;
        EndTimer = false;
        StartCoroutine(ChargeLaser(ChargeInSeconds));
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
        LaserCollider.enabled = false;
        EndTimer = true;
    }

    IEnumerator ChargeLaser(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
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
        LaserCollider.enabled = true;

        StartCoroutine(TurnOffLaser(TimerInSeconds));
    }

    // every certain amount of seconds turn off laser
    IEnumerator TurnOffLaser(float waitTime)
    {
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
        LaserCollider.enabled = false;

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
              LaserTimer();
              yield return new WaitForSeconds(waitTime);
           }
    }

}