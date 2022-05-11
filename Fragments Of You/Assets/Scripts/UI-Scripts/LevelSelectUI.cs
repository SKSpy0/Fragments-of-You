using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    public Animator LevelMapPink;
    public int level_Count = 0;
    bool waitIsOver = true;

    public void moveFoward()
    {
        if (level_Count != 5 && waitIsOver)
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
            yield return new WaitForSeconds(2.1f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive1-2", false);
        }

        if (level_Count == 1)
        {
            LevelMapPink.SetBool("Arrive1-2", true);
            yield return new WaitForSeconds(2.1f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive1-1", false);
        }

        if (level_Count == 2)
        {
            LevelMapPink.SetBool("Arrive1-3", true);
            yield return new WaitForSeconds(2.1f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive1-2", false);
        }

        if (level_Count == 3)
        {
            LevelMapPink.SetBool("Arrive2-1", true);
            yield return new WaitForSeconds(2.1f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive1-3", false);
        }

        if (level_Count == 4)
        {
            LevelMapPink.SetBool("Arrive2-2", true);
            yield return new WaitForSeconds(2.1f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive2-1", false);
        }

        if (level_Count == 5)
        {
            LevelMapPink.SetBool("Arrive3", true);
            yield return new WaitForSeconds(2.1f);
            waitIsOver = true;
            LevelMapPink.SetBool("Arrive2-2", false);
        }
    }
}
