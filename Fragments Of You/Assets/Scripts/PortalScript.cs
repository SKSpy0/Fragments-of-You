using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PortalScript : MonoBehaviour
{
    public GameObject linkedPortalObject;

    public bool isPortalLocked = false;
    public bool expectPinkExist = false;

    // isBoxBlocking is for VFX only, for other purpose use isPortalLocked
    public bool isBoxBlocking = false;

    [Range (0f, 15f)]
    public float boxPopOutVelocity = 1.5f;
    public float teleportSpeed = 0.05f;
    public float fadeSpeed = 0.02f;

    public ParticleSystem popOutParticle;
    public ParticleSystem ambientParticle;
    private PortalScript linkedPortalScript;
    private SpriteRenderer portalSprite;
    private GameObject telePortTempObject;
    private bool isReadyToTeleport = false;
    private bool shouldLowerLight = false;
    private float boxEnterVelocity = 0f;

    // Sound effects for Portal
    public AudioSource portalSFX;
    public AudioSource portalAmbienceSFX;
    bool singleton_check = false;


    // Start is called before the first frame update
    void Start()
    {
        linkedPortalScript = linkedPortalObject.GetComponent<PortalScript>();
        portalSprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isBoxBlocking || linkedPortalScript.isBoxBlocking)
        {
            if(portalSprite.color.a > 0.1f)
            {
                ambientParticle.Stop();
                Color tempColor = portalSprite.color;
                tempColor.a -= fadeSpeed;
                portalSprite.color = tempColor;
            }
        }
        else
        {
            if(portalSprite.color.a < 1f)
            {
                Color tempColor = portalSprite.color;
                tempColor.a += fadeSpeed;
                portalSprite.color = tempColor;
            }

            if (portalSprite.color.a > 0.1f)
            {
                if(!singleton_check){
                 portalAmbienceSFX.Play();
                 singleton_check = true;
                }
                ambientParticle.Play();
            }
        }
        

        if (isReadyToTeleport && telePortTempObject != null)
        {
            Light2D playerLight = telePortTempObject.GetComponent<Light2D>();

            if (playerLight.intensity < 3 && !shouldLowerLight)
            {
                playerLight.intensity += teleportSpeed;
            }

            if (playerLight.intensity >= 3)
            {
                isReadyToTeleport = false;
                telePortTempObject.transform.position = linkedPortalObject.transform.position;
                shouldLowerLight = true;


                if(telePortTempObject.CompareTag("Box"))
                {
                    Rigidbody2D otherRB = telePortTempObject.GetComponent<Rigidbody2D>();

                    if (boxEnterVelocity < 0f)
                    {
                        Debug.Log("Push right");
                        otherRB.AddForce(transform.right * boxPopOutVelocity, ForceMode2D.Impulse);
                    }
                    else
                    {
                        Debug.Log("Push left");
                        otherRB.AddForce(-transform.right * boxPopOutVelocity, ForceMode2D.Impulse);
                    }

                    otherRB.AddForce(transform.up * boxPopOutVelocity, ForceMode2D.Impulse);
                }

            }

            if (playerLight.intensity <= 0)
            {
                isReadyToTeleport = false;
            }
        }

        if(telePortTempObject != null)
        {
            if(shouldLowerLight)
            {
                Light2D playerLight = telePortTempObject.GetComponent<Light2D>();
                playerLight.intensity -= teleportSpeed;

                if (playerLight.intensity <= 0)
                {
                    shouldLowerLight = false;
                }
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
             if(!portalSFX.isPlaying){
                // play's portal sound effect
                portalSFX.Play();
             }

        }

        if(other.gameObject.CompareTag("Player") && !isPortalLocked)
        {
            isPortalLocked = true;
            linkedPortalScript.isPortalLocked = true;
            expectPinkExist = true;

            telePortTempObject = other.gameObject;
            isReadyToTeleport = true;
        }
  

        if (other.gameObject.CompareTag("Box") && !isPortalLocked)
        {
            isPortalLocked = true;
            linkedPortalScript.isPortalLocked = true;
            expectPinkExist = true;


            telePortTempObject = other.gameObject;
            isReadyToTeleport = true;

            
        }

        if(other.gameObject.CompareTag("Box"))
        {
            Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
            boxEnterVelocity = -otherRB.velocity.x;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !expectPinkExist)
        {
            isPortalLocked = false;
            linkedPortalScript.isPortalLocked = false;

            linkedPortalScript.expectPinkExist = false;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if(isReadyToTeleport && expectPinkExist)
            {
                isReadyToTeleport = false;
                isPortalLocked = false;
                linkedPortalScript.isPortalLocked = false;
                expectPinkExist = false;

                shouldLowerLight = true;
            }
        }

        if (other.gameObject.CompareTag("Box") && !expectPinkExist)
        {
            isPortalLocked = false;
            linkedPortalScript.isPortalLocked = false;

            linkedPortalScript.expectPinkExist = false;
        }

        if (other.gameObject.CompareTag("Box"))
        {
            isBoxBlocking = false;
            linkedPortalScript.isBoxBlocking = false;

            if (isReadyToTeleport && expectPinkExist)
            {
                isReadyToTeleport = false;
                isPortalLocked = false;
                linkedPortalScript.isPortalLocked = false;
                expectPinkExist = false;

                shouldLowerLight = true;
            }
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

