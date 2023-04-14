using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_GetRewards : MonoBehaviour
{
   [SerializeField]
    // TUTO CLIMB //
    public GameObject pickableObject;
    public GameObject buttonGet;
    private bool pickable = false;
    private bool picked = false;
    // Start is called before the first frame update
   
        void OnTriggerEnter(Collider other )
        {
            if (other.CompareTag("Player") && !picked)
            {
            Debug.Log("est rentr� dans le collider");
            buttonGet.SetActive(true);
                pickable = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !picked)
            {
                Debug.Log("sort du collider");
                buttonGet.SetActive(false);
                pickable = false;            
            }
        }

    // Update is called once per frame
    void Update()
    {
        if(pickable && Input.GetKeyDown(KeyCode.F))
        {
            pickableObject.SetActive(false);
            buttonGet.SetActive(false);
            picked = true;
            pickable = false;
        }
    }
}