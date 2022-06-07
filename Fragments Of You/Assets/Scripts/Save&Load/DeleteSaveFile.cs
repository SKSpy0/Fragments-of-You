using System.Collections;
using UnityEngine;

public class DeleteSaveFile : MonoBehaviour
{
    public Animator BG_Dark;
    public Animator SaveLoadFade;
    Animator DeleteMenu;

    void Start()
    {
        DeleteMenu = GetComponent<Animator>();
    }

    public void DeleteCurrentSaveFile()
    {
        StartCoroutine(DeleteWait());
    }

    IEnumerator DeleteWait()
    {
        DeleteMenu.SetBool("Enter", true);

        if (SaveID.saveID == 1)
        {
            fileToReset();
        }
        if (SaveID.saveID == 2)
        {
            fileToReset();
        }
        if (SaveID.saveID == 3)
        {
            fileToReset();
        }
        yield return new WaitForSeconds(1.0f);
        BG_Dark.SetBool("Enter", true);
        SaveLoadFade.SetBool("Enter", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    public void exitDeleteMenu()
    {
        StartCoroutine(exitDeleteWait());
    }

    IEnumerator exitDeleteWait()
    {
        DeleteMenu.SetBool("Enter", true);
        yield return new WaitForSeconds(0.5f);
        BG_Dark.SetBool("Enter", true);
        SaveLoadFade.SetBool("Enter", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    public void fileToReset()
    {
        PlayerPrefs.SetInt("SlotEmpty" + SaveID.saveID, 1);
        PlayerPrefs.SetFloat("openingPass" + SaveID.saveID, 0);
        PlayerPrefs.SetFloat("tutorialed" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("1-2Unlock" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("1-3Unlock" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("2-1Unlock" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("2-2Unlock" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("3Unlock" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("level_reached" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("1-1Collectables" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("1-2Collectables" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("1-3Collectables" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("2-1Collectables" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("2-2Collectables" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("3Collectables" + SaveID.saveID, 0);
        PlayerPrefs.SetInt("TotalCollected" + SaveID.saveID, 0);
    }
}
