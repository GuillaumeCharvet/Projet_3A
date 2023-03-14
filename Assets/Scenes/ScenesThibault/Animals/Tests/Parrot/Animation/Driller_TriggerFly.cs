using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller_TriggerFly : MonoBehaviour
{
    public bool BoolFly;
    public GameObject TriggerZoneFly;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolFly", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolFly", false);
    }

}
