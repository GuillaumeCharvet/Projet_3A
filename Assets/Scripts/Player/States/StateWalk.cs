using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="states/walk", fileName ="new Wallk")]
public class StateWalk : StateBehaviourParent
{
    public float speed;

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void Update()
    {
        
        float horizontal = reader.inputManager.HorizontalInput;
        float vertical = reader.inputManager.VerticalInput;
        /*
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
        */
        reader.mp.Move(speed);

        base.Update();
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
    }
}
