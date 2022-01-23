using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonAction : MonoBehaviour
{
    public Animator Canvas_Darken;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;


    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Retry()
    {
        StartCoroutine(RetryTransition());
    }

    public void Menu()
    {
        StartCoroutine(MenuTransition());
    }

    IEnumerator RetryTransition()
    {
        Canvas_Darken.SetTrigger("Start");
        if (Canvas_Darken.GetBool("Transitioning"))
        {
            Canvas_Darken.SetBool("Transitioning", false);
        }
        else
        {
            Canvas_Darken.SetBool("Transitioning", true);
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator MenuTransition()
    {
        Canvas_Darken.SetTrigger("Start");
        if (Canvas_Darken.GetBool("Transitioning"))
        {
            Canvas_Darken.SetBool("Transitioning", false);
        }
        else
        {
            Canvas_Darken.SetBool("Transitioning", true);
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
