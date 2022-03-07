using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimation : MonoBehaviour
{
    public float transitionTime;
    public Animator titleLogo;
    public Animator clickToStart;
    public Animator playButtonFade;
    public Animator moreButtonFade;
    public Animator volumnMenuFade;
    public Animator creditMenuFade;
    public Animator quitMenuFade;
    public Animator interfaceFade;
    public Animator DarkTransition;
    public GameObject clickToStartObj;
    public GameObject playButton;
    public GameObject optionButton;
    public GameObject quitButton;


    public void ClickToStart()
    {
        StartCoroutine(ClickTransition());
    }

    public void VolumnTransition()
    {
        volumnMenuFade.SetBool("Enter", true);
    }

    public void LeavevVolumnTransition()
    {
        interfaceFade.SetBool("Enter", true);
    }

    public void QuitTransition()
    {
        quitMenuFade.SetBool("Enter", true);
    }

    public void CreditsTransition()
    {
        creditMenuFade.SetBool("Enter", true);
    }

    public void GameStart()
    {
        StartCoroutine(GameStartTransition());
    }

    IEnumerator ClickTransition()
    {
        titleLogo.SetBool("MouseClicked", true);
        clickToStart.SetBool("StartClicked", true);

        yield return new WaitForSeconds(transitionTime);
        clickToStartObj.SetActive(false);
        playButton.SetActive(true);
        optionButton.SetActive(true);
        quitButton.SetActive(true);

        yield return new WaitForSeconds(transitionTime);
        interfaceFade.SetBool("Enter", true);
    }

    IEnumerator GameStartTransition()
    {
        moreButtonFade.SetBool("Play", true);
        playButtonFade.SetBool("Play", true);

        yield return new WaitForSeconds(1.0f);
        DarkTransition.SetBool("Enter", true);
    }
}
