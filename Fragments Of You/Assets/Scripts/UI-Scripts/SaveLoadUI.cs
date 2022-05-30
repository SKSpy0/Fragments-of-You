using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadUI : MonoBehaviour
{
    public Animator saveSlot_Empty;
    public Animator saveSlot_Active;
    public Animator DarkTransition;

    public void Saved()
    {
        StartCoroutine(SaveWait());
    }

    IEnumerator SaveWait()
    {
        saveSlot_Empty.SetBool("Enter", true);

        yield return new WaitForSeconds(0.5f);

        saveSlot_Active.SetBool("Enter", true);

        yield return new WaitForSeconds(0.5f);

        DarkTransition.SetBool("Enter", true);

        yield return new WaitForSeconds(1.0f);
        
        SceneManager.LoadScene("Level_Select");
    }
}
