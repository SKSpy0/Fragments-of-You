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
    private List<GameObject> recievers;

    public AudioSource PathUnlockedSFX;

    void Awake()
    {
        recievers = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SR = this.GetComponent<SpriteRenderer>();
        pressed = false;
        SR.sprite = up;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!pressed)
            {
                press();
            }
            pressed = true;
            SR.sprite = down;
        }

        if(other.gameObject.CompareTag("Box")){
            if(!pressed)
            {
                press();
                 PathUnlockedSFX.Play();
            }
            pressed = true;
            SR.sprite = down;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            if(pressed)
            {
                release();
            }
            pressed = false;
            SR.sprite = up;
        }
    }

    public void press()
    {
        foreach(GameObject GO in recievers)
        {
            Debug.Log("PressingDoor"+GO.GetComponent<DoorScript>().getSignal());
            Debug.Log("PressingReciever"+GO.GetComponent<RecieverScript>().getSignal());
            GO.GetComponent<RecieverScript>().triggerPress();
        }
    }

    public void release()
    {
        foreach(GameObject GO in recievers)
        {
            Debug.Log("RealisingDoor"+GO.GetComponent<DoorScript>().getSignal());
            Debug.Log("RealisingReciever"+GO.GetComponent<RecieverScript>().getSignal());
            GO.GetComponent<RecieverScript>().triggerRelease();
        }
    }

    public int getSignal()
    {
        return signal;
    }

    public void addItem(GameObject reciever)
    {
        recievers.Add(reciever);
        Debug.Log("adding Reciever");
    }
}
