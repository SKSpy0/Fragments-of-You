using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject linkedPortalObject;

    public bool isPortalLocked = false;
    public bool expectPinkExist = false;
    [Range (0f, 15f)]
    public float boxPopOutVelocity = 1.5f;

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

    private void LateUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))  && !isPortalLocked)
        {
            isPortalLocked = true;
            linkedPortalScript.isPortalLocked = true;
            expectPinkExist = true;
            other.gameObject.transform.position = linkedPortalObject.transform.position;

            Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();

            if (other.gameObject.CompareTag("Box"))
            {
                if (otherRB.velocity.x >= 0f)
                {
                    otherRB.AddForce(transform.right * boxPopOutVelocity, ForceMode2D.Impulse);
                }
                else
                {
                    otherRB.AddForce(-transform.right * boxPopOutVelocity, ForceMode2D.Impulse);
                }

                otherRB.AddForce(transform.up * boxPopOutVelocity, ForceMode2D.Impulse);
            }   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box")) && !expectPinkExist)
        {
            isPortalLocked = false;
            linkedPortalScript.isPortalLocked = false;

            linkedPortalScript.expectPinkExist = false;
        }
    }
}

