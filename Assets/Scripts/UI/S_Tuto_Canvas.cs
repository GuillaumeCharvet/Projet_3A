using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Tuto_Canvas : MonoBehaviour
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerTuto.SetActive(false);
        }
    }
}
