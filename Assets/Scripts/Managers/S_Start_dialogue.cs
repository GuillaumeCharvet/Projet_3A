using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;


public class S_Start_dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Intro_manager managerIntro;


    public GameObject startCanva;
    public float textSpeed;

    private int index;
    public bool isFinished = false;

    public string[] dialoguelines;
    public bool[] dialoguebools;
    public bool[] dialogueHBS;
    private bool dialogueIsActive = false;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && dialogueIsActive )
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

        dialogueIsActive = true;
        textComponent.text = string.Empty;

        index = 0;
        bool nextBoolFound1 = false;

        int iterations = 0;

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
            }
        }
    }
}
