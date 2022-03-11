using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthX, startposX, lengthY, startposY;
    public GameObject cam;
    public float parallaxEffect;
    public bool enableY = false;

    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        startposY = transform.position.y;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffect));
        float distanceX = (cam.transform.position.x * parallaxEffect);
        float tempY = (cam.transform.position.y * (1 - parallaxEffect));
        float distanceY = (cam.transform.position.y * parallaxEffect);
        
        if(enableY) {
            transform.position = new Vector3(startposX + distanceX, startposY + distanceY, transform.position.z);
        } else {
            transform.position = new Vector3(startposX + distanceX, transform.position.y, transform.position.z);
        }
        if (tempX > startposX + lengthX) startposX += lengthX;
        else if (tempX < startposX - lengthX) startposX -= lengthX;
        
        if (tempY > startposY + lengthY) startposY += lengthY;
        else if (tempY < startposY - lengthY) startposY -= lengthY;
    }
}
