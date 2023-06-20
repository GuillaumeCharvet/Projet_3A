using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using Cinemachine;


public class S_Last_dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject canvaTalk;
    [SerializeField] public CinemachineVirtualCamera cam_LastDialogue;




    public GameObject startCanva;
    public float textSpeed;

    private bool isIn = false;

    private int index;
    public bool isFinished = false;

    public string[] dialoguelines;
    public bool[] dialoguebools;
    public bool[] dialogueHBS;
    private bool dialogueIsActive = false;

    // Update is called once per frame

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvaTalk.SetActive(true);
            isIn = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvaTalk.SetActive(false);
            isIn = false;
        }
    }
    void Update()
    {
        if (isIn && (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("XboxX")))
        {
            canvaTalk.SetActive(false);
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
            StartDialogue();
        }



            if (Input.GetMouseButtonDown(0) && dialogueIsActive)
        {
            if (textComponent.text == dialoguelines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialoguelines[index];
            }
        }

        /*
        if (!dialoguebools[6])
        {
            triggerBoxNature.SetActive(true);
            triggerBoxTecnho.SetActive(true);
            TriggerBoxIntro.SetActive(false);

        }
        */
    }

    public void StartDialogue()
    {
        startCanva.SetActive(true);
        cam_LastDialogue.Priority = 101;

        dialogueIsActive = true;
        textComponent.text = string.Empty;

        index = 0;
        bool nextBoolFound1 = false;



        while (!nextBoolFound1)
        {
            if (index < dialoguelines.Length - 1)
            {
                if (!dialoguebools[index])
                {
                    textComponent.text = string.Empty;
                    StartCoroutine(Typeline());
                    index++;
                }
                else
                {
                    nextBoolFound1 = true;
                }
            }


        }
        StartCoroutine(Typeline());
    }
    IEnumerator Typeline()
    {
        yield return new WaitForSecondsRealtime(1);
        foreach (char c in dialoguelines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        dialogueHBS[index] = true;
        bool nextLineFound2 = false;
        while (!nextLineFound2)
        {
            //index = 0;
            dialoguebools[index] = false;
            if (index < dialoguelines.Length - 1)
            {
                if (dialoguebools[index + 1])
                {
                    dialoguebools[index] = false;
                    index++;
                    textComponent.text = string.Empty;
                    StartCoroutine(Typeline());
                    nextLineFound2 = true;
                }
                else
                {
                    index++;
                }
            }
            else
            {
                textComponent.text = string.Empty;
                nextLineFound2 = true;
                dialogueIsActive = false;
                startCanva.SetActive(false);
                ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = true;
                isFinished = true;
                cam_LastDialogue.Priority = 0;

            }
        }
    }
}
