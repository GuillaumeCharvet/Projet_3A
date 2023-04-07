using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot_TriggerFly2 : MonoBehaviour
{
    public bool BoolParrotFly;
    public GameObject Parot_TriggerZoneFly;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolParrotFly", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolParrotFly", false);
    }

}
