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
    [SerializeField] private AudioSource startButtonSFX;
    public AudioSource testingSFX;
    private bool sfxSaved;

    private bool ToggleIsOn;
    private bool thisToggleOn;

    // Accessibility Option - let's player play the game without the need to worry about environmentals
    public static bool GodMode = false;

    float savedValue1 = 1;
    float savedValue2 = 1;
    float savedValue3 = 1;

    public StartTransition startTransition;
    public TotalCollectibles totalCollectibles;

    void Start()
    {
        // Debug.Log("loading....");
        LoadingAudioSettings();
        sfxSaved = true;
        // Debug.Log("The toggle set to " + PlayerPrefs.GetFloat("SFXPToggle"));
        startTransition = GameObject.Find("Transition").GetComponent<StartTransition>();
        totalCollectibles = GameObject.Find("CollectibleTracker").GetComponent<TotalCollectibles>();
    }

    void Update()
    {
        if (startTransition.Stopwait)
        {
            ChangeScene(LevelName);
        }

        // Debug.Log("Current SaveID:" + SaveID.saveID);
        // Debug.Log("Current Level Reach:" + PlayerPrefs.GetInt("level_reached" + SaveID.saveID));
    }

    public void StartButtonPress()
    {
        // play's start button sound effect
        startButtonSFX.Play();
    }

    public void ButtonSelection()
    {
        // play's button selection sound effect
        buttonSelectionSFX.Play();
    }

    // Note: to import "sceneManagement" input library to work with LoadScence
    public void ChangeScene(string sceneName)
    {
        /*** Handles level music transitions ***/
        // Going back to main menu screen from pause menu
        if (sceneName == "Beginning_Cutscene_1")
        {
            Destroy(GameObject.Find("MenuMusic"));
        }
        LevelReachCheck(sceneName);
        StartCoroutine(ChangeSceneTransition(sceneName));
    }

    IEnumerator ChangeSceneTransition(string sceneName)
    {
        yield return new WaitForSeconds(2f);

        if (sceneName == "Level_Map")
        {
            Destroy(GameObject.Find("BGM_Music"));
        }

        if (sceneName == "Z0Tutorial")
        {
            Destroy(GameObject.Find("MenuMusic"));
        }
        if (sceneName == "Z1M1")
        {
            Destroy(GameObject.Find("BGM_Music"));
            Destroy(GameObject.Find("MenuMusic"));
        }
        if (sceneName == "Z1M2")
        {
            Destroy(GameObject.Find("MenuMusic"));
        }
        if (sceneName == "Z1M3")
        {
            Destroy(GameObject.Find("MenuMusic"));
        }
        if (sceneName == "Z2M1")
        {
            Destroy(GameObject.Find("MenuMusic"));
        }
        if (sceneName == "Z2M2")
        {
            Destroy(GameObject.Find("MenuMusic"));
        }
        if (sceneName == "Z3M1")
        {
            Destroy(GameObject.Find("MenuMusic"));
        }
        // cutscenes        
        if (sceneName == "LoseArms_Cutscene")
        {
            Destroy(GameObject.Find("BGM_Music"));
        }
        if (sceneName == "LoseLegs_Cutscene")
        {
            Destroy(GameObject.Find("BGM_Music"));
        }
        if (sceneName == "Ending Chop")
        {
            Destroy(GameObject.Find("BGM_Music"));
        }
        if (sceneName == "Ending_Credits")
        {
            Destroy(GameObject.Find("BGM_Music"));
        }
        if (sceneName == "MainMenu")
        {
            Destroy(GameObject.Find("EndCredits_Music"));
        }

        SceneManager.LoadScene(sceneName);
    }

    public void LevelReachCheck(string LevelName)
    {
        if (LevelName == "Z0Tutorial")
        {
            PlayerPrefs.SetFloat("tutorialed" + SaveID.saveID, 1);
        }
        if (LevelName == "Z1M2")
        {
            PlayerPrefs.SetInt("level_reached" + SaveID.saveID, 1);
            PlayerPrefs.SetInt("1-2Unlock" + SaveID.saveID, 1);
            if (PlayerPrefs.GetInt(("1-1Collectables")) < totalCollectibles.getNum())
            {
                PlayerPrefs.SetInt("1-1Collectables" + SaveID.saveID, totalCollectibles.getNum());
            }
            TotalCollectibles.collectiblesCollected = 0;
        }
        if (LevelName == "Z1M3")
        {
            PlayerPrefs.SetInt("level_reached" + SaveID.saveID, 2);
            PlayerPrefs.SetInt("1-3Unlock" + SaveID.saveID, 1);
            if (PlayerPrefs.GetInt(("1-2Collectables")) < totalCollectibles.getNum())
            {
                PlayerPrefs.SetInt("1-2Collectables" + SaveID.saveID, totalCollectibles.getNum());
            }
            TotalCollectibles.collectiblesCollected = 0;
        }
        if (LevelName == "LoseArms_Cutscene")
        {
            PlayerPrefs.SetInt("level_reached" + SaveID.saveID, 3);
            PlayerPrefs.SetInt("2-1Unlock" + SaveID.saveID, 1);
            if (PlayerPrefs.GetInt(("1-3Collectables")) < totalCollectibles.getNum())
            {
                PlayerPrefs.SetInt("1-3Collectables" + SaveID.saveID, totalCollectibles.getNum());
            }
            TotalCollectibles.collectiblesCollected = 0;
        }
        if (LevelName == "Z2M2")
        {
            PlayerPrefs.SetInt("level_reached" + SaveID.saveID, 4);
            PlayerPrefs.SetInt("2-2Unlock" + SaveID.saveID, 1);
            if (PlayerPrefs.GetInt(("2-1Collectables")) < totalCollectibles.getNum())
            {
                PlayerPrefs.SetInt("2-1Collectables" + SaveID.saveID, totalCollectibles.getNum());
            }
            TotalCollectibles.collectiblesCollected = 0;
        }
        if (LevelName == "LoseLegs_Cutscene")
        {
            PlayerPrefs.SetInt("level_reached" + SaveID.saveID, 5);
            PlayerPrefs.SetInt("3Unlock" + SaveID.saveID, 1);
            if (PlayerPrefs.GetInt(("2-2Collectables")) < totalCollectibles.getNum())
            {
                PlayerPrefs.SetInt("2-2Collectables" + SaveID.saveID, totalCollectibles.getNum());
            }
            TotalCollectibles.collectiblesCollected = 0;
        }
        if (LevelName == "Ending Chop")
        {
            if (PlayerPrefs.GetInt(("3Collectables")) < totalCollectibles.getNum())
            {
                PlayerPrefs.SetInt("3Collectables" + SaveID.saveID, totalCollectibles.getNum());
            }
            TotalCollectibles.collectiblesCollected = 0;
        }

    }

    // changes levels when player hits collider.
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Player has collided with next level collider.");
            // plays screen swipe sfx and changes the scene. This will play on awake.
            sceneSwitchSFX.Play();
            // ChangeScene(LevelName);
            startTransition.StartFadeIn();
        }
    }

    // When player click play button, jump into games
    public void PlayGame()
    {
        // play's button selection sound effect
        buttonSelectionSFX.Play();
        // StartCoroutine(PlayGameTransition());
    }

    // When player click quit button, kill the game
    public void QuitGame()
    {
        buttonSelectionSFX.Play();
        Application.Quit();
    }

    public void LoadingAudioSettings()
    {
        // Loads previously saved audio setting
        savedValue1 = PlayerPrefs.GetFloat("SavedMasterVolume");
        // Updates UI-Element
        masterSlider.value = PlayerPrefs.GetFloat("SavedMasterVolume", 0.70f);
        /****************************Background music***********************************/
        savedValue2 = PlayerPrefs.GetFloat("SavedBGMVolume");
        // Updates UI-Element - SavedMusicVolume
        bgmSlider.value = PlayerPrefs.GetFloat("SavedBGMVolume", 0.70f);
        /***************************Sound Effects**************************************/
        savedValue3 = PlayerPrefs.GetFloat("SavedSFXVolume");
        // Updates UI-Element
        sfxSlider.value = PlayerPrefs.GetFloat("SavedSFXVolume", 0.70f);
    }

    // Accessibility Option - Sound effect prompt toggle and God Mode toggle
    public bool LoadingButtonSettings(string ButtonName)
    {
        if (PlayerPrefs.GetFloat(ButtonName) == 1)
        {
            thisToggleOn = true;
        }

        if (PlayerPrefs.GetFloat(ButtonName) == 0)
        {
            thisToggleOn = false;
        }

        return thisToggleOn;
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

    public void SetSaveID(int saveID)
    {
        SaveID.saveID = saveID;
    }

    public void SetMasterVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last master volume audio settings
        PlayerPrefs.SetFloat("SavedMasterVolume", sliderValue);
        // Debug.Log("Master Volume saved!");
    }
    public void SetBGMVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last background music audio settings
        PlayerPrefs.SetFloat("SavedBGMVolume", sliderValue);
        // Debug.Log("Music Volume saved!");
    }
    public void SetSFXVolume(float sliderValue)
    {
        // better volume management with exposed parameter - Mathematical formula used
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        // Saves players last sound effect settings
        PlayerPrefs.SetFloat("SavedSFXVolume", sliderValue);
        // Debug.Log("Sound Effect Volume saved!");
    }
}