using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Animator Canvas_Darken;

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
