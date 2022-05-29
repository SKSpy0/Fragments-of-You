using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderScript : MonoBehaviour
{
    public GameObject lineRendererObject;
    public GameObject startLocationObject;
    public GameObject endLocationObject;
    public int numberOfNode;
    public float offset;
    public float fadeSpeed;
    public AudioSource thunderSFX;
    public bool playSFX = false;

    private LineRenderer thunderLineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        thunderLineRenderer = lineRendererObject.GetComponent<LineRenderer>();
        GenerateThunder();
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateThunder()
    {
        //Vector2[] thunderPathPoints;
        Vector2 startLocation = startLocationObject.transform.position;
        Vector2 endLocation = endLocationObject.transform.position;

        float lengthY = endLocation.y - startLocation.y;
        float lengthX = endLocation.x - startLocation.x;

        float deltaY = lengthY / numberOfNode;
        float deltaX = lengthX / numberOfNode;

        thunderLineRenderer.positionCount = numberOfNode;

        for(int i = 0; i < numberOfNode - 1; i++)
        {
            float xPosition = Random.Range(-offset, offset) + deltaX * i;
            thunderLineRenderer.SetPosition(i, new Vector2(xPosition, deltaY * i));
        }

        thunderLineRenderer.SetPosition(numberOfNode - 1, new Vector2(lengthX, lengthY));
        if(playSFX)
        {
            thunderSFX.Play();
        }
    }
}
