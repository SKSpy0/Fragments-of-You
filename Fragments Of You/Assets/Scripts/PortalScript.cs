using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject linkedPortalObject;



    public bool isPinkUsingPortal = false;

    public bool isReadyToRecivePink = false;

    private PortalScript linkedPortalScript;


    // Start is called before the first frame update
    void Start()
    {
        linkedPortalScript = linkedPortalObject.GetComponent<PortalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if(other.gameObject.CompareTag("Box"))
        {
            other.gameObject.transform.position = linkedPortalObject.transform.position + new Vector3(2f, 1.5f, 0);
        }
        */

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.name + " entered " + this.name);

            if (!isPinkUsingPortal && linkedPortalScript.GetPinkStatus())
            {
                Debug.Log(this.name + " isPinkUsingPortal: " + isPinkUsingPortal);
                isReadyToRecivePink = false;
            }
            else
            {
                isPinkUsingPortal = true;
                linkedPortalScript.SetPinkReciveStatus(true);
                other.gameObject.transform.position = linkedPortalObject.transform.position;
            }

            if (Input.GetButtonDown("Jump"))
            {

            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name + " existed " + this.name);

        if (other.gameObject.CompareTag("Player") && !GetPinkReciveStatus())
        {
            isPinkUsingPortal = false;
            linkedPortalScript.SetPinkStatus(false);
        }
    }


    public bool GetPinkStatus()
    {
        return isPinkUsingPortal;
    }

    public void SetPinkStatus(bool status)
    {
        isPinkUsingPortal = status;
    }

    public bool GetPinkReciveStatus()
    {
        return isReadyToRecivePink;
    }

    public void SetPinkReciveStatus(bool status)
    {
        isReadyToRecivePink = status;
    }


    /*

    private void OnTriggerStay2D(Collider2D other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log(this.name + "  enter trigger");
            
            if(!isPinkUsingPortal && linkedPortalScript.GetPinkStatus())
            {
                Debug.Log(this.name + " isPinkUsingPortal: " + isPinkUsingPortal);
                isReadyToRecivePink = false;
            }
            else
            {
                isPinkUsingPortal = true;
                linkedPortalScript.SetPinkReciveStatus(true);
                other.gameObject.transform.position = linkedPortalObject.transform.position;
            }

            if (Input.GetButtonDown("Jump"))
            {
                
            }
        }
    }

    */
}