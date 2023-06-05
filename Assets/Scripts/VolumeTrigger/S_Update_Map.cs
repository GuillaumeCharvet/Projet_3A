using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class S_Update_Map : MonoBehaviour
{
    public GameObject secteurMapAffiche;
    public GameObject cacheSecteur;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {      
            secteurMapAffiche.SetActive(true);
            cacheSecteur.SetActive(false);
        }
    }
}
