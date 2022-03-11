using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SFXPrompt : MonoBehaviour
{
    public TextMeshProUGUI sfxPrompt;
    public TextMeshProUGUI sfxPrompt1;
    public TextMeshProUGUI sfxPrompt2;
    public GameManager gameManager;
    public GameObject background;
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
        if(!gameManager.LoadingButtonSettings("SFXPToggle")) 
        {
            sfxPrompt.text = "";
            sfxPrompt1.text = "";
            sfxPrompt2.text = "";
            background.SetActive(false);
        } 
        else 
        {
            sfxPrompt.text = prompts[0];
            sfxPrompt1.text = prompts[1];
            sfxPrompt2.text = prompts[2];
            background.SetActive(true);
        }
    }

    public void NewSfxPrompt(string message) {
        prompts[2] = prompts[1];
        prompts[1] = prompts[0];
        prompts[0] = message;

        messageCounter += 1;
    }
}
