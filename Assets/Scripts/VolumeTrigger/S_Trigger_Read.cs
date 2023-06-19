using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Trigger_Read : MonoBehaviour
{
    public GameObject TrigerBoxRead;

    public bool readIsOuvert = false;
    public bool lectureIsOuvert = false;

    public GameObject canvaRead;
    public GameObject canvaLecture;
    public float textSpeed;

    private int index;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvaRead.SetActive(true);
            readIsOuvert = true;      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvaRead.SetActive(false);
            readIsOuvert = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (readIsOuvert && Input.GetKeyDown(KeyCode.F))
        {
            canvaRead.SetActive(false);
            readIsOuvert = false;
            canvaLecture.SetActive(true);
            lectureIsOuvert = true;
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
        }
        if (lectureIsOuvert && Input.GetMouseButtonDown(0))
        {      
            canvaLecture.SetActive(false);
            lectureIsOuvert = false;
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = true; ;
        }
    }
}
