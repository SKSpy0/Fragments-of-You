using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAnchorScript : MonoBehaviour
{
    [SerializeField] private Sprite anchor;
    [SerializeField] private Sprite active;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = anchor;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("interaction");
        // Fix box postion on the movingplatforms
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Respawn>().setRespawn(this.transform.position);
            GameObject[] respawns = GameObject.FindGameObjectsWithTag("RespawnPoint");
            foreach(GameObject GO in respawns)
            {
                GO.gameObject.GetComponent<RespawnAnchorScript>().deActivate();
            }
            this.Activate();
        }
    }

    public void deActivate()
    {
        this.GetComponent<SpriteRenderer>().sprite = anchor;
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().sprite = active;
    }
}
