using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private bool pressed;
    private SpriteRenderer SR;
    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;
    [SerializeField] private int signal = 0;

    // Start is called before the first frame update
    void Start()
    {
        SR = this.GetComponent<SpriteRenderer>();
        pressed = false;
        SR.sprite = up;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            pressed = true;
            SR.sprite = down;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            pressed = false;
            SR.sprite = up;
        }
    }

    public bool isPressed()
    {
        return pressed;
    }

    public int getSignal()
    {
        return signal;
    }
}
