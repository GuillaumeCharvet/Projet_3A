using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller_TriggerJump : MonoBehaviour
{
    public bool BoolDrillerJump;
    public GameObject Driller_TriggerZoneJump;
    public Animator animator;

    private float speedRotation = 190f;
    private Quaternion targetRotation;
    private Vector3 targetRotationEuler = Vector3.zero, targetRotationEuler2 = Vector3.zero;

    private void Start()
    {
        animator.Play("Base Layer.A_Driller_Idle_1_V2", 0, Random.Range(0f, 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var playerDirection = other.transform.position - transform.parent.position;
            var fleeingDirection = new Vector3(playerDirection.x, 0f, playerDirection.z);
            targetRotationEuler = fleeingDirection;
            targetRotation = Quaternion.LookRotation(fleeingDirection, transform.parent.up);
            targetRotationEuler2 = targetRotation * Vector3.forward;

            StartCoroutine(StartRotating());
        }
    }

    /*
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.parent.position, transform.parent.position + targetRotationEuler);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.parent.position + Vector3.up * 0.1f, transform.parent.position + Vector3.up * 0.1f + targetRotationEuler2);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.parent.position + Vector3.up * 0.2f, transform.parent.position + Vector3.up * 0.2f + transform.parent.forward);
    }
    */

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") animator.SetBool("BoolDrillerJump", false);
    }

    private IEnumerator StartRotating()
    {
        while (Vector3.Angle(-transform.parent.forward, -(targetRotation * Vector3.forward)) > 5f)
        {
            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, targetRotation, Time.deltaTime * speedRotation);
            yield return new WaitForEndOfFrame();
        }
        animator.SetBool("BoolDrillerJump", true);
    }
}