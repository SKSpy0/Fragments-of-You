using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    GameObject locksprite_1;
    GameObject unlockLevel_1;
    GameObject locksprite_2;
    GameObject unlockLevel_2;
    GameObject locksprite_3;
    GameObject unlockLevel_3;
    GameObject locksprite_4;
    GameObject unlockLevel_4;
    GameObject locksprite_5;
    GameObject unlockLevel_5;

    public bool locked = true;
    int x;


    // Start is called before the first frame update
    void Start()
    {
        locksprite_1 = GameObject.Find("1-2Lock");
        unlockLevel_1 = GameObject.Find("1-2_Unlock");
        locksprite_2 = GameObject.Find("1-3Lock");
        unlockLevel_2 = GameObject.Find("1-3_Unlock");
        locksprite_3 = GameObject.Find("2-1Lock");
        unlockLevel_3 = GameObject.Find("2-1_Unlock");
        locksprite_4 = GameObject.Find("2-2Lock");
        unlockLevel_4 = GameObject.Find("2-2_Unlock");
        locksprite_5 = GameObject.Find("3Lock");
        unlockLevel_5 = GameObject.Find("3_Unlock");

        // if (locked)
        // {
        //     x = 0;
        // }
        // else
        // {
        //     x = 1;
        // }
        
        // PlayerPrefs.SetInt("1-2Unlock" + SaveID.saveID, x);
        // PlayerPrefs.SetInt("1-3Unlock" + SaveID.saveID, x);
        // PlayerPrefs.SetInt("2-1Unlock" + SaveID.saveID, x);
        // PlayerPrefs.SetInt("2-2Unlock" + SaveID.saveID, x);
        // PlayerPrefs.SetInt("3Unlock" + SaveID.saveID, x);

        LevelStatusCheck();
    }

    public void LevelStatusCheck()
    {
        if (PlayerPrefs.GetInt("1-2Unlock" + SaveID.saveID) == 0)
        {
            locksprite_1.SetActive(true);
            unlockLevel_1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("1-2Unlock" + SaveID.saveID) == 1)
        {
            StartCoroutine(Level1_2UnlockWait());
        }

        if (PlayerPrefs.GetInt("1-3Unlock" + SaveID.saveID) == 0)
        {
            locksprite_2.SetActive(true);
            unlockLevel_2.SetActive(false);
        }
        if (PlayerPrefs.GetInt("1-3Unlock" + SaveID.saveID) == 1)
        {
            StartCoroutine(Level1_3UnlockWait());
        }

        if (PlayerPrefs.GetInt("2-1Unlock" + SaveID.saveID) == 0)
        {
            locksprite_3.SetActive(true);
            unlockLevel_3.SetActive(false);
        }
        if (PlayerPrefs.GetInt("2-1Unlock" + SaveID.saveID) == 1)
        {
            StartCoroutine(Level2_1UnlockWait());
        }

        if (PlayerPrefs.GetInt("2-2Unlock" + SaveID.saveID) == 0)
        {
            locksprite_4.SetActive(true);
            unlockLevel_4.SetActive(false);
        }
        if (PlayerPrefs.GetInt("2-2Unlock" + SaveID.saveID) == 1)
        {
            StartCoroutine(Level2_2UnlockWait());
        }

        if (PlayerPrefs.GetInt("3Unlock" + SaveID.saveID) == 0)
        {
            locksprite_5.SetActive(true);
            unlockLevel_5.SetActive(false);
        }
        if (PlayerPrefs.GetInt("3Unlock" + SaveID.saveID) == 1)
        {
            StartCoroutine(Level3UnlockWait());
        }
    }

    IEnumerator Level1_2UnlockWait()
    {
        unlockLevel_1.SetActive(false);
        locksprite_1.GetComponent<Animator>().SetBool("Enter", true);
        yield return new WaitForSeconds(1f);
        unlockLevel_1.SetActive(true);
        locksprite_1.SetActive(false);
    }
    IEnumerator Level1_3UnlockWait()
    {
        unlockLevel_2.SetActive(false);
        locksprite_2.GetComponent<Animator>().SetBool("Enter", true);
        yield return new WaitForSeconds(1f);
        unlockLevel_2.SetActive(true);
        locksprite_2.SetActive(false);
    }
    IEnumerator Level2_1UnlockWait()
    {
        unlockLevel_3.SetActive(false);
        locksprite_3.GetComponent<Animator>().SetBool("Enter", true);
        yield return new WaitForSeconds(1f);
        unlockLevel_3.SetActive(true);
        locksprite_3.SetActive(false);
    }
    IEnumerator Level2_2UnlockWait()
    {
        unlockLevel_4.SetActive(false);
        locksprite_4.GetComponent<Animator>().SetBool("Enter", true);
        yield return new WaitForSeconds(1f);
        unlockLevel_4.SetActive(true);
        locksprite_4.SetActive(false);
    }
    IEnumerator Level3UnlockWait()
    {
        unlockLevel_5.SetActive(false);
        locksprite_5.GetComponent<Animator>().SetBool("Enter", true);
        yield return new WaitForSeconds(1f);
        unlockLevel_5.SetActive(true);
        locksprite_5.SetActive(false);
    }
}
