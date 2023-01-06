using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineParameters : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    // PARAMETERS FOR CHECKISGROUNDED
    private float radius;
    [SerializeField] private LayerMask layerMask;
    public float distanceDefiningGroundedState = 3f;
    public float epsilonCheckGrounded = 0.001f;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        radius = GetComponent<CapsuleCollider>().radius;
    }

    void Update()
    {
        animator.SetBool("IsGrounded", characterController.isGrounded || CheckIsGrounded());
        animator.SetFloat("ForwardSpeed", Vector3.Dot(characterController.velocity, transform.forward));
        animator.SetFloat("VerticalSpeed", Vector3.Dot(characterController.velocity, Vector3.up));

    }

    public bool CheckIsGrounded()
    {
        var r = radius;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + 0f * transform.up, -transform.up, out hit, Mathf.Infinity, layerMask))
        {
            var angleSolToVertical = Vector3.Angle(Vector3.up, hit.normal);
            var distAB = r * (1f / Mathf.Cos(2f * Mathf.PI * angleSolToVertical / 360f) - 1f);
            if (hit.distance < distanceDefiningGroundedState + distAB + epsilonCheckGrounded)
            {
                return true;
            }
            Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.yellow);
        }
        return false;
    }
}
