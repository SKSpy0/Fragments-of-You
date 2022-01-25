using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Condition : MonoBehaviour
{
    public Animator GameOverMenu;
    public GameObject GameOver_obj;

    void Start()
    {
        GameOver_obj.SetActive(false);
    }

    public void Game_Over()
    {
        GameOver_obj.SetActive(true);
        GameOverMenu.SetBool("Enter", true);
    }

    
}