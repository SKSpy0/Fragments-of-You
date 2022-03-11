using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    public Animator Canvas_Darken;
    public AudioSource buttonSelectionSFX;

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

    private void ButtonSound()
    {
        buttonSelectionSFX.Play();
    }

    IEnumerator RetryTransition()
    {
        Canvas_Darken.SetBool("Enter", true);

        yield return new WaitForSeconds(1f);
        ButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator MenuTransition()
    {
        Canvas_Darken.SetBool("Enter", true);

        yield return new WaitForSeconds(1f);
        ButtonSound();
        Destroy(GameObject.Find("BGM_Music"));
        SceneManager.LoadScene("MainMenu");
    }
}
