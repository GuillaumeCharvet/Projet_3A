using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_manager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera cam1, cam2, cam3;

    public S_Start_dialogue dialogue;
    public GameObject TechnoCharacter;
    public Animator animator;
    private bool firstCheckUp = true;

    private void Start()
    {
        cam1.Priority = 99;
        StartCoroutine(DelayedStart());
        ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
    }

    private void Update()
    {
        if (dialogue.dialogueHBS[1])
        {
            cam2.Priority = 0;
            cam3.Priority = 99;
        }
        if (!dialogue.dialoguebools[3])
        {
            if (firstCheckUp) animator.SetTrigger("CheckUp");
            cam3.Priority = 0;
            cam2.Priority = 99;
            firstCheckUp = false;
        }
        if (dialogue.dialogueHBS[6])
        {
            cam2.Priority = 0;
            cam1.Priority = 0;
            TechnoCharacter.SetActive(false);
        }
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSecondsRealtime(3f);
        animator.SetTrigger("CheckUp");
        yield return new WaitForSecondsRealtime(0.75f);

        cam1.Priority = 0;
        cam2.Priority = 99;

        yield return new WaitForSecondsRealtime(3f);
        dialogue.StartDialogue();
    }
}