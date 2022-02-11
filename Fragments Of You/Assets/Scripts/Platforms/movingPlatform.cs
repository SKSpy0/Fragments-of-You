// The platform moving code I learned from Xlaugts
// https://www.youtube.com/watch?v=8aSzWGKiDAM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    // Declare Varaibles
    public Transform start, end;
    public float speed;
    public Transform startPos;
    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate variable
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Interchage different positions for the platform to go left and right
        if (transform.position == start.position)
        {
            nextPos = end.position;
        }
        if (transform.position == end.position)
        {
            nextPos = start.position;
        }
        // Move the platform to designated positon
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    // Draw a line on the dizmos carmera for better visability
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(start.position, end.position);
    }
}
