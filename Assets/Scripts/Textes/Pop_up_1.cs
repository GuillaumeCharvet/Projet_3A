using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class Pop_up_1 : MonoBehaviour
{


    public TextMeshProUGUI textComponent;
    

    [SerializeField] public CinemachineVirtualCamera cam4;


    public GameObject popUpCanva;
    public GameObject thisTriggerBox;
    public float textSpeed;

    private int index;
   

    public string[] dialoguelines;
    public bool[] dialoguebools;
    public bool[] dialogueHBS2;
    private bool dialogueIsActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
            popUpCanva.SetActive(true);
            
            StartDialogue();
            
        }
       
    }

    void Update()
    {
        if (!dialoguebools[0])
        {
            Debug.Log("ouai passe en cam 4 stp");
            cam4.Priority = 100;
        }
        if (!dialoguebools[1])
        {
            cam4.Priority = 0;
        }


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

    
    }

    public void StartDialogue()
{
    

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
    dialogueHBS2[index] = true;
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
            popUpCanva.SetActive(false);
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = true;
            cam4.Priority = 0;
            thisTriggerBox.SetActive(false);
          
        }
    }
}
}