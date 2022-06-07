using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalCollectibles : MonoBehaviour
{
    private TextMeshProUGUI collectibleCounter;
    private TextMeshProUGUI collectibleCounterText;
    public static int collectiblesCollected;

    void Awake()
    {
        // Using Textmesh UI to keep get the instance of the event
        Collectible.CollectiblePickedUp += Run_Coroutine;
        collectibleCounter = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        // string interpolation used here and the entire text pulses
        collectibleCounter.SetText($"Memories: {collectiblesCollected}");

        // Just the number pulses - with two Text Mesh Pro's
        //collectibleCounter.SetText(collectiblesCollected.ToString());
    }

    public int getNum()
    {
        int x = collectiblesCollected;
        return x;
    }

    private void Run_Coroutine()
    {
        // Starts Pulsing Effect to collectibles getting collected.
        StartCoroutine(Pulse());
    }

    private void OnDestroy()
    {
        Collectible.CollectiblePickedUp -= Run_Coroutine;
    }

    private IEnumerator Pulse()
    {
        // first pulse - scales it
        for (float i = 3f; i <= 3.5f; i += 0.1f)
        {
            collectibleCounter.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForFixedUpdate();
        }
        collectibleCounter.rectTransform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

        // increases collectible count by 1.
        collectiblesCollected += 1;

        // second pulse - lowers it
        for (float i = 3.5f; i <= 3f; i -= 0.5f)
        {
            collectibleCounter.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForFixedUpdate();
        }
        // original text set-up
        collectibleCounter.rectTransform.localScale = new Vector3(3f, 3f, 3f);

        /**** For just the number UI *****/
        /* // first pulse - scales it
        for (float i = 1f; i <= 1.5f; i += 0.01f){
            collectibleCounter.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        collectibleCounter.rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        // increases collectible count by 1.
        collectiblesCollected += 1;
        // second pulse - lowers it
        for (float i = 1.5f; i <= 1f; i -= 0.03f){
            collectibleCounter.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        // original text set-up
        collectibleCounter.rectTransform.localScale = new Vector3(1f, 1f, 1f); */
    }

}
