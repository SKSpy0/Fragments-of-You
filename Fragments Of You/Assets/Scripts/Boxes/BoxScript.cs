using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Vector2 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respawn()
    {
        this.transform.position = spawnPoint;
    }
}
