using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller_TriggerStress : MonoBehaviour
{
    public bool BoolDrillerStress;
    public GameObject Driller_TriggerZoneStress;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolDrillerStress", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolDrillerStress", false);
    }

}
