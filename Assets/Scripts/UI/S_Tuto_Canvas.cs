using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Tuto_Canvas : MonoBehaviour
{
    [SerializeField]
    public GameObject canvaTutoClimb;
    public GameObject triggerTutoClimb;
    private bool tutoOpen  = false;
   

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
            canvaTutoClimb.SetActive(true); 
                Debug.Log("444");
                tutoOpen = true;
            }
    }
    private void Update()
    {
        if (tutoOpen)
        {
            Debug.Log("Le tuto est open");
            if (Input.GetMouseButtonDown(0))
            {
                canvaTutoClimb.SetActive(false);
                triggerTutoClimb.SetActive(false);
                Debug.Log("Click gauche");
            }
        }
    }
}
