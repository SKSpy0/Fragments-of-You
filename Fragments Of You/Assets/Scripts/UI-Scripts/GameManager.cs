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
    public string LevelName;
    public AudioSource sceneSwitchSFX;

    float savedValue1 = 1;
    float savedValue2 = 1;
    float savedValue3 = 1;
    
    void Start()
    {
         Debug.Log("loading....");
         LoadingAudioSettings();
    }
    
    // note: to import "sceneManagement" input library to work with LoadScence
    public void ChangeScene(string sceneName){
        sceneSwitchSFX.Play();
        SceneManager.LoadScene(sceneName);

    }
    // changes levels
     public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player has collided with next level collider.");
        ChangeScene(LevelName);

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
        savedValue1 = PlayerPrefs.GetFloat("SavedMasterVolume");
        // Updates UI-Element
        masterSlider.value = PlayerPrefs.GetFloat("SavedMasterVolume", 0.90f); 
    /****************************Background music***********************************/
        savedValue2 = PlayerPrefs.GetFloat("SavedMusicVolume");
        // Updates UI-Element
        bgmSlider.value =  PlayerPrefs.GetFloat("SavedMusicVolume", 0.90f);
    /***************************Sound Effects**************************************/
       savedValue3 =  PlayerPrefs.GetFloat("SavedSFXVolume");
       // Updates UI-Element
       sfxSlider.value = PlayerPrefs.GetFloat("SavedSFXVolume", 0.90f);
    }

    // Options to set different volumes
    public void SetMasterVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last audio settings
        PlayerPrefs.SetFloat("SavedMasterVolume", sliderValue);
        // PlayerPrefs.Save();
        Debug.Log("Master Volume saved!");
    }
    public void SetBGMVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last audio settings
        PlayerPrefs.SetFloat("SavedBGMVolume", sliderValue);
        // PlayerPrefs.Save();
        Debug.Log("Music Volume saved!");
    }
    public void SetSFXVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last audio settings
        PlayerPrefs.SetFloat("SavedSFXVolume", sliderValue);
        // PlayerPrefs.Save();
        Debug.Log("Sound Effect Volume saved!");
    }
}