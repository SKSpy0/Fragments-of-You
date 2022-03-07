using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXPrompt : MonoBehaviour
{
    public Text sfxPrompt;
    public Image backgroundImage;
    private string[] prompts = new string[3];

    private int messageCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        NewSfxPrompt("SFX Prompt Enabled");
    }

    // Update is called once per frame
    void Update()
    {
        sfxPrompt.text = prompts[0];
    }

    public void NewSfxPrompt(string message) {
        prompts[2] = prompts[1];
        prompts[1] = prompts[0];
        prompts[0] = message;

        messageCounter += 1;
    }
}
