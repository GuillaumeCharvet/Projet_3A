using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Intro_manager : MonoBehaviour
{

    [SerializeField] public CinemachineVirtualCamera cam1, cam2, cam3;

    public S_Start_dialogue dialogue;
    public GameObject TechnoCharacter;

    private void Start()
    {
          cam1.Priority = 101;
          StartCoroutine(DelayedStart());
         ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
    }

    private void Update()
    {
        if (dialogue.dialogueHBS[1])
        {
            cam2.Priority = 0;
            cam3.Priority = 101;
        }
        if (dialogue.dialogueHBS[3])
        {
            cam3.Priority = 0;
            cam2.Priority = 101;
        }
        if (dialogue.dialogueHBS[6])
        {
            cam2.Priority = 0;
            cam1.Priority = 0;
            TechnoCharacter.SetActive(false);
        }
    }



    IEnumerator DelayedStart()
    {
        yield return new WaitForSecondsRealtime(4);

        cam1.Priority = 0;
        cam2.Priority = 101;

        yield return new WaitForSecondsRealtime(3);
        dialogue.StartDialogue();
    }
}

