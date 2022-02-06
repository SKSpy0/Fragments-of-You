using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink_KeyInput : MonoBehaviour
{
    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseMenu.isPause)
            {
                pauseMenu.unpause();
            }
            else
            {
                pauseMenu.pause();
            }
        }
    }
}
