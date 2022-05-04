using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Trigger : MonoBehaviour
{
    public Laser[] LaserObj;
    bool singleton_check = false;
    bool singleton_check_2 = false;

     public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            singleton_check = false;
            if(!singleton_check){
            
            foreach(Laser laserEffect in LaserObj){
              laserEffect.LaserTimer();
              Debug.Log("LASER Activated!");
            }
             singleton_check = true;
            }
        }

    }
    // Releases all laser's in specfic section
    public void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("Player"))
         {
             foreach(Laser laserEffect in LaserObj){
                 laserEffect.LaserStop();
                 Debug.Log("LASER Deactivated!");
             }
             singleton_check = true; 
         }
    }

}