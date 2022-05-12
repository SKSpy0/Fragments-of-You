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

    private LineRenderer thunderLineRenderer;
    private bool fadeOut;

    // Start is called before the first frame update
    void Awake()
    {
        thunderLineRenderer = lineRendererObject.GetComponent<LineRenderer>();
        GenerateThunder();
    }

    // Update is called once per frame
    void Update()
    {
        //RenderThunder();
        if(fadeOut)
        {
            Debug.Log("Alpha: " + lineRendererObject.GetComponent<LineRenderer>().material.color.a);
            Color objectColor = lineRendererObject.GetComponent<LineRenderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.g, fadeAmount);
            lineRendererObject.GetComponent<LineRenderer>().material.color = objectColor;

            if(objectColor.a <= 0)
            {
                Debug.Log("fadeout complete");
                fadeOut = false;
            }
        }
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
    }
}
