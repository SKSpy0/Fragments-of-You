using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  // Declare variables
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    float savedValue1 = 1;
    float savedValue2 = 1 ;
    float savedValue3 = 1;

    void Start()
    {
         Debug.Log("loading....");
        mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        mixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("BGMVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        
         LoadingAudioSettings();

    }
    
    // note: to import "sceneManagement" input library to work with LoadScence
    public void ChangeScence(string scenceName){

        SceneManager.LoadScene(scenceName);

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

    public void LoadingAudioSettings(){
        // Loads previously saved audio setting
        savedValue1 = PlayerPrefs.GetFloat("MasterVolume");
        // Updates UI-Element
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.90f);
    /*************************** *Background music***********************************/
        savedValue2 = PlayerPrefs.GetFloat("BGMVolume");
        // Updates UI-Element
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.90f);
    /***************************Sound Effects**************************************/
       savedValue3 =  PlayerPrefs.GetFloat("SFXVolume");
       // Updates UI-Element
       sfxSlider = PlayerPrefs.GetFloat("SFXVolume", 0.90f);
    }

    // Options to set different volumes
    public void SetMasterVolume(float sliderValue)
    {
        // better volume management - Mathematical formula used
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last audio settings
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
        // PlayerPrefs.Save();
        Debug.Log("Master Volume saved!");
    }
    public void SetBGMVolume(float sliderValue)
    {
        // better volume management - Mathematical formula used
        mixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last audio settings
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);
        // PlayerPrefs.Save();
        Debug.Log("Music Volume saved!");
    }
    public void SetSFXVolume(float sliderValue)
    {
        // better volume management - Mathematical formula used
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last audio settings
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        // PlayerPrefs.Save();
        Debug.Log("Sound Effect Volume saved!");
    }
}