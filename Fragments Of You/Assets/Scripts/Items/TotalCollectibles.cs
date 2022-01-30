using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotalCollectibles : MonoBehaviour
{
    public Text collectibleText;
    public static int collectible;

    void Update(){
        collectibleText.GetComponent<Text>().text = "Hearts: " + collectible;
    }

}
