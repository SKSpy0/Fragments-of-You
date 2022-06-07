using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    public Animator LevelMapPink;
    public int level_Count = 0;
    public int level_Cap = 0;
    public bool waitIsOver = true;

    void Start()
    {
        level_Cap = PlayerPrefs.GetInt("level_reached" + SaveID.saveID);
        StartCoroutine(placePinkToCurrentLevelTransition());
        placePinkToCurrentLevel();
    }

    public void placePinkToCurrentLevel()
    {
        if (level_Cap > 0)
        {
            level_Count = 1;
            LevelMapPink.SetInteger("levelCount", level_Count);
            LevelMapPink.SetBool("Arrive1-2", true);
        }
        if (level_Cap > 1)
        {
            level_Count = 2;
            LevelMapPink.SetInteger("levelCount", level_Count);
            LevelMapPink.SetBool("Arrive1-3", true);
        }
        if (level_Cap > 2)
        {
            level_Count = 3;
            LevelMapPink.SetInteger("levelCount", level_Count);
            LevelMapPink.SetBool("Arrive2-1", true);
        }
        if (level_Cap > 3)
        {
            level_Count = 4;
            LevelMapPink.SetInteger("levelCount", level_Count);
            LevelMapPink.SetBool("Arrive2-2", true);
        }
        if (level_Cap > 4)
        {
            level_Count = 5;
            LevelMapPink.SetInteger("levelCount", level_Count);
            LevelMapPink.SetBool("Arrive3", true);
        }
    }

    IEnumerator placePinkToCurrentLevelTransition()
    {
        if (level_Cap == 1)
        {
            LevelMapPink.SetBool("Arrived1-2", true);
            yield return new WaitForSeconds(0.1f);
            LevelMapPink.SetBool("Arrived1-2", false);
        }
        if (level_Cap == 2)
        {
            LevelMapPink.SetBool("Arrived1-3", true);
            yield return new WaitForSeconds(0.1f);
            LevelMapPink.SetBool("Arrived1-3", false);
        }
        if (level_Cap == 3)
        {
            LevelMapPink.SetBool("Arrived2-1", true);
            yield return new WaitForSeconds(0.1f);
            LevelMapPink.SetBool("Arrived2-1", false);
        }
        if (level_Cap == 4)
        {
            LevelMapPink.SetBool("Arrived2-2", true);
            yield return new WaitForSeconds(0.1f);
            LevelMapPink.SetBool("Arrived2-2", false);
        }
        if (level_Cap == 5)
        {
            LevelMapPink.SetBool("Arrived3", true);
            yield return new WaitForSeconds(0.1f);
            LevelMapPink.SetBool("Arrived3", false);
        }
        yield return new WaitForSeconds(0f);
    }

    public void moveFoward()
    {
        if (level_Count != level_Cap && waitIsOver)
        {
            level_Count++;
            LevelMapPink.SetInteger("levelCount", level_Count);
            ariveWait();
            waitIsOver = false;
        }
    }

    public void moveBackward()
    {
        if (level_Count != 0 && waitIsOver)
        {
            level_Count--;
            LevelMapPink.SetInteger("levelCount", level_Count);
            ariveWait();
            waitIsOver = false;
        }
    }

    public void ariveWait()
    {
        StartCoroutine(ariveWaitTransition());
    }

    public void EnterLevel()
    {
        if (level_Count == 0 && waitIsOver)
        {
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Z1M1");
        }
        if (level_Count == 1 && waitIsOver)
        {
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Z1M2");
        }
        if (level_Count == 2 && waitIsOver)
        {
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Z1M3");
        }
        if (level_Count == 3 && waitIsOver)
        {
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Z2M1");
        }
        if (level_Count == 4 && waitIsOver)
        {
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Z2M2");
        }
        if (level_Count == 5 && waitIsOver)
        {
            Destroy(GameObject.Find("MenuMusic"));
            SceneManager.LoadScene("Z3M1");
        }
    }

    IEnumerator ariveWaitTransition()
    {
        if (level_Count == 0)
        {
            LevelMapPink.SetBool("Arrive1-1", true);
            yield return new WaitForSeconds(1.2f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive1-2", false);
        }

        if (level_Count == 1)
        {
            LevelMapPink.SetBool("Arrive1-2", true);
            yield return new WaitForSeconds(1.2f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive1-1", false);
        }

        if (level_Count == 2)
        {
            LevelMapPink.SetBool("Arrive1-3", true);
            yield return new WaitForSeconds(1f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive1-2", false);
        }

        if (level_Count == 3)
        {
            LevelMapPink.SetBool("Arrive2-1", true);
            yield return new WaitForSeconds(1.2f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive1-3", false);
        }

        if (level_Count == 4)
        {
            LevelMapPink.SetBool("Arrive2-2", true);
            yield return new WaitForSeconds(1.2f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive2-1", false);
        }

        if (level_Count == 5)
        {
            LevelMapPink.SetBool("Arrive3", true);
            yield return new WaitForSeconds(1.2f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive2-2", false);
        }
    }
}
