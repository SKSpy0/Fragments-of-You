using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator pause_Anim;
    public bool isPause;
    private GameObject pausemenu;


    void Start()
    {
        pausemenu = GameObject.Find("PauseMenu");
        pausemenu.SetActive(false);
    }

    public void pause()
    {
        pausemenu.SetActive(true);
        if (!pause_Anim.GetBool("Enter"))
        {
            pause_Anim.SetBool("Enter", true);
        }
        else
        {
            Resume();
        }

        Debug.Log("Game Paused!");
        // Resume();
        isPause = true;
        Time.timeScale = 0;
    }

    public void unpause()
    {
        Debug.Log("Game UnPaused!");

        Resume();
        isPause = false;
        Time.timeScale = 1;
    }

    public void Resume()
    {
        StartCoroutine(ResumeTransition());
    }

    public void OptionMenu()
    {
        StartCoroutine(OptionMenuTransition());
    }

    IEnumerator ResumeTransition()
    {
        if (pause_Anim.GetBool("isPause"))
        {
            pause_Anim.SetBool("isPause", false);
        }
        else
        {
            pause_Anim.SetBool("isPause", true);
        }

        yield return new WaitForSeconds(1f);
        pausemenu.SetActive(false);
    }

    IEnumerator OptionMenuTransition()
    {
        if (pause_Anim.GetBool("isPause"))
        {
            pause_Anim.SetBool("isPause", false);
        }
        else
        {
            pause_Anim.SetBool("isPause", true);
        }

        yield return new WaitForSeconds(1f);
    }
}
