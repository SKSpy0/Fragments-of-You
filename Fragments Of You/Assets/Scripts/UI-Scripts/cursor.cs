using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite handCursor;
    public Sprite normalCursor;

    public GameObject clickEffect;

    void Start()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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