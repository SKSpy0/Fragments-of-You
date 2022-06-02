using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDirector : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetTrigger("Base Layer.New_Chop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
