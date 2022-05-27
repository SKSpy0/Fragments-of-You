using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        StartCoroutine(clickWait());
    }
    
    IEnumerator clickWait()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Here");
        button.Select();
    }
}