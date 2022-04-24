using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTransition : MonoBehaviour
{
    Animator Transition;
    public bool Stopwait = false;

    // Start is called before the first frame update
    void Start()
    {
        Transition = gameObject.GetComponent<Animator>();
    }

    public void StartFadeIn()
    {
        StartCoroutine(StartFadeInWait());
    }

    IEnumerator StartFadeInWait()
    {
        Transition.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        Stopwait = true;
    }
}
