using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCam : MonoBehaviour
{
    public GameObject virtualCam;
    private Respawn resp;

    void Start()
    {
        resp = GameObject.FindGameObjectWithTag("Player").GetComponent<Respawn>();
    }

    // open the cam when player eneter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
            resp.setRespawn(other.transform.position);
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
