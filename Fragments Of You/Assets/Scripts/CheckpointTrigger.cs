using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class CheckpointTrigger : MonoBehaviour
{
    [Header("Custom Event")]
    public UnityEvent myEvents;
    bool oncePlay = false;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myEvents == null)
        {
            print("null");
        }
        else
        {
            print("trigger");
            if (!oncePlay)
            {
                myEvents.Invoke();
                audioSource.PlayDelayed(0.4f);
                oncePlay = true;
            }
        }
    }
}
