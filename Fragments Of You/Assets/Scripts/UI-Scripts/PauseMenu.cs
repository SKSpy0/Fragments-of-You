using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    public Animator pause_Anim;
    public Animator menu_darken;
    Animator blackout;
    public Animator volumnMenuFade;
    public Animator quitMenuFade;
    public bool isPause;
    private GameObject pausemenu;
    public AudioSource buttonSelectionSFX;

    public Animator mouseFade;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        pausemenu = GameObject.Find("PauseMenu");
        pausemenu.SetActive(false);
        mouseFade = GameObject.Find("MouseCursor").GetComponent<Animator>();
        blackout = GameObject.Find("Transition").GetComponent<Animator>();
    }

    void Update()
    {
        Cursor.visible = false;
    }

    public void ButtonSelection()
    {
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
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1;
        mouseFade.SetBool("Fade", true);
        StartCoroutine(ResumeTransition());
    }

    public void OptionMenu()
    {
        volumnMenuFade.SetBool("Enter", true);
    }

    public void QuitMenu()
    {
        quitMenuFade.SetBool("Enter", true);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(BackToMenuTransition());
    }

    public void PauseQuit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void LeavevVolumnTransition()
    {
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
        pause_Anim.SetBool("isPause", true);

        yield return new WaitForSecondsRealtime(0.0001f);
        pause_Anim.SetBool("isPause", false);
    }

    IEnumerator BackToMenuTransition()
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
        blackout.SetBool("FadeIn", true);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.Find("BGM_Music"));
    }
}
