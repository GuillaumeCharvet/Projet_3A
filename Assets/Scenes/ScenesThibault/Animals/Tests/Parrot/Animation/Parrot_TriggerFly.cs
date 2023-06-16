using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot_TriggerFly : MonoBehaviour
{
    public bool BoolParrotFly;
    public GameObject Parot_TriggerZoneFly;
    public Animator animator;

    private void Start()
    {
        animator.Play("Base Layer.A_Parrot_Idle", 0, Random.Range(0f, 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetInteger("ParrotFlyRandom", Random.Range(0, 100));
        animator.SetBool("BoolParrotFly", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolParrotFly", false);
    }
}