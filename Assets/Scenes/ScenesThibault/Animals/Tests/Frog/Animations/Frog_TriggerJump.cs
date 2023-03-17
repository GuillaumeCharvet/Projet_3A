using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_TriggerFrogJump : MonoBehaviour
{
    public bool BoolFrogJump;
    public GameObject Frog_TriggerZoneJump;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolFrogJump", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolFrogJump", false);
    }

}
