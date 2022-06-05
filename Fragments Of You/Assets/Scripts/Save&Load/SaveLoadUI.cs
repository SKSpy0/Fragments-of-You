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

    void Start()
    {
        // PlayerPrefs.GetInt("FirstTime") != 1
        if (PlayerPrefs.GetInt("FirstTime") != 1)
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
        Debug.Log("Current SaveSlot" + SaveID.saveID + " pass: " + PlayerPrefs.GetFloat("openingPass" + SaveID.saveID));
    }

    public void saveTextUpdate()
    {
        if (PlayerPrefs.GetInt("1-2Unlock" + 1) == 0)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-1";
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "0%";
        }
        if (PlayerPrefs.GetInt("1-2Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-2";
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "15%";
        }
        if (PlayerPrefs.GetInt("1-3Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-3";
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "30%";
        }
        if (PlayerPrefs.GetInt("2-1Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-1";
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "45%";
        }
        if (PlayerPrefs.GetInt("2-2Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-2";
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "60%";
        }
        if (PlayerPrefs.GetInt("3Unlock" + 1) == 1)
        {
            saveSlot1_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 3";
            saveSlot1_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "99%";
        }
        
        //---------------------------------------------
        if (PlayerPrefs.GetInt("1-2Unlock" + 2) == 0)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-1";
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "0%";
        }
        if (PlayerPrefs.GetInt("1-2Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-2";
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "15%";
        }
        if (PlayerPrefs.GetInt("1-3Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-3";
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "30%";
        }
        if (PlayerPrefs.GetInt("2-1Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-1";
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "45%";
        }
        if (PlayerPrefs.GetInt("2-2Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-2";
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "60%";
        }
        if (PlayerPrefs.GetInt("3Unlock" + 2) == 1)
        {
            saveSlot2_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 3";
            saveSlot2_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "99%";
        }

        //---------------------------------------------
        if (PlayerPrefs.GetInt("1-2Unlock" + 3) == 0)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-1";
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "0%";
        }
        if (PlayerPrefs.GetInt("1-2Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-2";
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "15%";
        }
        if (PlayerPrefs.GetInt("1-3Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 1-3";
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "30%";
        }
        if (PlayerPrefs.GetInt("2-1Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-1";
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "45%";
        }
        if (PlayerPrefs.GetInt("2-2Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 2-2";
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "60%";
        }
        if (PlayerPrefs.GetInt("3Unlock" + 3) == 1)
        {
            saveSlot3_Active.transform.Find("CurrentLevel_Text").GetComponent<TextMeshProUGUI>().text = "Level 3";
            saveSlot3_Active.transform.Find("ProgressPercent_Text").GetComponent<TextMeshProUGUI>().text = "99%";
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
            // Changed starting point to tutorial
            SceneManager.LoadScene("Z0Tutorial");
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
