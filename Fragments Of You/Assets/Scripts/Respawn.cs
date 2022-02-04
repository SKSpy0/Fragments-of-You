using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 respawnPoint;
    private Vector2 increaseSpawnPoint;

    private PinkMovement pm;
    private Animator animator;

     public AudioSource RespawnSFX;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PinkMovement>();
        animator = GetComponent<Animator>();
        increaseSpawnPoint = new Vector2 (0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Respawn functions start --------------------------------------------------------------------
    // sets repawnpoint taking vector 2 as input
    // vector 3 can be converted to vector 2 implicitly
    // Note: not set by default.
    public void setRespawn(Vector2 newPoint)
    {
        respawnPoint = newPoint;
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
        RespawnSFX.Play();
        StartCoroutine(PlayRespawnAnim());
    }
    
    IEnumerator PlayRespawnAnim()
    {
        pm.transform.SetPositionAndRotation(respawnPoint + increaseSpawnPoint,new Quaternion(0,0,0,0));

        // respawn animation
        animator.SetBool("Dead", false);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Respawn", true);
    }
    // Respawn functions end ----------------------------------------------------------------------
}
