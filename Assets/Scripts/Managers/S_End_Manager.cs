using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using Cinemachine;

public class S_End_Manager : MonoBehaviour
{

    [SerializeField] public CinemachineVirtualCamera cam_End;

    public S_Trigger_Read stele1;
    public S_Trigger_Read stele2;
    public S_Trigger_Read stele3;
    public S_Trigger_Read stele4;
    public S_Trigger_Read stele5;

    private bool isActive = false;

    public GameObject readCanva;
    public GameObject daronne;

    public GameObject pop_Up_Canva;
    public float textSpeed;

    public TextMeshProUGUI textComponent;
    private int index;
    public bool isFinished = false;

    public string[] dialoguelines;
    public bool[] dialoguebools;
    public bool[] dialogueHBS;
    private bool dialogueIsActive = false;



    // Update is called once per frame
    void Update()
    {
      if (!readCanva.activeInHierarchy &&
          stele1.hasBeenRead && 
          stele2.hasBeenRead && 
          stele3.hasBeenRead && 
          stele4.hasBeenRead &&
          stele5.hasBeenRead &&
          !isActive
          )
        {
            isActive = true;
            daronne.SetActive(true);
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
            cam_End.Priority = 101;
            pop_Up_Canva.SetActive(true);
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
        pop_Up_Canva.SetActive(true);

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
                pop_Up_Canva.SetActive(false);
                ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = true;
                isFinished = true;
                cam_End.Priority = 0;
            }
        }
    }
}
