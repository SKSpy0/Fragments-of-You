using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Declare variables
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Get Player Settings on sound and save it when player changes it.
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));

        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        mixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("BGMVolume"));

        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }

    // When player click play button, jump into games
    public void PlayGame()
    {
        StartCoroutine(PlayGameTransition());
    }

    IEnumerator PlayGameTransition()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // When player click quit button, kill the game
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    // Options to set different volumes
    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    public void SetBGMVolume(float value)
    {
        mixer.SetFloat("BGMVolume", value);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
