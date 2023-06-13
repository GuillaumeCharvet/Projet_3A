using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using Cinemachine;

public class S_Intro_Dialogue : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera cam5, cam6, cam7, cam8;

    public TextMeshProUGUI textComponent;
    public GameObject TriggerBoxIntro;
    public GameObject triggerBoxTecnho;
    

    public GameObject canvaIntro;
    public GameObject tutoMapBox;
    public float textSpeed;

    private int index;

    public string[] dialoguelines;
    public bool[] dialoguebools;
    public bool[] dialogueHBS;
    private bool dialogueIsActive = false;

    // Update is called once per frame
    void Update()
    {
        if (canvaIntro.activeInHierarchy && !dialogueIsActive)
        {
            dialogueIsActive = true;
            textComponent.text = string.Empty;
            StartDialogue();
            //dialogueIsActive = true;

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

        if (!dialoguebools[6])
        {
            cam6.Priority = 101;
        }
        if (!dialoguebools[7])
        {
            cam6.Priority = 0;
            cam7.Priority = 101;
        }
        if (!dialoguebools[8])
        {
            cam7.Priority = 0;
        }
        if (!dialoguebools[9])
        {
            
            cam8.Priority = 101;
        }
        if (!dialoguebools[10])
        {
            cam8.Priority = 0;
           
        }

        if (!dialoguebools[12])
        {
           
            triggerBoxTecnho.SetActive(true);
            TriggerBoxIntro.SetActive(false);

        }
    }

    void StartDialogue()
    {
        cam5.Priority = 101;
        index = 0;
        var nextBoolFound1 = false;
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
                canvaIntro.SetActive(false);
                ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = true;
                cam5.Priority = 0;
                tutoMapBox.SetActive(true);
            }
        }
    }
}
