using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public BoxCollider2D SpikeCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Accessibility Option - God Mode check
        if(GameManager.GodMode){
            SpikeCollider.enabled = false;
        }          
    }

}
