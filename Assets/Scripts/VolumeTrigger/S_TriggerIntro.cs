using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class S_TriggerIntro : MonoBehaviour
{

    private bool isIn = false;
    private bool dialogueActif;

    public GameObject introCanva;
    public GameObject talkCanva;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkCanva.SetActive(true);
            isIn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkCanva.SetActive(false);
            dialogueActif = false;
            isIn = false;
        }
    }
    void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.F))
        {
            talkCanva.SetActive(false);
            introCanva.SetActive(true);
            dialogueActif = true;
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
        }
    }
}
