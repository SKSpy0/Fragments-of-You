using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class EndCreditsManager : MonoBehaviour
{
    public AudioSource buttonSelectionSFX;
    StartTransition startTransition;
    public string LevelName;

    
    // Start is called before the first frame update
    void Start()
    {
         // Debug.Log("The toggle set to " + PlayerPrefs.GetFloat("SFXPToggle"));
        startTransition = GameObject.Find("Transition").GetComponent<StartTransition>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTransition.Stopwait)
        {
            ChangeScene(LevelName);
        }        
    }
    
    public void ButtonSelection()
    {
        // play's button selection sound effect
        buttonSelectionSFX.Play();
    }

    // Note: to import "sceneManagement" input library to work with LoadScence
    public void ChangeScene(string sceneName)
    {
         StartCoroutine(ChangeSceneTransition(sceneName));
    }

     IEnumerator ChangeSceneTransition(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        
        if (sceneName == "MainMenu")
        {
            Destroy(GameObject.Find("EndCredits_Music"));
        }

         SceneManager.LoadScene(sceneName);
    }   

}
