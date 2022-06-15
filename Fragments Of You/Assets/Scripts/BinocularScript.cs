using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BinocularScript : MonoBehaviour
{
    public GameObject target;

    private GameObject mainCam;
    private CinemachineBrain cB;
    public GameObject sectionCam;
    private GameObject player;

    public AudioSource telescopeSFX;

    // Start is called before the first frame update
    void Start()
    {
        //sectionCams = GameObject.FindObjectsOfType<CinemachineConfiner>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        cB = mainCam.GetComponent<CinemachineBrain>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, 0f);
    }

    private GameObject GetActiveVCamObject()
    {
        return cB.ActiveVirtualCamera.VirtualCameraGameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
            sectionCam = GetActiveVCamObject();
            sectionCam.SetActive(false);
            target.SetActive(true);
            telescopeSFX.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, player))
        {
            target.SetActive(false);
            sectionCam.SetActive(true);
        }
    }
}
