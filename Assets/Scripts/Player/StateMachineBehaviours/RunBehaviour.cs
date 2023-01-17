using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBehaviour : StateMachineBehaviour
{
    private InputManager inputManager;
    private MovementParameters playerParameters;
    private StateMachineParameters sMP;

    private void Awake()
    {
        sMP = FindObjectOfType<StateMachineParameters>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sMP.currentModeMovement = ModeMovement.Run;
        inputManager = ManagerManager.Instance.GetManager<InputManager>();
        playerParameters = animator.GetComponent<MovementParameters>();

        playerParameters.currentClimbStamina = playerParameters.maxClimbStamina;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (sMP.currentModeMovement == ModeMovement.Run)
        {
            float horizontal = inputManager.HorizontalInput;
            float vertical = inputManager.VerticalInput;

            Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);
            Vector3 transformDirection = (animator.GetBool("PlayerJumped") ? playerParameters.jumpHorizontalBoost : 1f) * animator.transform.TransformDirection(inputDirection);

            //Debug.Log("transformDirection : " + transformDirection);

            Vector3 flatMovement = playerParameters.moveSpeed * Time.deltaTime * transformDirection;
            playerParameters.moveDirection = new Vector3(flatMovement.x, playerParameters.moveDirection.y, flatMovement.z);

            if (animator.GetBool("PlayerJumped"))
                playerParameters.moveDirection.y = playerParameters.jumpVerticalBoost;
            else if (playerParameters.characterController.isGrounded)
                playerParameters.moveDirection.y = 0f;
            else
                playerParameters.moveDirection.y -= playerParameters.gravity * Time.deltaTime;

            if (playerParameters.isInWaterNextFixedUpdate)
            {
                playerParameters.moveDirection.y += playerParameters.forceOfWater * Time.deltaTime;
                playerParameters.moveDirection.y *= 0.99f;
            }
            else playerParameters.moveDirection.y *= 0.999f;

            //Debug.Log("playerParameters.moveDirection : " + playerParameters.moveDirection);

            playerParameters.characterController.Move(playerParameters.moveDirection);

            // Horizontal player rotation
            animator.transform.localRotation = Quaternion.Euler(animator.transform.rotation.eulerAngles + playerParameters.sensitivityH * inputManager.MouseXInput * Time.deltaTime * 100f * Vector3.up);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
