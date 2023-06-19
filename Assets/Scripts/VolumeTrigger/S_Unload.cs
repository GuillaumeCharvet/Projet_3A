using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Unload : MonoBehaviour
{
    public GameObject package;

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        package.SetActive(false);
    }

    public void OnTriggerExit(Collider other)
    {
        package.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
