using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieverScript : MonoBehaviour
{
    [SerializeField] private int signal = 0;
    private int buttonCount = 0;
    private int buttonPresses = 0;
    // Start is called before the first frame update

    // make sure you override Start(), Update(), press(), release()
    // these 4 functions must be overwritten even if they do nothing

    public virtual void Start()
    {
        // start always needs this to work.
        searchForSignal();
    }

    public virtual void Update()
    {
        
    }

    public void searchForSignal()
    {
        Debug.Log("door signal: "+signal);
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        for(int i = 0; i < buttons.Length; i++)
        {
            Debug.Log("button Signal: "+buttons[i].GetComponent<ButtonScript>().getSignal());
            if(buttons[i].GetComponent<ButtonScript>().getSignal() == signal)
            {
                buttons[i].GetComponent<ButtonScript>().addItem(this.gameObject);
                buttonCount += 1;
            }
        }
    }

    public void triggerPress()
    {
        buttonPresses += 1;
        if(buttonPresses==buttonCount)
        {
            press();
        }
    }

    public void triggerRelease()
    {
        buttonPresses -= 1;
        if(buttonPresses!=buttonCount)
        {
            release();
        }
    }

    public virtual void press()
    {
        
    }

    public virtual void release()
    {
        
    }

    public int getSignal()
    {
        return signal;
    }
}
