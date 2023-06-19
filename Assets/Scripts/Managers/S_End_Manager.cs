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


    public TextMeshProUGUI textComponent;
    public string[] dialoguelines;
    public bool[] dialoguebools;
    public bool[] dialogueHBS; // dialogue has been said
    public float textSpeed;

    private int index;

    public GameObject pop_Up_Canva;



    public string[] lines;
    public bool[] bools;
    public bool[] dialogueHBS2;
    private bool dialogueIsActive = false;

    // Update is called once per frame
    void Update()
    {
      if (stele1.hasBeenRead && 
          stele2.hasBeenRead && 
          stele3.hasBeenRead && 
          stele4.hasBeenRead &&
          stele5.hasBeenRead )
        {
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
            cam_End.Priority = 101;
            dialogue();
        }
        

    }
    void dialogue()
    {
        index = 0;
        var nextBoolFound = false;
        while (!nextBoolFound)
        {
            if (index < dialoguebools.Length - 1)
            {
                if (!dialoguebools[index])
                {
                    index++;
                }
                else
                {
                    nextBoolFound = true;
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
        //index = 0;
        //dialoguebools[index] = false;
        dialogueHBS[index] = true;
        bool nextLineFound = false;
        while (!nextLineFound)
        {
            dialoguebools[index] = false;
            if (index < dialoguelines.Length - 1)
            {
                Debug.Log("ducoup le bool passe faux là");
                if (dialoguebools[index + 1] && !dialogueHBS[index + 1])
                {
                    dialogueHBS[index + 1] = true;
                    index++;

                    textComponent.text = string.Empty;
                    StartCoroutine(Typeline());
                    nextLineFound = true;                
                }
                else
                {                
                    index++;
                }
            }
            else
            {
                textComponent.text = string.Empty;
                nextLineFound = true;
                dialogueIsActive = false;
                bools[0] = true;
                index = 0;
                Debug.Log("index à 0");
                pop_Up_Canva.SetActive(false);
                cam_End.Priority = 0;

                ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = true;

                



            }
        }
    }
}
