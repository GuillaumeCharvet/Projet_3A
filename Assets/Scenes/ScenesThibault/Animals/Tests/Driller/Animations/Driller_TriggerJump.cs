using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller_TriggerJump : MonoBehaviour
{
    public bool BoolJump;
    public GameObject TriggerZoneJump;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolJump", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolJump", false);
    }

}
