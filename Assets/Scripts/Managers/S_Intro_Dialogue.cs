using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class S_Intro_Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject triggerBoxTecnho;
    public GameObject triggerBoxNature;

    public GameObject canvaIntro;
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
    }

    void StartDialogue()
    {
        index = 0;
        var nextBoolFound1 = false;
        while (!nextBoolFound1)
        {
            if (index < dialoguelines.Length - 1)
            {
                if (!dialoguebools[index])
                {
                    dialogueHBS[index + 1] = true;
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
                if (dialoguebools[index + 1] && !dialogueHBS[index + 1])
                {
                    dialogueHBS[index + 1] = true;
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
            }
        }
    }
}
