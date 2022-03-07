using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : RecieverScript
{
    // make sure it inherits from reciever script
    public Transform closePos, openPos;
    private Vector2 destination;
    public AudioSource DoorOpeningSFX;

    public override void Start()
    {
        searchForSignal();
        destination = closePos.position;
    }

    public override void Update()
    {
        transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime);
    }

    public override void press()
    {
        Debug.Log("Open");
        open();
    }

    public override void release()
    {
        Debug.Log("Close");
        close();
    }    

    void open()
    {
        destination = openPos.position;
        // Door opening sound effect plays when door is opened
        DoorOpeningSFX.Play();
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    void close()
    {
        destination = closePos.position;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

}
