using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkRespawn : MonoBehaviour
{
    private PinkMovement pm;

    private Vector2 respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        pm = this.GetComponent<PinkMovement>();

        respawnPoint = pm.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if player falls off level respawn
        if(pm.transform.position.y < -8)
        {
            respawnPlayer();
        }
        // ^this can be removed once spikes and other kills are implemented
    }

    // Respawn functions start --------------------------------------------------------------------
    // sets repawnpoint taking vector 2 as input
    // vector 3 can be converted to vector 2 implicitly
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
        pm.transform.SetPositionAndRotation(respawnPoint,new Quaternion(0,0,0,0));
    }
    // Respawn functions end ----------------------------------------------------------------------
}
