using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideBehaviour : StateMachineBehaviour
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
        sMP.currentModeMovement = ModeMovement.Glide;
        inputManager = ManagerManager.Instance.GetManager<InputManager>();
        playerParameters = animator.GetComponent<MovementParameters>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float horizontal = inputManager.HorizontalInput;
        float vertical = inputManager.VerticalInput;

        /*
        var targetDirection = new Vector3(moveDirection.x, (-3f * vertical) * Mathf.Sqrt(Vector2.SqrMagnitude(new Vector2(moveDirection.x, moveDirection.z))), moveDirection.z);

        moveDirection = Vector3.RotateTowards(moveDirection, Vector3.Magnitude(moveDirection) * Vector3.Normalize(targetDirection), gliderRotationSpeed, 0f);
        //moveDirection = Quaternion.RotateTowards(moveDirection, targetDirection, 0.5f) * moveDirection;

        //if (vertical >= 0.1f) moveDirection *= 1.005f;
        //if (vertical <= -0.1f) moveDirection *= 0.99f;

        moveDirection.y -= (1.05f - Mathf.Abs(Vector3.Dot(Vector3.up, transform.TransformDirection(Vector3.forward)))) * gravity * Time.deltaTime;

        transform.rotation = Quaternion.FromToRotation(transform.TransformDirection(Vector3.forward), moveDirection) * transform.rotation;

        if (Mathf.Abs(inputManager.VerticalInput) > 0.1f)
        {
            gliderRotationSpeed = Mathf.Min(gliderRotationSpeed + gliderRotationAcceleration * Time.deltaTime * 60f, maxGliderRotationSpeed);
        }
        else gliderRotationSpeed = Mathf.Max(gliderRotationSpeed - 0.5f * gliderRotationAcceleration * Time.deltaTime * 60f, 0f);
        */

        if (horizontal > 0.1f)
        {
            playerParameters.gliderTurnSpeed = Mathf.Min(playerParameters.gliderTurnSpeed + playerParameters.gliderTurnAcceleration * Time.deltaTime * 60f, playerParameters.maxGliderTurnSpeed);
        }
        else if (horizontal < -0.1f)
        {
            playerParameters.gliderTurnSpeed = Mathf.Max(playerParameters.gliderTurnSpeed - playerParameters.gliderTurnAcceleration * Time.deltaTime * 60f, -playerParameters.maxGliderTurnSpeed);
        }
        else if (playerParameters.gliderTurnSpeed <= 0f)
        {
            playerParameters.gliderTurnSpeed = Mathf.Min(playerParameters.gliderTurnSpeed + 0.5f * playerParameters.gliderTurnAcceleration * Time.deltaTime * 60f, 0f);
        }
        else if (playerParameters.gliderTurnSpeed >= 0f)
        {
            playerParameters.gliderTurnSpeed = Mathf.Max(playerParameters.gliderTurnSpeed - 0.5f * playerParameters.gliderTurnAcceleration * Time.deltaTime * 60f, 0f);
        }

        if (horizontal > 0.1f)
        {
            playerParameters.gliderCameraTurnSpeed = Mathf.Min(playerParameters.gliderCameraTurnSpeed + 0.2f * playerParameters.gliderTurnAcceleration * Time.deltaTime * 60f, 3f * playerParameters.maxGliderTurnSpeed);
        }
        else if (horizontal < -0.1f)
        {
            playerParameters.gliderCameraTurnSpeed = Mathf.Max(playerParameters.gliderCameraTurnSpeed - 0.2f * playerParameters.gliderTurnAcceleration * Time.deltaTime * 60f, 3f * -playerParameters.maxGliderTurnSpeed);
        }
        else if (playerParameters.gliderCameraTurnSpeed <= 0f)
        {
            playerParameters.gliderCameraTurnSpeed = Mathf.Min(playerParameters.gliderCameraTurnSpeed + 0.5f * 0.2f * playerParameters.gliderTurnAcceleration * Time.deltaTime * 60f, 0f);
        }
        else if (playerParameters.gliderCameraTurnSpeed >= 0f)
        {
           playerParameters.gliderCameraTurnSpeed = Mathf.Max(playerParameters.gliderCameraTurnSpeed - 0.5f * 0.2f * playerParameters.gliderTurnAcceleration * Time.deltaTime * 60f, 0f);
        }

        /*
        if (vertical > 0.1f)
        {
            gliderRotationSpeed = Mathf.Min(gliderRotationSpeed + gliderRotationAcceleration * Time.deltaTime * 60f, maxGliderRotationSpeed);
        }
        else if (vertical < -0.1f)
        {
            gliderRotationSpeed = Mathf.Max(gliderRotationSpeed - gliderRotationAcceleration * Time.deltaTime * 60f, -maxGliderRotationSpeed);
        }
        else if (gliderRotationSpeed <= 0f)
        {
            gliderRotationSpeed = Mathf.Min(gliderRotationSpeed + 0.5f * gliderRotationAcceleration * Time.deltaTime * 60f, 0f);
        }
        else if (gliderRotationSpeed >= 0f)
        {
            gliderRotationSpeed = Mathf.Max(gliderRotationSpeed - 0.5f * gliderRotationAcceleration * Time.deltaTime * 60f, 0f);
        }
        */

        /*
        if (vertical > 0.1f)
        {
            angleDiff = vertical * gliderRotationAcceleration;
        }

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -gliderCameraTurnSpeed * 8f);

        var quaternion = Quaternion.AngleAxis(-20f - angleDiff, Vector3.right);

        direction = quaternion * (10f * Vector3.ProjectOnPlane(transform.forward, Vector3.up));
        */

        // VERSION A1

        /*
        if (horizontal > 0.1f)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + 0.6f, transform.localRotation.eulerAngles.z - 0.4f);
            direction = Quaternion.Euler(0f, 0.6f, -0.4f) * direction;
        }
        else if (horizontal < -0.1f)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y - 0.6f, transform.localRotation.eulerAngles.z + 0.4f);
            direction = Quaternion.Euler(0f, -0.6f, 0.4f) * direction;
        }

        if (vertical > 0.1f)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x + 0.1f, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }
        else if (vertical < -0.1f)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x - 0.1f, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }

        frictionForce = -frictionParameter * Vector3.Dot(direction, transform.forward) * Vector3.Dot(direction, transform.forward) * transform.forward;

        liftForce = liftParameter * Vector3.Dot(direction, transform.forward) * Vector3.Dot(direction, transform.forward) * transform.up;

        // mass * a = sum of the forces
        // v(t + dt) - v(t) = forces/mass * dt
        // v(n+1) = v(n) + forces/mass * dt

        direction += (frictionForce + liftForce + weightForce) / mass;

        Debug.DrawRay(transform.position, direction, Color.red, 0f);

        moveDirection = direction * Time.deltaTime;
        characterController.Move(moveDirection);
        //speed = direction.magnitude;
        */

        // VERSION A2

        Debug.Log("transform.localRotation.eulerAngles.z = " + playerParameters.transform.localRotation.eulerAngles.z);

        var eulerZ = playerParameters.transform.localRotation.eulerAngles.z > 180 ? playerParameters.transform.localRotation.eulerAngles.z - 360 : playerParameters.transform.localRotation.eulerAngles.z;
        if (horizontal > 0.1f)
        {
            playerParameters.transform.localRotation = Quaternion.Euler(playerParameters.transform.localRotation.eulerAngles.x, playerParameters.transform.localRotation.eulerAngles.y + 1.4f, Mathf.Max(eulerZ - 0.4f, -15f));
        }
        else if (horizontal < -0.1f)
        {
            playerParameters.transform.localRotation = Quaternion.Euler(playerParameters.transform.localRotation.eulerAngles.x, playerParameters.transform.localRotation.eulerAngles.y - 1.4f, Mathf.Min(eulerZ + 0.4f, +15f));
        }
        else if (eulerZ > 0.2f)
        {
            playerParameters.transform.localRotation = Quaternion.Euler(playerParameters.transform.localRotation.eulerAngles.x, playerParameters.transform.localRotation.eulerAngles.y, eulerZ - 0.4f);
        }
        else if (eulerZ < -0.2f)
        {
            playerParameters.transform.localRotation = Quaternion.Euler(playerParameters.transform.localRotation.eulerAngles.x, playerParameters.transform.localRotation.eulerAngles.y, eulerZ + 0.4f);
        }

        if (vertical > 0.1f && playerParameters.speed > 1f)
        {
            var eulerX = playerParameters.transform.localRotation.eulerAngles.x > 180 ? playerParameters.transform.localRotation.eulerAngles.x - 360 : playerParameters.transform.localRotation.eulerAngles.x;
            playerParameters.transform.localRotation = Quaternion.Euler(Mathf.Min(eulerX + 0.8f, 45f), playerParameters.transform.localRotation.eulerAngles.y, playerParameters.transform.localRotation.eulerAngles.z);
        }
        else if (vertical < -0.1f)
        {
            var eulerX = playerParameters.transform.localRotation.eulerAngles.x > 180 ? playerParameters.transform.localRotation.eulerAngles.x - 360 : playerParameters.transform.localRotation.eulerAngles.x;
            playerParameters.transform.localRotation = Quaternion.Euler(Mathf.Max(eulerX - 0.8f, -45f), playerParameters.transform.localRotation.eulerAngles.y, playerParameters.transform.localRotation.eulerAngles.z);
        }

        var acceleration = 2f * Mathf.Cos(Mathf.Deg2Rad * Vector3.SignedAngle(playerParameters.transform.forward, Vector3.down, playerParameters.transform.right));
        Debug.Log("acceleration = " + acceleration);

        playerParameters.speed = Mathf.Max(Mathf.Min(playerParameters.speed + acceleration, 100f), 0f);

        playerParameters.transform.localRotation = Quaternion.Euler(playerParameters.transform.localRotation.eulerAngles.x + (1f / (1f + playerParameters.speed * playerParameters.speed)) * 3f, playerParameters.transform.localRotation.eulerAngles.y, playerParameters.transform.localRotation.eulerAngles.z);

        playerParameters.moveDirection = playerParameters.speed * (playerParameters.transform.forward + 0.1f * Vector3.down) * Time.deltaTime;
        playerParameters.characterController.Move(playerParameters.moveDirection);
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
