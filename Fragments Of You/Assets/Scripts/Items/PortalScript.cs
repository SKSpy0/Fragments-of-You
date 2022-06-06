using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PortalScript : MonoBehaviour
{
    public GameObject linkedPortalObject;

    
    public float teleportAnimationSpeed = 0.05f;
    [Range(0f, 15f)]
    public float boxPopOutVelocity = 1.5f;
    public float fadeSpeed = 0.1f;
    public ParticleSystem popOutParticle;
    public ParticleSystem ambientParticle;
    public AudioSource portalSFX;
    public AudioSource portalAmbienceSFX;

    [HideInInspector]
    public bool isPortalLocked = false;
    [HideInInspector]
    public bool expectTargetExist = false;
    [HideInInspector]
    public GameObject teleportTargetObj;
    [HideInInspector]
    public bool isPortalBlocked = false;

    public string[] teleportableTags = new string[] {"Player", "Box"};


    private PortalScript linkedPortalScript;
    private SpriteRenderer portalSprite;
    
    private bool isReadyToTeleport = false;
    private bool shouldLowerLight = false;
    private bool singletonSFXCheck = false;
    private float targetEnterVelocity = 0f;

    // isBoxBlocking is for VFX only, for other purpose use isPortalLocked
    /*
    public bool isBoxBlocking = false;
    */
    /*
    [Range (0f, 15f)]
    public float boxPopOutVelocity = 1.5f;
    */

    // Sound effects for Portal
    /*
    public AudioSource portalSFX;
    public AudioSource portalAmbienceSFX;
    */
    


    // Start is called before the first frame update
    void Start()
    {
        linkedPortalScript = linkedPortalObject.GetComponent<PortalScript>();
        portalSprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPortalBlocked || linkedPortalScript.isPortalBlocked)
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
                ambientParticle.Play();
            }
            
        }

        if(teleportTargetObj != null)
        {
            Light2D playerLight = teleportTargetObj.GetComponent<Light2D>();

            if (isReadyToTeleport)
            {
                if (playerLight.intensity < 3 && !shouldLowerLight)
                {
                    playerLight.intensity += teleportAnimationSpeed;
                }

                if (playerLight.intensity >= 3)
                {
                    isReadyToTeleport = false;
                    teleportTargetObj.transform.position = linkedPortalObject.transform.position;
                    shouldLowerLight = true;
                    singletonSFXCheck = false;


                    if (!teleportTargetObj.CompareTag("Player") && teleportTargetObj.GetComponent<Rigidbody2D>() != null)
                    {
                        Rigidbody2D otherRB = teleportTargetObj.GetComponent<Rigidbody2D>();

                        if (targetEnterVelocity < 0f)
                        {
                            otherRB.AddForce(-transform.right * boxPopOutVelocity, ForceMode2D.Impulse);
                        }
                        else
                        {
                            otherRB.AddForce(transform.right * boxPopOutVelocity, ForceMode2D.Impulse);
                        }

                        otherRB.AddForce(transform.up * boxPopOutVelocity, ForceMode2D.Impulse);
                    }
                }
            }

            if(shouldLowerLight)
            {
                playerLight.intensity -= teleportAnimationSpeed;

                if (playerLight.intensity <= 0)
                {
                    shouldLowerLight = false;
                }
            }

        }
        
    }

    public void Portal_Ambience()
    {
         if(!portalAmbienceSFX.isPlaying)
         {
             portalAmbienceSFX.Play();
         }       
    }

    public void Portal_Ambience_Fade()
    {
         portalAmbienceSFX.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(validateObjectTeleportable(other.gameObject))
        {
            

            if (!isPortalLocked && !shouldLowerLight)
            {
                isPortalLocked = true;
                linkedPortalScript.isPortalLocked = true;
                expectTargetExist = true;

                teleportTargetObj = other.gameObject;
                linkedPortalScript.teleportTargetObj = other.gameObject;
                isReadyToTeleport = true;
                portalSFX.Play();
            }

            if (!(isPortalBlocked || linkedPortalScript.isPortalBlocked))
            {
                popOutParticle.Play();
            }
        }

        
  
        /*
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
        */
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (GameObject.ReferenceEquals(teleportTargetObj, other.gameObject))
        {
            if(!other.gameObject.CompareTag("Player"))
            {
                isPortalBlocked = false;
                linkedPortalScript.isPortalBlocked = false;
            }

            if(isReadyToTeleport && expectTargetExist)
            {
                isReadyToTeleport = false;
                isPortalLocked = false;
                linkedPortalScript.isPortalLocked = false;
                expectTargetExist = false;

                shouldLowerLight = true;
            }

            if(!expectTargetExist)
            {
                isPortalLocked = false;
                linkedPortalScript.isPortalLocked = false;
                linkedPortalScript.expectTargetExist = false;
                portalSFX.Stop();
            }
        }

        /*
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
        */
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        /*
        if(other.gameObject.CompareTag("Box"))
        {
            isBoxBlocking = true;
        }
        */

        if(validateObjectTeleportable(other.gameObject))
        {
            if(!other.gameObject.CompareTag("Player"))
            {
                isPortalBlocked = true;
            }
        }
    }

    private bool validateObjectTeleportable(GameObject gb)
    {
        if(gb.GetComponent<Light2D>() == null)
        {
            return false;
        }

        foreach (string tag in teleportableTags)
        {
            if(gb.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}

