using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Trigger_Intro_cam : MonoBehaviour
{
    public bool isEnterOnce = false;

   



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            isEnterOnce = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
