using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink_KeyInput : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Animator mouseFade;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.GetComponent<PauseMenu>();
        mouseFade = GameObject.Find("MouseCursor").GetComponent<Animator>();
        mouseFade.SetBool("Fade", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseMenu.isPause)
            {
                mouseFade.SetBool("Fade", true);
                pauseMenu.unpause();
            }
            else
            {
                mouseFade.SetBool("Fade", false);
                pauseMenu.pause();
            }
        }
    }
}
