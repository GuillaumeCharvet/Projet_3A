using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Destroy_Tuto : MonoBehaviour
{
    
    public GameObject tutoDegage;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            tutoDegage.SetActive(false);
        }
    }
}
