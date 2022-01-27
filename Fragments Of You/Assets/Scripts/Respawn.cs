using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 respawnPoint;

    private PinkMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PinkMovement>();
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
        pm.transform.SetPositionAndRotation(respawnPoint,new Quaternion(0,0,0,0));
        // respawn animation and sound effect can be added here.
    }
    // Respawn functions end ----------------------------------------------------------------------
}
