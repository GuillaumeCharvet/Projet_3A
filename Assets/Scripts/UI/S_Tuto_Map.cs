using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Tuto_Map : MonoBehaviour
{
    [SerializeField]
    // TUTO CLIMB //
    public GameObject canvaTuto;
    public GameObject triggerTuto;
    private bool tutoOpen = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvaTuto.SetActive(true);
            tutoOpen = true;
        }
    }
    private void Update()
    {
        if (tutoOpen && Input.GetKeyDown(KeyCode.M))
        {
            canvaTuto.SetActive(false);
            triggerTuto.SetActive(false);
        }
    }
}

