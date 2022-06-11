using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveLoadUI : MonoBehaviour
{
    public GameObject saveSlot1_Empty;
    public GameObject saveSlot1_Active;
    public GameObject saveSlot2_Empty;
    public GameObject saveSlot2_Active;
    public GameObject saveSlot3_Empty;
    public GameObject saveSlot3_Active;
    public Animator DarkTransition;

    bool first = true;

    int slot1Percent = 0;
    int slot2Percent = 0;
    int slot3Percent = 0;

    void Start()
    {
        // PlayerPrefs.GetInt("FirstTime") != 1
        
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            PlayerPrefs.SetInt("SlotEmpty" + 1, 1);
            PlayerPrefs.SetInt("SlotEmpty" + 2, 1);
            PlayerPrefs.SetInt("SlotEmpty" + 3, 1);
            PlayerPrefs.SetInt("FirstTime", 1);
        }

        saveTextUpdate();
    }

    void Update()
    {
    }

    public void saveTextUpdate()
    {
        if (PlayerPrefs.GetInt("1-2Unlock" + 1) == 0)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-1";
            slot1Percent = 0;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("1-2Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-2";
            slot1Percent = 15;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("1-3Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-3";
            slot1Percent = 30;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("2-1Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-1";
            slot1Percent = 45;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("2-2Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-2";
            slot1Percent = 60;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("3Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 3";
            slot1Percent = 93;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 1) >= 10)
        {
            slot1Percent += 1;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 1) >= 20)
        {
            slot1Percent += 1;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 1) >= 30)
        {
            slot1Percent += 1;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 1) >= 40)
        {
            slot1Percent += 1;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 1) >= 50)
        {
            slot1Percent += 1;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 1) >= 60)
        {
            slot1Percent += 1;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 1) >= 70)
        {
            slot1Percent += 1;
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot1Percent + "%";
        }

        //---------------------------------------------
        if (PlayerPrefs.GetInt("1-2Unlock" + 2) == 0)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-1";
            slot2Percent = 0;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("1-2Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-2";
            slot2Percent = 15;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("1-3Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-3";
            slot2Percent = 30;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("2-1Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-1";
            slot2Percent = 45;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("2-2Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-2";
            slot2Percent = 60;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("3Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 3";
            slot2Percent = 93;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 2) >= 10)
        {
            slot2Percent += 1;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 2) >= 20)
        {
            slot2Percent += 1;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 2) >= 30)
        {
            slot2Percent += 1;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 2) >= 40)
        {
            slot2Percent += 1;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 2) >= 50)
        {
            slot2Percent += 1;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 2) >= 60)
        {
            slot2Percent += 1;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 2) >= 70)
        {
            slot2Percent += 1;
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot2Percent + "%";
        }

        //---------------------------------------------
        if (PlayerPrefs.GetInt("1-2Unlock" + 3) == 0)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-1";
            slot3Percent = 0;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("1-2Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-2";
            slot3Percent = 15;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("1-3Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-3";
            slot3Percent = 30;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("2-1Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-1";
            slot3Percent = 45;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("2-2Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-2";
            slot3Percent = 60;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("3Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 3";
            slot3Percent = 93;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 3) >= 10)
        {
            slot3Percent += 1;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 3) >= 20)
        {
            slot3Percent += 1;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 3) >= 30)
        {
            slot3Percent += 1;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 3) >= 40)
        {
            slot3Percent += 1;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 3) >= 50)
        {
            slot3Percent += 1;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 3) >= 60)
        {
            slot3Percent += 1;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
        if (PlayerPrefs.GetInt("TotalCollected" + 3) >= 70)
        {
            slot3Percent += 1;
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = slot3Percent + "%";
        }
    }

    public void saveSlotUpdate()
    {
        // saveTextUpdate();
        // Debug.Log("Current Save Slot Num:" + SaveID.saveID);
        // Debug.Log("Current Playerprefs" + SaveID.saveID + ": " + PlayerPrefs.GetInt("SlotEmpty" + SaveID.saveID));
        saveSlotCheck(SaveID.saveID);
    }

    public void saveSlotCheck(int idNum)
    {
        if (PlayerPrefs.GetInt("SlotEmpty" + 1) == 1)
        {
            saveSlot1_Empty.SetActive(true);
            saveSlot1_Active.SetActive(false);
        }
        if (PlayerPrefs.GetInt("SlotEmpty" + 1) == 0)
        {
            saveSlot1_Empty.SetActive(false);
            saveSlot1_Active.SetActive(true);
            saveSlot1_Active.GetComponent<Animator>().SetBool("Enter", true);
        }



        if (PlayerPrefs.GetInt("SlotEmpty" + 2) == 1)
        {
            saveSlot2_Empty.SetActive(true);
            saveSlot2_Active.SetActive(false);
        }
        if (PlayerPrefs.GetInt("SlotEmpty" + 2) == 0)
        {
            saveSlot2_Empty.SetActive(false);
            saveSlot2_Active.SetActive(true);
            saveSlot2_Active.GetComponent<Animator>().SetBool("Enter", true);
        }



        if (PlayerPrefs.GetInt("SlotEmpty" + 3) == 1)
        {
            saveSlot3_Empty.SetActive(true);
            saveSlot3_Active.SetActive(false);
        }
        if (PlayerPrefs.GetInt("SlotEmpty" + 3) == 0)
        {
            saveSlot3_Empty.SetActive(false);
            saveSlot3_Active.SetActive(true);
            saveSlot3_Active.GetComponent<Animator>().SetBool("Enter", true);
        }

    }

    public void Saved1()
    {
        Debug.Log("Current SaveID: " + SaveID.saveID);
        saveTextUpdate();
        StartCoroutine(SaveWait1());
    }

    IEnumerator SaveWait1()
    {
        if (PlayerPrefs.GetInt("SlotEmpty" + SaveID.saveID) == 1)
        {
            PlayerPrefs.SetInt("SlotEmpty" + SaveID.saveID, 0);
            saveSlot1_Empty.GetComponent<Animator>().SetBool("Enter", true);
            yield return new WaitForSeconds(0.5f);
            saveSlot1_Active.GetComponent<Animator>().SetBool("Enter", true);
            yield return new WaitForSeconds(0.5f);
        }

        DarkTransition.SetBool("Enter", true);
        yield return new WaitForSeconds(1.0f);
        if (PlayerPrefs.GetFloat("openingPass" + SaveID.saveID) == 1)
        {
            SceneManager.LoadScene("Level_Map");
        }
        else if (PlayerPrefs.GetFloat("openingPass" + SaveID.saveID) == 0)
        {
            PlayerPrefs.SetFloat("openingPass" + SaveID.saveID, 1);
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Beginning_Cutscene_1");
        }
    }

    public void Saved2()
    {
        Debug.Log("Current SaveID: " + SaveID.saveID);
        StartCoroutine(SaveWait2());
    }

    IEnumerator SaveWait2()
    {
        if (PlayerPrefs.GetInt("SlotEmpty" + SaveID.saveID) == 1)
        {
            PlayerPrefs.SetInt("SlotEmpty" + SaveID.saveID, 0);
            saveSlot2_Empty.GetComponent<Animator>().SetBool("Enter", true);
            yield return new WaitForSeconds(0.5f);
            saveSlot2_Active.GetComponent<Animator>().SetBool("Enter", true);
            yield return new WaitForSeconds(0.5f);
        }

        DarkTransition.SetBool("Enter", true);
        yield return new WaitForSeconds(1.0f);
        if (PlayerPrefs.GetFloat("openingPass" + SaveID.saveID) == 1)
        {
            SceneManager.LoadScene("Level_Map");
        }
        else if (PlayerPrefs.GetFloat("openingPass" + SaveID.saveID) == 0)
        {
            PlayerPrefs.SetFloat("openingPass" + SaveID.saveID, 1);
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Beginning_Cutscene_1");
        }
    }

    public void Saved3()
    {
        Debug.Log("Current SaveID: " + SaveID.saveID);
        StartCoroutine(SaveWait3());
    }

    IEnumerator SaveWait3()
    {
        if (PlayerPrefs.GetInt("SlotEmpty" + SaveID.saveID) == 1)
        {
            PlayerPrefs.SetInt("SlotEmpty" + SaveID.saveID, 0);
            saveSlot3_Empty.GetComponent<Animator>().SetBool("Enter", true);
            yield return new WaitForSeconds(0.5f);
            saveSlot3_Active.GetComponent<Animator>().SetBool("Enter", true);
            yield return new WaitForSeconds(0.5f);
        }

        DarkTransition.SetBool("Enter", true);
        yield return new WaitForSeconds(1.0f);
        if (PlayerPrefs.GetFloat("openingPass" + SaveID.saveID) == 1)
        {
            SceneManager.LoadScene("Level_Map");
        }
        else if (PlayerPrefs.GetFloat("openingPass" + SaveID.saveID) == 0)
        {
            PlayerPrefs.SetFloat("openingPass" + SaveID.saveID, 1);
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Beginning_Cutscene_1");
        }
    }
}
