using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    // music file
    public AudioSource BGM;

    // This will continue to play the audio throughout the scenes
    void Awake(){
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
