using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Trigger_Read : MonoBehaviour
{
    public GameObject TrigerBoxRead;

    public bool readIsOuvert = false;
    public bool lectureIsOuvert = false;
    public bool hasBeenRead = false;

    public GameObject canvaRead;
    public GameObject canvaLecture;
    public GameObject textShow;

    private void OnTriggerEnter(Collider other)
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
    private void Update()
    {
        if (readIsOuvert && (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("XboxX")))
        {
            canvaRead.SetActive(false);
            readIsOuvert = false;
            canvaLecture.SetActive(true);
            lectureIsOuvert = true;
            textShow.SetActive(true);
           
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
        }
        if (lectureIsOuvert && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("XboxA")))
        {
            canvaLecture.SetActive(false);
            lectureIsOuvert = false;
            textShow.SetActive(false);
            hasBeenRead = true;
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = true; ;
        }
    }
}