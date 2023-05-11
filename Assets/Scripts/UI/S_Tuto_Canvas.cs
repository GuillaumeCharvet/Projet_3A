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
    public GameObject playerMovement;
    public GameObject cameraMovement;

    private bool tutoOpen = false;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
            canvaTuto.SetActive(true);
            tutoOpen = true;
            //playerMovement.SetActive(false);
            //cameraMovement.SetActive(false);
            //  Debug.Log("444");
            }
    }
    private void Update()
    {
        if (tutoOpen)
        {
            //Debug.Log("Le tuto est open");
            if (Input.GetMouseButtonDown(0))
            {
                canvaTuto.SetActive(false);
                triggerTuto.SetActive(false);
                //playerMovement.SetActive(true);
                //cameraMovement.SetActive(false);
                //Debug.Log("Click gauche");
            }
        }
    }
}
