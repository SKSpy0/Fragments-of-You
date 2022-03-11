using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXPrompt : MonoBehaviour
{
    public Text sfxPrompt;
    public Text sfxPrompt1;
    public Text sfxPrompt2;
    public GameManager gameManager;
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
        if(gameManager.LoadingButtonSettings("SFXPToggle")) 
        {
            sfxPrompt.text = "";
            sfxPrompt1.text = "";
            sfxPrompt2.text = "";

        } 
        else 
        {
            sfxPrompt.text = prompts[0];
            sfxPrompt1.text = prompts[1];
            sfxPrompt2.text = prompts[2];
        }
    }

    public void NewSfxPrompt(string message) {
        prompts[2] = prompts[1];
        prompts[1] = prompts[0];
        prompts[0] = message;

        messageCounter += 1;
    }
}
