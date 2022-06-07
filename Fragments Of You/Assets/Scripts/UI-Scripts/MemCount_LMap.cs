using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemCount_LMap : MonoBehaviour
{
    public TextMeshProUGUI count1;
    public TextMeshProUGUI count2;
    public TextMeshProUGUI count3;
    public TextMeshProUGUI count4;
    public TextMeshProUGUI count5;
    public TextMeshProUGUI count6;
    public TextMeshProUGUI TotalCount;
    public GameObject totalMem_Image;
    public GameObject pog;

    int num1;
    int num2;
    int num3;
    int num4;
    int num5;
    int num6;
    public int total;

    void Start()
    {
        num1 = PlayerPrefs.GetInt("1-1Collectables" + SaveID.saveID);
        num2 = PlayerPrefs.GetInt("1-2Collectables" + SaveID.saveID);
        num3 = PlayerPrefs.GetInt("1-3Collectables" + SaveID.saveID);
        num4 = PlayerPrefs.GetInt("2-1Collectables" + SaveID.saveID);
        num5 = PlayerPrefs.GetInt("2-2Collectables" + SaveID.saveID);
        num6 = PlayerPrefs.GetInt("3Collectables" + SaveID.saveID);
        total = num1 + num2 + num3 + num4 + num5 + num6;
        PlayerPrefs.SetInt("TotalCollected" + SaveID.saveID, total);
        updateCountText();
    }

    public void updateCountText()
    {
        count1.text = num1 + "/4";
        count2.text = num2 + "/4";
        count3.text = num3 + "/9";
        count4.text = num4 + "/8";
        count5.text = num5 + "/3";
        count6.text = num6 + "/42";
        TotalCount.text = total + "/70";

        if (total >= 70)
        {
            totalMem_Image.SetActive(false);
            pog.SetActive(true);
        }
    }
}
