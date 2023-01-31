using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public enum ModeMovement { Idle, Walk, Run, Jump, Swim, Climb, Slide, Glide, GrabLedge, Hang, Fall };

public class StateMachineParameters : MonoBehaviour
{
    private Animator animator;
    private MovementParameters playerParameters;
    private CharacterController characterController;
    private InputManager inputManager;

    [SerializeField] private Transform camTrsf;
    private Vector3 displacement = Vector3.zero;

    public ModeMovement currentModeMovement;

    // PARAMETERS FOR CHECKISGROUNDED
    private float radius;
    [SerializeField] private LayerMask layerMask;
    public float distanceDefiningGroundedState = 0f;//3f;
    public float epsilonCheckGrounded = 0.001f;

    // PARAMETERS FOR CHECKIFCLIMBINGTOPTOBOT
    private float grabToClimbDistance = 2f;
    private float grabToHangDistance = 1.8f;
    public float distanceToGrabbedWall = 0f;
    public float distanceToGrabbedWallLimit = 0.5f;
    public bool middleOfClimbing = false;

    [Header("RUN/JUMP")]

    [SerializeField] private float maxSpeed = 10f;
    public float MaxSpeed { get => maxSpeed; }

    [SerializeField] public float jumpVerticalBoost = 0.4f;
    [SerializeField] public float jumpHorizontalBoost = 1f;

    [Header("CLIMB")]
    [SerializeField] public float climbSpeed = 2.5f;
    public float maxClimbStamina = 10f;
    public float currentClimbStamina = 0f;
    public Vector3 currentNormalToClimb;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        inputManager = ManagerManager.Instance.GetManager<InputManager>();
        playerParameters = GetComponent<MovementParameters>();

        radius = GetComponent<CharacterController>().radius;
    }

    void Update()
    {
        UpdateIsGrounded();

        UpdateInputValue();
        animator.SetFloat("VerticalSpeed", Vector3.Dot(characterController.velocity, Vector3.up));
        animator.SetFloat("ForwardSpeed", (characterController.velocity.x * Vector3.right + characterController.velocity.z * Vector3.forward).magnitude);//Vector3.Dot(characterController.velocity, transform.forward));

        /*
        //animator.SetBool("PlayerJumped", (characterController.isGrounded || CheckIsGrounded()) && inputManager.IsSpaceJump);

        //Debug.Log("ForwardSpeed : " + (characterController.velocity.x * Vector3.right + characterController.velocity.z * Vector3.forward).magnitude);

        */
    }

    void FixedUpdate()
    {
        animator.SetBool("PlayerJumped", (characterController.isGrounded || CheckIsGrounded()) && inputManager.IsSpaceJump);
        animator.SetBool("PlayerStartGlide", inputManager.IsSpaceJump);
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

            //Debug.Log("**************************************************************");
            //Debug.Log("RAYCAST HIT, hit.distance = " + hit.distance + " distanceDefiningGroundedState + distAB + epsilonCheckGrounded : " + (distanceDefiningGroundedState + distAB + epsilonCheckGrounded));

            if (hit.distance < distanceDefiningGroundedState + distAB + epsilonCheckGrounded)
            {
                return true;
            }
            //Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.yellow);
        }
        else
        {
            //Debug.Log("**************************************************************");
            //Debug.Log("RAYCAST DOESNT HIT");
        }
        return false;
    }
    public bool CheckIfClimbingTopToBot()
    {
        float correctiveGrabDistance = 0f;
        if (currentModeMovement == ModeMovement.Climb) correctiveGrabDistance = 1f;

        RaycastHit hitTop;

        /*if (Physics.SphereCast(player.transform.position, grabToClimbDistance, player.transform.forward, out hitTop, 10f, layerMask))
        {

        }*/

        if (Physics.Raycast(transform.position + 2.5f * transform.up, transform.forward, out hitTop, 5f, layerMask))
        {

            //Debug.Log("TOP RAY HIT");
            //Debug.DrawRay(transform.position + 2.5f * transform.up, transform.forward * hitTop.distance, Color.red);

            if (hitTop.distance <= grabToClimbDistance + correctiveGrabDistance)
            {
                //Debug.Log("TOP RAY CLOSE ENOUGH : " + (grabToClimbDistance + correctiveGrabDistance - hitTop.distance));
                var normalHit = hitTop.normal;
                if (Vector3.Angle(normalHit, Vector3.up) > 50f)
                {
                    distanceToGrabbedWall = hitTop.distance;
                    if (normalHit != currentNormalToClimb)
                    {
                        currentNormalToClimb = normalHit;
                        //Debug.Log("normal frér : x " + -currentNormalToClimb.x + ", y " + -currentNormalToClimb.y + ", z " + -currentNormalToClimb.z);
                        //playerRotation.StartRecenterPlayer(-currentNormalToClimb, playerRotationSpeed);
                        //cameraManager.StartRecenterCamera(cameraManager.MidRelativePosition, cameraManager.DefaultEulerAngle);
                    }
                    return true;
                }
            }
            else
            {
                //Debug.Log("TOP RAY TOO FAR : " + (grabToClimbDistance + correctiveGrabDistance - hitTop.distance));
            }
        }
        else
        {
            //Debug.DrawRay(transform.position + 2.5f * transform.up, transform.forward * 5f, Color.yellow);
        }

        if (!middleOfClimbing)
        {
            RaycastHit hitMid;

            if (Physics.Raycast(transform.position + 0f * transform.up, transform.forward, out hitMid, 5f, layerMask))
            {
                //Debug.Log("MID RAY HIT");
                //Debug.DrawRay(transform.position + 0f * transform.up, transform.forward * hitMid.distance, Color.red);

                if (hitMid.distance <= grabToClimbDistance + correctiveGrabDistance)
                {
                    //Debug.Log("MID RAY CLOSE ENOUGH : " + (grabToClimbDistance + correctiveGrabDistance - hitMid.distance));
                    var normalHit = hitMid.normal;
                    if (Vector3.Angle(normalHit, Vector3.up) > 50f)
                    {
                        distanceToGrabbedWall = hitMid.distance;
                        //Debug.Log("new distance to grabbed wall = " + distanceToGrabbedWall);

                        if (normalHit != currentNormalToClimb)
                        {
                            currentNormalToClimb = normalHit;
                            //Debug.Log("normal frér : x " + -currentNormalToClimb.x + ", y " + -currentNormalToClimb.y + ", z " + -currentNormalToClimb.z);
                            //playerRotation.StartRecenterPlayer(-currentNormalToClimb, playerRotationSpeed);
                            //cameraManager.StartRecenterCamera(cameraManager.MidRelativePosition, cameraManager.DefaultEulerAngle);
                        }
                        return true;
                    }
                }
                else
                {
                    //Debug.Log("MID RAY TOO FAR : " + (grabToClimbDistance + correctiveGrabDistance - hitMid.distance));
                }
            }
            else
            {
                //Debug.DrawRay(transform.position + 0f * transform.up, transform.forward * 5f, Color.yellow);
            }

            RaycastHit hitBot;

            if (Physics.Raycast(transform.position + 0.35f * transform.up, transform.forward, out hitBot, 5f, layerMask))
            {
                //Debug.Log("BOT RAY HIT");
                //Debug.DrawRay(transform.position + 0.35f * transform.up, transform.forward * hitBot.distance, Color.red);

                if (hitBot.distance <= grabToClimbDistance + correctiveGrabDistance)
                {
                    //Debug.Log("BOT RAY CLOSE ENOUGH : " + (grabToClimbDistance + correctiveGrabDistance - hitBot.distance));
                    var normalHit = hitBot.normal;
                    if (Vector3.Angle(normalHit, Vector3.up) > 50f)
                    {
                        distanceToGrabbedWall = hitBot.distance;
                        if (normalHit != currentNormalToClimb)
                        {
                            currentNormalToClimb = normalHit;
                            //Debug.Log("normal frér : x " + -currentNormalToClimb.x + ", y " + -currentNormalToClimb.y + ", z " + -currentNormalToClimb.z);
                            //playerRotation.StartRecenterPlayer(-currentNormalToClimb, playerRotationSpeed);
                            //cameraManager.StartRecenterCamera(cameraManager.MidRelativePosition, cameraManager.DefaultEulerAngle);
                        }
                        return true;
                    }
                }
                else
                {
                    //Debug.Log("BOT RAY TOO FAR : " + (grabToClimbDistance + correctiveGrabDistance - hitBot.distance));
                }
            }
            else
            {
                //Debug.DrawRay(player.transform.position + -2.5f * player.transform.up, player.transform.forward * 5f, Color.yellow);
            }
        }
        return false;
    }
    public bool CheckIfClimbingTopRay()
    {
        float correctiveGrabDistance = 0f;
        if (currentModeMovement == ModeMovement.Climb) correctiveGrabDistance = 1f;

        RaycastHit hitTop;

        /*if (Physics.SphereCast(player.transform.position, grabToClimbDistance, player.transform.forward, out hitTop, 10f, layerMask))
        {

        }*/

        if (Physics.Raycast(transform.position + 2.5f * transform.up, transform.forward, out hitTop, 5f, layerMask))
        {
            //Debug.Log("TOP RAY HIT");
            //Debug.DrawRay(transform.position + 2.5f * transform.up, transform.forward * hitTop.distance, Color.red);

            if (hitTop.distance <= grabToClimbDistance + correctiveGrabDistance)
            {
                //Debug.Log("TOP RAY CLOSE ENOUGH : " + (grabToClimbDistance + correctiveGrabDistance - hitTop.distance));
                var normalHit = hitTop.normal;
                if (Vector3.Angle(normalHit, Vector3.up) > 50f)
                {
                    distanceToGrabbedWall = hitTop.distance;
                    if (normalHit != currentNormalToClimb)
                    {
                        currentNormalToClimb = normalHit;
                        //Debug.Log("normal frér : x " + -currentNormalToClimb.x + ", y " + -currentNormalToClimb.y + ", z " + -currentNormalToClimb.z);
                        //playerRotation.StartRecenterPlayer(-currentNormalToClimb, playerRotationSpeed);
                        //cameraManager.StartRecenterCamera(cameraManager.MidRelativePosition, cameraManager.DefaultEulerAngle);
                    }
                    return true;
                }
            }
            else
            {
                //Debug.Log("TOP RAY TOO FAR : " + (grabToClimbDistance + correctiveGrabDistance - hitTop.distance));
            }
        }
        else
        {
            //Debug.DrawRay(transform.position + 2.5f * transform.up, transform.forward * 5f, Color.yellow);
        }
        return false;
    }
    public bool CheckIfClimbingUp()
    {
        RaycastHit hit;
        //if (Physics.Raycast(player.transform.position + 1.0f * Vector3.up + 0.25f * Vector3.forward, player.transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, layerMask))
        if (Physics.Raycast(transform.position + 0f * transform.up /*+ 0.25f * Vector3.forward*/, Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log($"hit distance = {hit.distance}");
            //Debug.DrawRay(player.transform.position + 1.0f * Vector3.up + 0.25f * Vector3.forward, player.transform.TransformDirection(Vector3.up) * hit.distance, Color.green, 0f);
            Debug.DrawRay(transform.position + 0f * transform.up /*+ 0.25f * Vector3.forward*/, Vector3.up * hit.distance, Color.green, 0f);
            //if (hit.distance > 2f * grabToClimbDistance) return true;
            if (hit.distance <= grabToHangDistance)
            {
                return true;
                /*
                if (Physics.Raycast(transform.position + 1.0f * transform.up + 0.25f * Vector3.forward, Vector3.up, out hit, Mathf.Infinity, layerMask))
                {
                    return true;
                }*/
            }
        }
        else
        {
            //Debug.DrawRay(player.transform.position + 1.0f * Vector3.up + 0.25f * Vector3.forward, player.transform.TransformDirection(Vector3.up) * 10f, Color.green, 0f);
            //Debug.DrawRay(transform.position + 1.0f * transform.up /*+ 0.25f * Vector3.forward*/, Vector3.up * 10f, Color.green, 0f);
        }
        return false;
    }
    public bool CheckIfClimbingBotRay()
    {
        float correctiveGrabDistance = 0f;
        if (currentModeMovement == ModeMovement.Climb) correctiveGrabDistance = 1f;

        RaycastHit hitBot;

        if (Physics.Raycast(transform.position + 0.35f * transform.up, transform.forward, out hitBot, 5f, layerMask))
        {
            //Debug.Log("BOT RAY HIT");
            Debug.DrawRay(transform.position + 0.35f * transform.up, transform.forward * hitBot.distance, Color.red);

            if (hitBot.distance <= grabToClimbDistance + correctiveGrabDistance)
            {
                //Debug.Log("BOT RAY CLOSE ENOUGH : " + (grabToClimbDistance + correctiveGrabDistance - hitBot.distance));
                var normalHit = hitBot.normal;
                if (Vector3.Angle(normalHit, Vector3.up) > 50f)
                {
                    distanceToGrabbedWall = hitBot.distance;
                    if (normalHit != currentNormalToClimb)
                    {
                        currentNormalToClimb = normalHit;
                        //Debug.Log("normal frér : x " + -currentNormalToClimb.x + ", y " + -currentNormalToClimb.y + ", z " + -currentNormalToClimb.z);
                        //playerRotation.StartRecenterPlayer(-currentNormalToClimb, playerRotationSpeed);
                        //cameraManager.StartRecenterCamera(cameraManager.MidRelativePosition, cameraManager.DefaultEulerAngle);
                    }
                    return true;
                }
            }
            else
            {
                //Debug.Log("BOT RAY TOO FAR : " + (grabToClimbDistance + correctiveGrabDistance - hitBot.distance));
            }
        }
        else
        {
            //Debug.DrawRay(transform.position + -2.5f * transform.up, transform.forward * 5f, Color.yellow);
        }
        return false;
    }
    public bool CheckIfStopHanging()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + 1.0f * transform.up, Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.DrawRay(transform.position + 1.0f * transform.up, Vector3.up * hit.distance, Color.green, 0f);
            if (hit.distance <= 2f * grabToHangDistance)
            {
                return false;
            }
        }
        //else Debug.DrawRay(transform.position + 1.0f * transform.up, Vector3.up * 10f, Color.green, 0f);

        if (Physics.Raycast(transform.position + 1.0f * transform.up - 0.6f * transform.forward, Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.DrawRay(transform.position + 1.0f * transform.up - 0.6f * transform.forward, Vector3.up * hit.distance, Color.green, 0f);
            if (hit.distance <= 2f * grabToHangDistance)
            {
                return false;
            }
            else
            {
                //Debug.DrawRay(transform.position + 1.0f * transform.up - 0.6f * transform.forward, Vector3.up * 10f, Color.green, 0f);
                return true;
            }
        }
        return true;
    }
    public void ResetStamina()
    {
        currentClimbStamina = maxClimbStamina;
    }

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;

    public void Move(float maxSpeed, float maxAcceleration)
    {
        Vector3 velocity = characterController.velocity;

        // Check Input to determine direction
        Vector2 playerInput;
        playerInput.x = inputManager.HorizontalInput;
        playerInput.y = inputManager.VerticalInput;

        // Clamp it to disallow strafe walking
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        // Add camera angle to the input vector so that the player moves where the camera looks
        float targetAngle = Mathf.Atan2(playerInput.x, playerInput.y) * Mathf.Rad2Deg + camTrsf.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        // Modify the direction the player model is looking
        if (playerInput.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // Give the movement inertia by changing the velocity from its previous value to its desired value 
        Vector3 targetDirection = playerInput.magnitude * transform.forward;
        Vector3 desiredVelocity = new Vector3(targetDirection.x, 0f, targetDirection.z) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        // Apply gravity if not grounded
        if (animator.GetBool("PlayerJumped"))
            velocity.y = jumpVerticalBoost;
        else if (playerParameters.characterController.isGrounded)
            velocity.y = 0f;
        else
            velocity.y -= playerParameters.gravity;

        // Apply appropriate friction force depending if in water or not
        if (playerParameters.isInWaterNextFixedUpdate)
        {
            velocity.y += playerParameters.forceOfWater;
            velocity.y *= 0.99f;
        }
        else velocity.y *= 0.999f;

        // Move the player through its character controller
        characterController.Move(velocity * Time.deltaTime);

        //Vector3 newPosition = transform.localPosition + transform.TransformDirection(displacement);

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
    }

    public void Climb(float maxClimbSpeed, float maxClimbAcceleration)
    {
        if (currentClimbStamina > 0f)
        {
            Vector3 velocity = characterController.velocity;

            Vector2 playerInput;
            playerInput.x = inputManager.HorizontalInput;
            playerInput.y = inputManager.VerticalInput;
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);
            Vector3 desiredVelocity = new Vector3(playerInput.x, playerInput.y, 0f) * maxClimbSpeed;
            float maxSpeedChange = maxClimbAcceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
            Vector3 displacement = velocity * Time.deltaTime;

            animator.transform.rotation = Quaternion.FromToRotation(animator.transform.TransformDirection(Vector3.forward), -currentNormalToClimb) * animator.transform.rotation;

            currentClimbStamina -= displacement.magnitude;
            characterController.Move(transform.TransformDirection(displacement));
        }
    }

    public void ClimbHanging(float maxClimbSpeed, float maxClimbAcceleration)
    {
        if (currentClimbStamina > 0f)
        {
            Vector3 velocity = characterController.velocity;

            Vector2 playerInput;
            playerInput.x = inputManager.HorizontalInput;
            playerInput.y = inputManager.VerticalInput;
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);
            Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, -playerInput.y) * maxClimbSpeed;
            float maxSpeedChange = maxClimbAcceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            Vector3 displacement = velocity * Time.deltaTime;

            currentClimbStamina -= displacement.magnitude;
            playerParameters.characterController.Move(displacement);
        }
    }
    /*public void ChangeAnimSpeed()
    {
        animator.speed = 0.5f;
    }*/
    public void Glide(float maxSpeed, float maxAcceleration)
    {
        Vector3 velocity = characterController.velocity;

        // Check Input to determine direction
        Vector2 playerInput;
        playerInput.x = inputManager.HorizontalInput;
        playerInput.y = inputManager.VerticalInput;

        // Clamp it to disallow strafe walking
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        // Add camera angle to the input vector so that the player moves where the camera looks
        float targetAngle = Mathf.Atan2(playerInput.x, playerInput.y) * Mathf.Rad2Deg + camTrsf.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        // Modify the direction the player model is looking
        if (playerInput.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // Give the movement inertia by changing the velocity from its previous value to its desired value 
        Vector3 targetDirection = playerInput.magnitude * transform.forward;
        Vector3 desiredVelocity = new Vector3(targetDirection.x, 0f, targetDirection.z) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        // Apply gravity if not grounded
        if (animator.GetBool("PlayerJumped"))
            velocity.y = jumpVerticalBoost;
        else if (playerParameters.characterController.isGrounded)
            velocity.y = 0f;
        else
            velocity.y -= playerParameters.gravity;

        // Apply appropriate friction force depending if in water or not
        if (playerParameters.isInWaterNextFixedUpdate)
        {
            velocity.y += playerParameters.forceOfWater;
            velocity.y *= 0.99f;
        }
        else velocity.y *= 0.999f;

        // Move the player through its character controller
        characterController.Move(velocity * Time.deltaTime);

        //Vector3 newPosition = transform.localPosition + transform.TransformDirection(displacement);

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
    }
    #region PARAMETERS UPDATER
    public void UpdateIdleTransitionsParameters(string parameterName, float maxSpeedTransition)
    {
        animator.SetBool("" + parameterName, (characterController.velocity.x * Vector3.right + characterController.velocity.z * Vector3.forward).magnitude > maxSpeedTransition);
    }
    public void UpdateIsGrounded()
    {
        animator.SetBool("IsGrounded", CheckIsGrounded() || characterController.isGrounded);
    }
    public void UpdateCanClimbTopToBot()
    {
        animator.SetBool("CanClimbTopToBot", CheckIfClimbingTopToBot());
    }
    public void UpdateCanClimbTopRay()
    {
        animator.SetBool("CanClimbTopRay", CheckIfClimbingTopRay());
    }
    public void UpdateCanClimbBotRay()
    {
        animator.SetBool("CanClimbBotRay", CheckIfClimbingBotRay());
    }
    public void UpdateCanClimbUp()
    {
        animator.SetBool("CanClimbUp", CheckIfClimbingUp());
    }
    public void UpdateStopHanging()
    {
        animator.SetBool("StopHanging", CheckIfStopHanging());
    }
    public void UpdateInputValue()
    {
        animator.SetFloat("VerticalInput", inputManager.VerticalInput);
        animator.SetFloat("HorizontalInput", inputManager.HorizontalInput);
    }
    public void UpdateStartGlide()
    {
        animator.SetBool("PlayerStartGlide", inputManager.IsSpaceJump);
    }
    #endregion
}
