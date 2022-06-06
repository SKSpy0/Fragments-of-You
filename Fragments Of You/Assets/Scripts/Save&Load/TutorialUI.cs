using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    Animator TutorialMenu;
    public Animator Transition;
    public StartTransition startTransition;

    void Start()
    {
        if (PlayerPrefs.GetFloat("tutorialed"+ SaveID.saveID) == 1)
        {
            gameObject.SetActive(false);
        }

        TutorialMenu = GetComponent<Animator>();
    }

    public void startTutorial()
    {
        StartCoroutine(startTutorialWait());
    }

    IEnumerator startTutorialWait()
    {
        PlayerPrefs.SetFloat("tutorialed" + SaveID.saveID, 1);
        Transition.SetBool("FadeIn", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Z0Tutorial");
        Destroy(GameObject.Find("MenuMusic"));
    }

    public void exitTutorial()
    {
        StartCoroutine(exitTutorialWait());
    }

    IEnumerator exitTutorialWait()
    {
        TutorialMenu.SetBool("Enter", true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
