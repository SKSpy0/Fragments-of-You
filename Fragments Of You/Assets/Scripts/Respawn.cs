using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 respawnPoint;
    private Vector2 increaseSpawnPoint;

    private PinkMovement pm;
    private Animator animator;
    private Rigidbody2D rb;
    private PinkGrapple pinkGrapple;

    public AudioSource RespawnSFX;
    private GameObject[] gm;
    //public AudioSource FastRespawnSFX;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PinkMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gm = GameObject.FindGameObjectsWithTag("GameManager");
        pinkGrapple = GetComponent<PinkGrapple>();
    }

    // Respawn functions start --------------------------------------------------------------------
    // sets repawnpoint taking vector 2 as input
    // vector 3 can be converted to vector 2 implicitly
    // Note: not set by default.
    public void setRespawn(Vector2 newPoint)
    {
        
        respawnPoint = newPoint;
        Debug.Log("resP: "+respawnPoint);
    }

    // gets repawnpoint returning vector 2
    public Vector2 getRespawnPoint()
    {
        return respawnPoint;
    }

    // teleports player back to respawnpoint and corrects any orientation
    public void respawnPlayer()
    {
        // Respawn sound effect can be added here.
        gm[0].GetComponent<BoxHandler>().resetAllBoxes();
        RespawnSFX.Play();
        
        if(!pinkGrapple.generateRope){
            pinkGrapple.ReleaseArms();
        }
        StartCoroutine(PlayRespawnAnim());
    }

    // teleports player back to respawnpoint and corrects any orientation
   /* public void faster_respawnPlayer()
    {
        // Respawn sound effect can be added here.
        FastRespawnSFX.Play();
        StartCoroutine(PlayRespawnAnim());
    } */
    
    
    IEnumerator PlayRespawnAnim()
    {
        pm.transform.SetPositionAndRotation(respawnPoint,new Quaternion(0,0,0,0));

        // respawn animation
        pm.gameObject.transform.parent = null;
        animator.SetBool("Dead", false);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Respawn", true);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if(!pinkGrapple.generateRope){
            pinkGrapple.ReleaseArms();
        }

    }
    // Respawn functions end ----------------------------------------------------------------------
}
