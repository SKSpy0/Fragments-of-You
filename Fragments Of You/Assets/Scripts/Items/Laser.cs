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
        Debug.Log("Laser warmed");
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
        Debug.Log("LaserTimer()");
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
        StartCoroutine(ChargeLaser(ChargeInSeconds));
    }

    public void LaserStop()
    {
        Debug.Log("LaserStop()");
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
        Debug.Log("ChargeLaser()");
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

        Debug.Log("Laser is off");
        StartCoroutine(TurnOffLaser(TimerInSeconds));
    }

    // every certain amount of seconds turn off laser
    IEnumerator TurnOffLaser(float waitTime)
    {
        Debug.Log("TurnOffLaser()");
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

        Debug.Log("Laser is off");
        LaserSFX.Stop();
        StartCoroutine(TurnOnLaser(waitTime));       
    }

    // every certain amount of seconds turn on laser
    IEnumerator TurnOnLaser(float waitTime)
    {
           Debug.Log("TurnOnLaser()");
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