using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalCollectibles : MonoBehaviour
{
    private TextMeshProUGUI collectibleCounter;
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
        // increases collectible count by 1.
        collectiblesCollected += 1;
        yield return new WaitForEndOfFrame();
    }

}
