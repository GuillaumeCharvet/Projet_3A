using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Intro_manager : MonoBehaviour
{

    [SerializeField] public CinemachineVirtualCamera cam1, cam2, cam3;

    public S_Start_dialogue dialogue;

    private void Start()
    {
          StartCoroutine(DelayedStart());
    }



    IEnumerator DelayedStart()
    {
        yield return new WaitForSecondsRealtime(3);

        cam1.Priority = 0;
        cam2.Priority = 101;

        yield return new WaitForSecondsRealtime(2);
        dialogue.StartDialogue();
    }
}

