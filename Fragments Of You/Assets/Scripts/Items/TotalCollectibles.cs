using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalCollectibles : MonoBehaviour
{
    private TextMeshProUGUI collectibleCounter;
    private TextMeshProUGUI collectibleCounterText;
    public static int collectiblesCollected;

    void Awake(){
        // Using Textmesh UI to keep get the instance of the event
        Collectible.CollectiblePickedUp += Run_Coroutine;
        collectibleCounter = GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate(){
        collectibleCounter.text = "Memories: " + collectiblesCollected.ToString();
    }

     private void Run_Coroutine(){
        // Starts Pulsing Effect to collectibles getting collected.
        StartCoroutine(Pulse());
    }

    private void OnDestroy(){
        Collectible.CollectiblePickedUp -= Run_Coroutine;
    }

    private IEnumerator Pulse(){
        // first pulse - scales it
        for (float i = 3f; i <= 3.5f; i += 0.01f){
            collectibleCounter.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        collectibleCounter.rectTransform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        // increases collectible count by 1.
        collectiblesCollected += 1;
        // second pulse - lowers it
        for (float i = 3.5f; i <= 3f; i -= 0.03f){
            collectibleCounter.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        // original text set-up
        collectibleCounter.rectTransform.localScale = new Vector3(3f, 3f, 3f);
    }

}
