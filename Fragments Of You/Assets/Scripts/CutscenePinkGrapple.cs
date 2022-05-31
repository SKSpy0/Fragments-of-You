using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenePinkGrapple : MonoBehaviour
{
    public GameObject handsGameObject;
    [Range (1f, -1f)]
    public float startPosYOffset = -0.1f;
    [Range(1f, -1f)]
    public float endPosXOffset = 0.1f;

    private LineRenderer lr;



    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 startPt = new Vector2(this.gameObject.transform.position.x,
            this.gameObject.transform.position.y + startPosYOffset);
        Vector2 endPt = new Vector2(handsGameObject.transform.position.x + endPosXOffset,
            handsGameObject.transform.position.y);

        lr.SetPosition(0, startPt);
        lr.SetPosition(1, endPt);
    }
}
