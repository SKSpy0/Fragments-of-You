using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCam : MonoBehaviour
{
    public GameObject virtualCam;
    public GameObject sectionRespawnPoint;
    Vector2 Respawnposition;
    public Respawn resp;

    void Start()
    {
        resp = GameObject.Find("Pink").GetComponent<Respawn>();
        Respawnposition = new Vector2 (sectionRespawnPoint.transform.position.x, sectionRespawnPoint.transform.position.y);
    }

    // open the cam when player eneter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
            resp.setRespawn(Respawnposition);
        }
    }

    // close the cam when player leave
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
        }
    }
}
