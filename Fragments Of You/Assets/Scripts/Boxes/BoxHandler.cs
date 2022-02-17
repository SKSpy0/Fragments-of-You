using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    private GameObject[] boxes;
    // Start is called before the first frame update
    void Start()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetAllBoxes()
    {
        for(int i = 0; i < boxes.Length; i++)
        {
            boxes[i].GetComponent<BoxScript>().respawn();
        }
    }
}
