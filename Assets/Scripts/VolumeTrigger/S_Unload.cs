using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Unload : MonoBehaviour
{
    public GameObject package1;
    public GameObject package2;
    public GameObject package3;
    public GameObject package4;
    public GameObject package5;

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        package1.SetActive(false);
        package2.SetActive(false);
        package3.SetActive(false);
        package4.SetActive(false);
        package5.SetActive(false);
    }

    public void OnTriggerExit(Collider other)
    {
        package1.SetActive(true);
        package2.SetActive(true);
        package3.SetActive(true);
        package4.SetActive(true);
        package5.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
