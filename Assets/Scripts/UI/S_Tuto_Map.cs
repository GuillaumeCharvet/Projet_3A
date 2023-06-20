using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Tuto_Map : MonoBehaviour
{
    [SerializeField]
    public GameObject canvaTuto;

    public GameObject triggerTuto;
    private bool tutoOpen = false;
    private bool hasBeenSaid = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenSaid)
        {
            canvaTuto.SetActive(true);
            tutoOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
    void Update()
    {
        if ( !hasBeenSaid && tutoOpen && (Input.GetKeyDown(KeyCode.M) || !hasBeenSaid && tutoOpen &&  Input.GetButtonDown("XboxMenu")))
        {
            canvaTuto.SetActive(false);
            triggerTuto.SetActive(false);
            hasBeenSaid = true;
            tutoOpen = false;
        }
    }
}