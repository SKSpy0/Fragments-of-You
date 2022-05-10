using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnSelect : MonoBehaviour
{
    private Button current;
    Animator animator;
    bool start = false;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void Pressed()
    {
        start = true;
    }

    public void WaitForPress()
    {
        if (start == true && count < 1)
        {
            StartCoroutine(WaitForPressTransition());
            count ++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        WaitForPress();
    }

    IEnumerator WaitForPressTransition()
    {
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger("Selected");
        animator.SetTrigger("Normal");
    }
}
