using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene_Loader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 5f;
    public float fadeAnimationTime = 1f;
    private IEnumerator coroutine;
    bool time;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        coroutine = TimeInCutScene(transitionTime);
        time = false;
        // Start cutscene timer    
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    { 
        if(time){
            LoadNextCutScene();
            time = false;
        }
    }

    public void LoadNextCutScene(){
        // This must be updated if a new scene is added in the build.
       if(SceneManager.GetActiveScene().buildIndex + 1 == 21){
           Destroy(GameObject.Find("BGM_Music"));
        }
         // pass in next Cutscene index and start timer for animation.
       StartCoroutine(LoadCutscene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // Delays fade animation
    IEnumerator LoadCutscene(int cutsceneIndex){

        // Parameter passed in
        transition.SetTrigger("Start");
        // wait one second to play animation
        yield return new WaitForSeconds(fadeAnimationTime);
        SceneManager.LoadScene(cutsceneIndex);
    }

    // Time spent with the cutscene before loading the next one.
    IEnumerator TimeInCutScene(float waitTime)
    {
        // Waits five seconds and then go to next cutscene.
          yield return new WaitForSeconds(waitTime);
        // set boolean check to true;
        time = true;
    }
}
