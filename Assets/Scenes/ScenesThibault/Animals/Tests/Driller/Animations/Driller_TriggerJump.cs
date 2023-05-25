using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller_TriggerJump : MonoBehaviour
{
    public bool BoolDrillerJump;
    public GameObject Driller_TriggerZoneJump;
    public Animator animator;

    private Vector3 targetRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            targetRotation = Vector3.zero;
        }
        animator.SetBool("BoolDrillerJump", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolDrillerJump", false);
    }

    private IEnumerator StartRotating()
    {
        var t = 0f;
        while (true)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}