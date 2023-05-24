using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_Enable_Lights : MonoBehaviour



{
    [SerializeField]
    // TUTO CLIMB //
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;
    public GameObject gameObject4;
    public GameObject gameObject5;





    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject1.SetActive(true);
            gameObject2.SetActive(true); 
            gameObject3.SetActive(true);
            gameObject4.SetActive(true);
            gameObject5.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject1.SetActive(false);
            gameObject2.SetActive(false);
            gameObject3.SetActive(false);
            gameObject4.SetActive(false);
            gameObject5.SetActive(false);
        }
    }
}
