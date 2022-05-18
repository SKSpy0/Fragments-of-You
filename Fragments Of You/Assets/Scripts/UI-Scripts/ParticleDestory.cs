using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestory : MonoBehaviour
{
    public float destoryCD;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ParticalDestoryWait());
    }

    IEnumerator ParticalDestoryWait()
    {
        yield return new WaitForSeconds(destoryCD);

        Destroy(this.gameObject);
    }
}
