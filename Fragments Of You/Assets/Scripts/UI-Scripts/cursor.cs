using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite handCursor;
    public Sprite normalCursor;

    public GameObject clickEffect;
    Vector2 cursorPos;


    void Start()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        cursorPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - 0.2f);
        transform.position = cursorPos;

        if (Input.GetMouseButtonDown(0))
        {
            // rend.sprite = handCursor;
            Instantiate(clickEffect, transform.position, Quaternion.identity);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // rend.sprite = normalCursor;
        }
    }

    public void normalCursorSprite()
    {
        rend.sprite = normalCursor;
    }
    public void handCursorSprite()
    {
        rend.sprite = handCursor;
    }
}