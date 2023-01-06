using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //float horizontal = ManagerManager.Instance. .GetManager<InputManager>()
        //float vertical = inputManager.VerticalInput;

        //Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);
        //Vector3 transformDirection = (PlayerJumped ? jumpHorizontalBoost : 1f) * transform.TransformDirection(inputDirection);

        //Vector3 flatMovement = moveSpeed * Time.deltaTime * transformDirection;
        //moveDirection = new Vector3(flatMovement.x, moveDirection.y, flatMovement.z);

        //if (PlayerJumped)
        //    moveDirection.y = jumpVerticalBoost;
        //else if (characterController.isGrounded)
        //    moveDirection.y = 0f;
        //else
        //    moveDirection.y -= gravity * Time.deltaTime;

        //if (isInWaterNextFixedUpdate)
        //{
        //    moveDirection.y += forceOfWater * Time.deltaTime;
        //    moveDirection.y *= 0.99f;
        //}
        //else moveDirection.y *= 0.999f;

        //characterController.Move(moveDirection);
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
