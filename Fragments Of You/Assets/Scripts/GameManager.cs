using System.Collections;
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
    public AudioSource buttonSelectionSFX;
    public AudioSource testingSFX;
    private bool sfxSaved;

    float savedValue1 = 1;
    float savedValue2 = 1;
    float savedValue3 = 1;

    void Start()
    {
        Debug.Log("loading....");
        LoadingAudioSettings();
        sfxSaved = true;
    }

    public void ButtonSelection()
    {
        // play's button selection sound effect
        buttonSelectionSFX.Play();
    }

    // Note: to import "sceneManagement" input library to work with LoadScence
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        /*** Handles level music transitions ***/
        // Going back to main menu screen from pause menu
        if(sceneName == "Z1Tutorial"){
                Destroy(GameObject.Find("MenuMusic"));
            }
        if(sceneName == "Z1M1"){
                Destroy(GameObject.Find("MenuMusic"));
            }
        if(sceneName == "Z1M2"){
                Destroy(GameObject.Find("MenuMusic"));
            }
        if(sceneName == "Z2M1"){
                Destroy(GameObject.Find("MenuMusic"));
            }
        // first cutscene        
        if(sceneName == "LoseArms_Cutscene"){
                Destroy(GameObject.Find("BGM_Music"));
            }   
    }

    // changes levels when player hits collider.
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has collided with next level collider.");
            // plays screen swipe sfx and changes the scene. This will play on awake.
            sceneSwitchSFX.Play();
            ChangeScene(LevelName);
        }
    }

    // When player click play button, jump into games
    public void PlayGame()
    {
        // play's button selection sound effect
        buttonSelectionSFX.Play();
        StartCoroutine(PlayGameTransition());
    }

    IEnumerator PlayGameTransition()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level_Select");
    }

    // When player click quit button, kill the game
    public void QuitGame()
    {
        Debug.Log("Quit!");
        buttonSelectionSFX.Play();
        Application.Quit();
    }

    public void LoadingAudioSettings()
    {
        // Loads previously saved audio setting
        savedValue1 = PlayerPrefs.GetFloat("SavedMasterVolume");
        // Updates UI-Element
        masterSlider.value = PlayerPrefs.GetFloat("SavedMasterVolume", 0.90f);
        /****************************Background music***********************************/
        savedValue2 = PlayerPrefs.GetFloat("SavedBGMVolume");
        // Updates UI-Element - SavedMusicVolume
        bgmSlider.value = PlayerPrefs.GetFloat("SavedBGMVolume", 0.90f);
        /***************************Sound Effects**************************************/
        savedValue3 = PlayerPrefs.GetFloat("SavedSFXVolume");
        // Updates UI-Element
        sfxSlider.value = PlayerPrefs.GetFloat("SavedSFXVolume", 0.90f);
    }

    // Options to set different volumes and here audio

    public void loopingTestSFX()
    {
        if (sfxSaved && !testingSFX.isPlaying)
        {
            // play looping testing effect if sound effects slider is moved
            testingSFX.Play();
            sfxSaved = false;
        }
        else
        {
            sfxSaved = true;
        }
    }

    public void SetMasterVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last master volume audio settings
        PlayerPrefs.SetFloat("SavedMasterVolume", sliderValue);
        Debug.Log("Master Volume saved!");
    }
    public void SetBGMVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last background music audio settings
        PlayerPrefs.SetFloat("SavedBGMVolume", sliderValue);
        Debug.Log("Music Volume saved!");
    }
    public void SetSFXVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last sound effect settings
        PlayerPrefs.SetFloat("SavedSFXVolume", sliderValue);
        Debug.Log("Sound Effect Volume saved!");
    }
}