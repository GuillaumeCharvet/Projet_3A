using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class StateMachineParameters : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    // PARAMETERS FOR CHECKISGROUNDED
    private float radius;
    [SerializeField] private LayerMask layerMask;
    public float distanceDefiningGroundedState = 0f;//3f;
    public float epsilonCheckGrounded = 0.001f;
    private InputManager inputManager;


    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        inputManager = ManagerManager.Instance.GetManager<InputManager>();

        radius = GetComponent<CharacterController>().radius;
    }

    void Update()
    {
        animator.SetBool("IsGrounded", CheckIsGrounded() || characterController.isGrounded);
        animator.SetBool("PlayerJumped", (characterController.isGrounded || CheckIsGrounded()) && inputManager.IsSpaceJump);
        animator.SetFloat("ForwardSpeed", Vector3.Dot(characterController.velocity, transform.forward));
        Debug.Log("ForwardSpeed : " + Vector3.Dot(characterController.velocity, transform.forward));
        animator.SetFloat("VerticalSpeed", Vector3.Dot(characterController.velocity, Vector3.up));

    }

    public bool CheckIsGrounded()
    {
        //if(characterController.isGrounded) { Debug.Log("mais pourquoi :(((((((((((((((((((((((((((((((((((((((((("); }
        var r = radius;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + 0f * transform.up, -transform.up, out hit, Mathf.Infinity, layerMask))
        {
            var angleSolToVertical = Vector3.Angle(Vector3.up, hit.normal);
            var distAB = r * (1f / Mathf.Cos(2f * Mathf.PI * angleSolToVertical / 360f) - 1f);
            if (hit.distance < distanceDefiningGroundedState + distAB + epsilonCheckGrounded)
            {
                //Debug.Log("**************************************************************");
                //Debug.Log("RAYCAST HIT, hit.distance = " + hit.distance + " distanceDefiningGroundedState + distAB + epsilonCheckGrounded : " + (distanceDefiningGroundedState + distAB + epsilonCheckGrounded));
                return true;
            }
            //Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.yellow);
        }
        return false;
    }
}
