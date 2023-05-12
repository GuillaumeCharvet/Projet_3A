using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class S_Update_Map : MonoBehaviour
{
    public GameObject secteurMap;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {      
            secteurMap.SetActive(true);  
        }
    }
}
