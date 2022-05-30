using System.Collections;
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
    public Animator volumeMenuRealFade;
    public Animator accessbilityMenuFade;
    public Animator creditMenuFade;
    public Animator quitMenuFade;
    public Animator interfaceFade;
    public Animator DarkTransition;
    public Animator BG_Dark;
    public Animator SaveLoadFade;
    public GameObject clickToStartObj;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject titleText;
    public GameObject MainMenuInterface;

    public GameObject myEventSystem;

    bool clicked = false;

    void Start()
    {
        myEventSystem = GameObject.Find("EventSystem");
    }

    public void unSelectButtons()
    {
        clicked = false;
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void ClickToStart()
    {
        StartCoroutine(ClickTransition());
    }

    public void VolumnTransition()
    {
        volumnMenuFade.SetBool("Enter", true);
    }

    public void ButtonSelect(GameObject button)
    {
        button.GetComponent<Animator>().SetBool("Enter", true);
    }

    public void ButtonTextSelect(GameObject buttonText)
    {
        buttonText.GetComponent<Animator>().SetBool("Enter", true);
    }

    public void ButtonDeSelect(GameObject button)
    {
        button.GetComponent<Animator>().SetBool("Enter", false);
    }

    public void ButtonTextDeSelect(GameObject buttonText)
    {
        buttonText.GetComponent<Animator>().SetBool("Enter", false);
    }

    public void VolumeRealTransition(GameObject menu)
    {
        StartCoroutine(VolumnRealTransitionWait(menu));
    }

    public void LeavevVolumnRealTransition(GameObject menu)
    {
        if (clicked == false)
        {
            StartCoroutine(LeavevVolumnRealTransitionWait(menu));
        }
    }

    public void LeavevVolumnTransition()
    {
        interfaceFade.SetBool("Enter", true);
    }

    public void AccessbilityTransition(GameObject menu)
    {
        StartCoroutine(AccessbilityTransitionWait(menu));
    }

    public void LeaveAccessbilityTransition(GameObject menu)
    {
        if (clicked)
        {
            StartCoroutine(LeavevAccessbilityTransitionWait(menu));
        }
    }

    public void CreditsTransition()
    {
        StartCoroutine(CreditsTransitionWait());
    }

    public void QuitTransition()
    {
        quitMenuFade.SetBool("Enter", true);
    }

    public void GameStart()
    {
        StartCoroutine(GameStartTransition());
    }
    public void BG_Darken()
    {
        if (BG_Dark.GetBool("Enter"))
        {
            BG_Dark.SetBool("Enter", false);
        }
        else
        {
            BG_Dark.SetBool("Enter", true);
        }
    }
    public void ButtonsFade()
    {
        moreButtonFade.SetBool("Back", true);

    }

    public void CloseTitle()
    {
        StartCoroutine(CloseTitleTransition());
    }

    IEnumerator CloseTitleTransition()
    {
        yield return new WaitForSeconds(1.5f);
        titleText.SetActive(false);
    }

    IEnumerator ClickTransition()
    {
        titleLogo.SetBool("MouseClicked", true);
        clickToStart.SetBool("StartClicked", true);

        yield return new WaitForSeconds(transitionTime);
        clickToStartObj.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);

        yield return new WaitForSeconds(transitionTime);
        interfaceFade.SetBool("Enter", true);
    }

    IEnumerator VolumnRealTransitionWait(GameObject menu)
    {
        yield return new WaitForSeconds(0.5f);
        menu.SetActive(true);
        volumeMenuRealFade.SetBool("Enter1", true);
    }

    IEnumerator LeavevVolumnRealTransitionWait(GameObject menu)
    {
        volumeMenuRealFade.SetBool("Enter1", false);

        yield return new WaitForSeconds(0.5f);

        accessbilityMenuFade.SetBool("Enter", true);
        menu.SetActive(false);
        clicked = true;
    }

    IEnumerator AccessbilityTransitionWait(GameObject menu)
    {
        yield return new WaitForSeconds(0.5f);
        menu.SetActive(true);
        accessbilityMenuFade.SetBool("Enter", true);
    }

    IEnumerator LeavevAccessbilityTransitionWait(GameObject menu)
    {
        accessbilityMenuFade.SetBool("Enter", false);

        yield return new WaitForSeconds(0.5f);

        volumeMenuRealFade.SetBool("Enter1", true);
        menu.SetActive(false);
        clicked = false;
    }

    IEnumerator CreditsTransitionWait()
    {
        creditMenuFade.SetBool("Enter", true);

        yield return new WaitForSeconds(1.0f);

        creditMenuFade.SetBool("Rolling", true);
    }

    IEnumerator GameStartTransition()
    {
        moreButtonFade.SetBool("Play", true);
        playButtonFade.SetBool("Play", true);

        yield return new WaitForSeconds(1.0f);
        BG_Dark.SetBool("Enter", true);
        SaveLoadFade.SetBool("Enter", true);
        yield return new WaitForSeconds(0.5f);
        MainMenuInterface.SetActive(false);
    }
}
