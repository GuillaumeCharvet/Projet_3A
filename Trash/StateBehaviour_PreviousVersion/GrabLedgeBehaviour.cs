using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrabLedgeBehaviour : StateMachineBehaviour
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
        sMP.currentModeMovement = ModeMovement.GrabLedge;
        inputManager = ManagerManager.Instance.GetManager<InputManager>();
        playerParameters = animator.GetComponent<MovementParameters>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (sMP.currentModeMovement == ModeMovement.GrabLedge)
        {
            float horizontal = inputManager.HorizontalInput;
            float vertical = inputManager.VerticalInput;

            Vector3 inputDirection = new Vector3(horizontal, vertical, 0f);
            Vector3 transformDirection = (animator.GetBool("PlayerJumped") ? playerParameters.jumpHorizontalBoost : 1f) * animator.transform.TransformDirection(inputDirection).normalized;

            Vector3 flatMovement = playerParameters.climbSpeed * Time.deltaTime * transformDirection;

            //transform.rotation = Quaternion.FromToRotation(transform.TransformDirection(Vector3.forward), -modeMovementManagement.currentNormalToClimb) * transform.rotation;

            playerParameters.moveDirection = new Vector3(0f, flatMovement.y, 0f);

            playerParameters.currentClimbStamina -= playerParameters.moveDirection.magnitude;
            playerParameters.characterController.Move(playerParameters.moveDirection);
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
