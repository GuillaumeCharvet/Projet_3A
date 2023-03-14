using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller_TriggerStress : MonoBehaviour
{
    public bool BoolStress;
    public GameObject TriggerZoneStress;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolStress", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolStress", false);
    }

}
