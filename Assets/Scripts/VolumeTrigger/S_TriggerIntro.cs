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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkCanva.SetActive(true);
            isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkCanva.SetActive(false);
            dialogueActif = false;
            isIn = false;
        }
    }

    private void Update()
    {
        if (isIn && (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("XboxX")))
        {
            talkCanva.SetActive(false);
            introCanva.SetActive(true);
            dialogueActif = true;
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
        }
    }
}