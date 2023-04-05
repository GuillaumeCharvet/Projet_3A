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

    private bool tutoClimbOpen = false;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
            canvaTuto.SetActive(true); 
               //  Debug.Log("444");
                tutoClimbOpen = true;
            }
    }
    private void Update()
    {
        if (tutoClimbOpen)
        {
            //Debug.Log("Le tuto est open");
            if (Input.GetMouseButtonDown(0))
            {
                canvaTuto.SetActive(false);
                triggerTuto.SetActive(false);
                //Debug.Log("Click gauche");
            }
        }
    }
}
