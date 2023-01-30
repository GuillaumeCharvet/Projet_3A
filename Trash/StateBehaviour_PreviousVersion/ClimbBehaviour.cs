using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClimbBehaviour : StateMachineBehaviour
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
        sMP.currentModeMovement = ModeMovement.Climb;
        inputManager = ManagerManager.Instance.GetManager<InputManager>();
        playerParameters = animator.GetComponent<MovementParameters>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerParameters.currentClimbStamina > 0f)
        {
            float horizontal = inputManager.HorizontalInput;
            float vertical = inputManager.VerticalInput;

            Vector3 inputDirection = horizontal * Vector3.right + vertical * Vector3.up;
            Vector3 transformDirection = (animator.GetBool("PlayerJumped") ? playerParameters.jumpHorizontalBoost : 1f) * animator.transform.TransformDirection(inputDirection).normalized;
            Vector3 flatMovement = playerParameters.climbSpeed * Time.deltaTime * transformDirection;

            playerParameters.currentClimbStamina -= flatMovement.magnitude;

            Debug.Log("distanceToWall = " + sMP.distanceToGrabbedWall);
            if (sMP.distanceToGrabbedWall > sMP.distanceToGrabbedWallLimit)
            {
                inputDirection += Vector3.forward;
                transformDirection = (animator.GetBool("PlayerJumped") ? playerParameters.jumpHorizontalBoost : 1f) * animator.transform.TransformDirection(inputDirection).normalized;
                Debug.Log("direction actuelle : " + transformDirection);
                flatMovement = playerParameters.climbSpeed * Time.deltaTime * transformDirection;
            }

            Debug.DrawRay(animator.transform.position, transformDirection);

            animator.transform.rotation = Quaternion.FromToRotation(animator.transform.TransformDirection(Vector3.forward), -playerParameters.currentNormalToClimb) * animator.transform.rotation;

            Debug.Log("flatMovement : " + flatMovement);

            playerParameters.moveDirection = flatMovement; //new Vector3(flatMovement.x, flatMovement.y, flatMovement.z);

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
