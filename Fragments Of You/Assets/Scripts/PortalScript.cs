using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject linkedPortalObject;

    public bool isPortalLocked = false;
    public bool expectPinkExist = false;

    // isBoxBlocking is for VFX only, for other purpose use isPortalLocked
    public bool isBoxBlocking = false;

    [Range (0f, 15f)]
    public float boxPopOutVelocity = 1.5f;
    public ParticleSystem popOutParticle;
    public ParticleSystem ambientParticle;
    private PortalScript linkedPortalScript;
    private SpriteRenderer portalSprite;


    // Start is called before the first frame update
    void Start()
    {
        linkedPortalScript = linkedPortalObject.GetComponent<PortalScript>();
        portalSprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBoxBlocking || linkedPortalScript.isBoxBlocking)
        {
            if(portalSprite.color.a > 0.1f)
            {
                ambientParticle.Stop();
                Color tempColor = portalSprite.color;
                tempColor.a -= 0.01f;
                portalSprite.color = tempColor;
            }
        }
        else
        {
            if(portalSprite.color.a < 1f)
            {
                ambientParticle.Play();
                Color tempColor = portalSprite.color;
                tempColor.a += 0.01f;
                portalSprite.color = tempColor;
            }
        }
        
    }

    private void LateUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!(isBoxBlocking || linkedPortalScript.isBoxBlocking))
        {
            popOutParticle.Play();
        }
  

        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))  && !isPortalLocked)
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

        if (other.gameObject.CompareTag("Box"))
        {
            isBoxBlocking = false;
            linkedPortalScript.isBoxBlocking = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Box"))
        {
            isBoxBlocking = true;
        }
    }
}

