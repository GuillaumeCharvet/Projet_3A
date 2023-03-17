using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller_TriggerJump : MonoBehaviour
{
    public bool BoolDrillerJump;
    public GameObject Driller_TriggerZoneJump;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolDrillerJump", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolDrillerJump", false);
    }

}
