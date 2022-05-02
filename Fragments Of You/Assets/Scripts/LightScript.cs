using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public float startRange = 1.0f;
    public float endRange = 24f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var oscilationRange = (endRange - startRange) / 2;
        var oscilationOffset = oscilationRange + startRange;
        var result = oscilationOffset + Mathf.Sin(Time.time) * oscilationRange;
        transform.rotation = Quaternion.Euler(Vector3.forward * result);
    }
}
