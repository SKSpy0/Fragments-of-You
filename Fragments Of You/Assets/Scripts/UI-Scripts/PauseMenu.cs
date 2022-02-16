using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator pause_Anim;
    public Animator menu_darken;
    public Animator volumnMenuFade;
    public bool isPause;
    private GameObject pausemenu;
    public AudioSource buttonSelectionSFX;


    void Start()
    {
        pausemenu = GameObject.Find("PauseMenu");
        pausemenu.SetActive(false);
    }

      public void ButtonSelection(){
        // play's button selection sound effect
        buttonSelectionSFX.Play();
    }

    public void pause()
    {
        pausemenu.SetActive(true);
        if (!pause_Anim.GetBool("Enter"))
        {
            menu_darken.SetTrigger("Start");
            pause_Anim.SetBool("Enter", true);
        }
        else
        {
            Resume();
        }

        isPause = true;
        Time.timeScale = 0;
    }

    public void unpause()
    {
        Resume();
        isPause = false;
        Time.timeScale = 1;
    }

    private void Resume()
    {
        Time.timeScale = 1;
        StartCoroutine(ResumeTransition());
    }

    public void OptionMenu()
    {
        volumnMenuFade.SetBool("Enter", true);
    }

    public void PauseQuit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void LeavevVolumnTransition()
    {

        Debug.Log("Here1");
        StartCoroutine(OptionMenuTransition());
    }

    IEnumerator ResumeTransition()
    {
        if (pause_Anim.GetBool("isPause"))
        {
            menu_darken.SetBool("isFade", false);
            pause_Anim.SetBool("isPause", false);
        }
        else
        {
            menu_darken.SetBool("isFade", true);
            pause_Anim.SetBool("isPause", true);
        }

        yield return new WaitForSeconds(1f);
        pausemenu.SetActive(false);
    }

    IEnumerator OptionMenuTransition()
    {
        Debug.Log("Here2");
        pause_Anim.SetBool("isPause", true);

        yield return new WaitForSecondsRealtime(0.0001f);
        pause_Anim.SetBool("isPause", false);
    }
}
