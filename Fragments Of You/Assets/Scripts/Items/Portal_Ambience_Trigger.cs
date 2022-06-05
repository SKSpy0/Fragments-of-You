using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Ambience_Trigger : MonoBehaviour
{
    public PortalScript[] PortalObj;
    bool singleton_check;

    void Start()
    {
        singleton_check = false;
    }


     public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!singleton_check){
            
            foreach(PortalScript portalEffect in PortalObj){
              portalEffect.Portal_Ambience();
              Debug.Log("Portal Ambience effect activated!");
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
            foreach(PortalScript portalEffect in PortalObj){
              portalEffect.Portal_Ambience_Fade();
              Debug.Log("Portal Ambience effect deactivated!");
            }
             singleton_check = true; 
         }
    }

}