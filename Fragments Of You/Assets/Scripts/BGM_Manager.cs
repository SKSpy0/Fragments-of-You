using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    // music file
    public AudioSource BGM;
    public static BGM_Manager instance = null;

    // This will continue to play the audio throughout the scenes
    void Awake(){
         // Singleton checks to not let music play more than once.
        if(instance == null){
           instance = this;
           }
        else if (instance != this){
         Debug.Log("Level 1 music destroyed");
            Destroy(transform.gameObject);
        }
    
      DontDestroyOnLoad(transform.gameObject);
    }

    // This will be used to change audio between levels 1, 2, and 3.
    public void ChangeBGM(AudioClip music){
        // continues music where it was left off.
        if(BGM.clip.name == music.name) {
               return;
            }

        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }
}
