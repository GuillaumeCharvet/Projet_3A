using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Analytics;

//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public enum ModeMovement
{ Idle, Walk, Run, Jump, Swim, Climb, Slide, Glide, GrabLedge, Hang, Fall, ChargeThrow, Throw };

public class StateMachineParameters : MonoBehaviour
{
    private Animator animator;
    private MovementParameters playerParameters;
    private CharacterController characterController;
    private Rigidbody rigidBodyPlayer;
    private InputManager inputManager;

    [SerializeField] private Transform camTrsf;
    private Vector3 displacement = Vector3.zero;

    public ModeMovement currentModeMovement;

    // PARAMETERS FOR CHECKISGROUNDED
    private float radius;

    [SerializeField] private LayerMask layerMaskClimb;
    [SerializeField] private LayerMask layerMaskIsGrounded;
    public float distanceDefiningGroundedState = 0f;//3f;
    public float epsilonCheckGrounded = 0.001f;

    // PARAMETERS FOR CHECKIFCLIMBINGTOPTOBOT
    private float grabToClimbDistance = 1f;

    private float grabToHangDistance = 1.8f;
    public float distanceToGrabbedWall = 0f;
    public float distanceToGrabbedWallLimit = 0.5f;
    public bool middleOfClimbing = false;

    [Header("FALL")]
    [SerializeField] public float airControl = 0.2f;

    [SerializeField] public float gravity = 1.2f;
    public float timeInAir = 0f;
    public float timeInAirBefore = 0f;

    [Header("RUN/JUMP")]
    [SerializeField] private float influenceOfSlopeOnSpeed = 0.5f;

    [SerializeField] private float maxSpeed = 10f;
    public float MaxSpeed { get => maxSpeed; }

    private float turnSmoothTime = 0.15f;
    private float turnSmoothVelocity = 0f;

    [SerializeField] public float jumpVerticalBoost = 0.4f;
    [SerializeField] public float jumpHorizontalBoost = 1f;
    public Vector3 currentGroundNormal;
    [SerializeField] private AnimationCurve plotGroundAngleInfluence = new AnimationCurve();
    public float slopeLimitOnGround = 60f;

    [Header("CLIMB")]
    [SerializeField] public float climbSpeed = 2.5f;

    public float maxClimbStamina = 10f;
    public float currentClimbStamina = 0f;
    public Vector3 currentNormalToClimb;
    private float stickingToSurfaceSpeed = 300f;
    private float stickingToSurfaceEpsilon = 0.0005f;
    private float maxPlayerRotation = 8f;
    public float characterControlerHeightResetValue = 1.8f;
    public bool isDuringFirst2SecondsOfClimbing = false;
    private float wallJumpVelocity = 16f;
    private float topRay = 2.5f, midRay = 1.5f, botRay = 0.25f;

    [Header("GRAB LEDGE")]
    [SerializeField] private float grabLedgeDistance = 3.2f;

    [Header("GLIDE")]
    public Animator animatorGlider;

    public Transform gliderTransform;
    public float gliderRotationSpeed = 0f;
    public float gliderRotationAcceleration = 0.5f;
    public float maxGliderRotationSpeed = 40f;

    public float gliderTurnSpeed = 0f;
    public float gliderCameraTurnSpeed = 0f;
    public float gliderTurnAcceleration = 0.1f;
    public float maxGliderTurnSpeed = 1f;

    public Vector3 windEffect = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 moveNormalToDirection = Vector3.zero;

    [SerializeField] public float gliderSpeed = 0f;
    [SerializeField] private float gliderNormalSpeed = 0f;

    [SerializeField] private float maxGliderSpeed = 50f;
    [SerializeField] private float accelerationMaxGlider = 30f;
    [SerializeField] private float gliderDescentFactor = 0.1f;
    private bool preventGliderOn = false;
    private bool preventGliderOff = false;
    private float preventGliderDelay = 0.5f;

    private bool initialGlideDiveBlock = false;

    [Header("SWIM")]
    public bool isInWaterNextFixedUpdate = false;

    public bool isInNoWaterZone = false;
    public BuoyancyEffect lastWaterVisited;
    public float forceOfWater;

    private float turnSmoothTimeSwim = 0.005f;

    [SerializeField] private float currentHeightDiff = 0f;
    [SerializeField] private float currentHeightRef = 0f;
    public float slopeLimitInWater = 90f;

    [Header("THROW")]
    [SerializeField] private float throwPower = 2f;

    [SerializeField] private GameObject prefabSpear;
    [SerializeField] private Transform spearTransform;
    [SerializeField] private Vector3 spearPositionOffset;
    [SerializeField] private Vector3 spearInitialRotationEulerAngle;

    [SerializeField] public Transform ropeEndOnPlayer;
    public GameObject ropeExtremityPrefab;

    private float timeChargingThrow = 0f;
    private float timeBeforeThrow = 0.58f * 1.3f / 1.6f;
    public float GliderRotationSpeed { get => gliderRotationSpeed; set => gliderRotationSpeed = value; }
    public bool InitialGlideDiveBlock { get => initialGlideDiveBlock; set => initialGlideDiveBlock = value; }

    public float angleDiff = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        rigidBodyPlayer = GetComponent<Rigidbody>();
        inputManager = ManagerManager.Instance.GetManager<InputManager>();
        playerParameters = GetComponent<MovementParameters>();

        radius = GetComponent<CharacterController>().radius;
    }

    private void Update()
    {
        UpdateIsGrounded();
        UpdateHasGroundBelow();

        UpdateInputValue();
        animator.SetFloat("VerticalSpeed", Vector3.Dot(characterController.velocity, Vector3.up));
        animator.SetFloat("ForwardSpeed", (characterController.velocity.x * Vector3.right + characterController.velocity.z * Vector3.forward).magnitude); //Vector3.Dot(characterController.velocity, transform.forward));

        //animator.SetFloat("InputDotSurfaceNormal", Vector3.Dot(Quaternion.Euler(0f, camTrsf.rotation.eulerAngles.y, 0f) * (inputManager.HorizontalInput * Vector3.right + inputManager.VerticalInput * Vector3.forward).normalized, currentNormalToClimb.x * Vector3.right + currentNormalToClimb.z * Vector3.forward));
        animator.SetFloat("InputDotSurfaceNormal", Vector3.Dot(transform.forward, Vector3.ProjectOnPlane(currentNormalToClimb, Vector3.up)));

        animator.SetBool("IsInWater", isInWaterNextFixedUpdate && !isInNoWaterZone);

        // Check if player is charging the throw
        UpdateTimeChargingThrow();
        animator.SetFloat("ThrowCharge", timeChargingThrow);

        // Deblock capacity to dive in glider
        CheckBlockGliderDive();

        timeInAirBefore = timeInAir;
        UpdateTimeInAir();
        animator.SetFloat("TimeInAir", timeInAirBefore);
    }

    private void FixedUpdate()
    {
        animator.SetBool("PlayerJumped", (characterController.isGrounded || CheckIsGrounded() || currentModeMovement == ModeMovement.Climb) && inputManager.IsSpaceJump);

        animatorGlider.SetBool("IsInGlideState", currentModeMovement == ModeMovement.Glide);

        animator.SetBool("PlayerStartGlide", !CheckIsGrounded() && inputManager.IsSpaceDownFixed);

        if (!CheckIsGrounded() && inputManager.IsSpaceDownFixed && currentModeMovement != ModeMovement.Glide && !preventGliderOn) animator.SetTrigger("GliderTrigger");
        if (inputManager.IsSpaceDownFixed && currentModeMovement == ModeMovement.Glide && !preventGliderOff) animator.SetTrigger("GliderOffTrigger");
    }

    #region CLIMB CHECKS

    public bool CheckIsGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + (characterControlerHeightResetValue - (characterController.radius + 0.05f)) * transform.up, characterController.radius + epsilonCheckGrounded, -transform.up, out hit, (characterControlerHeightResetValue - 0.1f), layerMaskIsGrounded))
        {
            currentGroundNormal = hit.normal;
            return true;
        }
        return false;
    }

    public bool CheckIfClimbingTopToBot(bool updateCurrentNormal)
    {
        float correctiveGrabDistance = 0f;
        if (currentModeMovement == ModeMovement.Climb) correctiveGrabDistance = 1f;

        RaycastHit hitTop;

        if (Physics.Raycast(transform.position + topRay * transform.up, transform.forward, out hitTop, 5f, layerMaskClimb))
        {
            Debug.DrawRay(transform.position + topRay * transform.up, transform.forward * hitTop.distance, Color.red);

            if (hitTop.distance <= grabToClimbDistance + correctiveGrabDistance)
            {
                //Debug.Log("TOP RAY CLOSE ENOUGH : " + (grabToClimbDistance + correctiveGrabDistance - hitTop.distance));
                var normalHit = hitTop.normal;
                if (Vector3.Angle(normalHit, Vector3.up) > 40f)
                {
                    distanceToGrabbedWall = hitTop.distance;
                    if (normalHit != currentNormalToClimb && updateCurrentNormal)
                    {
                        currentNormalToClimb = normalHit;
                    }
                    return true;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position + topRay * transform.up, transform.forward * 5f, Color.yellow);
        }

        if (!middleOfClimbing)
        {
            RaycastHit hitMid;

            if (Physics.Raycast(transform.position + midRay * transform.up, transform.forward, out hitMid, 5f, layerMaskClimb))
            {
                Debug.DrawRay(transform.position + midRay * transform.up, transform.forward * hitMid.distance, Color.red);

                if (hitMid.distance <= grabToClimbDistance + correctiveGrabDistance)
                {
                    var normalHit = hitMid.normal;
                    if (Vector3.Angle(normalHit, Vector3.up) > 40f)
                    {
                        distanceToGrabbedWall = hitMid.distance;

                        if (normalHit != currentNormalToClimb && updateCurrentNormal)
                        {
                            currentNormalToClimb = normalHit;
                        }
                        return true;
                    }
                }
            }
            else
            {
                Debug.DrawRay(transform.position + midRay * transform.up, transform.forward * 5f, Color.yellow);
            }

            /*
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
            */
        }
        return false;
    }

    public bool CheckIfClimbingTopRay()
    {
        float correctiveGrabDistance = 0f;
        if (currentModeMovement == ModeMovement.Climb) correctiveGrabDistance = 1f;

        RaycastHit hitTop;

        if (Physics.Raycast(transform.position + topRay * transform.up, transform.forward, out hitTop, 5f, layerMaskClimb))
        {
            Debug.DrawRay(transform.position + topRay * transform.up, transform.forward * hitTop.distance, Color.blue);

            if (hitTop.distance <= grabLedgeDistance + correctiveGrabDistance)
            {
                var normalHit = hitTop.normal;
                if (Vector3.Angle(normalHit, Vector3.up) > 40f)
                {
                    distanceToGrabbedWall = hitTop.distance;
                    if (normalHit != currentNormalToClimb)
                    {
                        currentNormalToClimb = normalHit;
                    }
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckIfClimbingUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + 0f * transform.up /*+ 0.25f * Vector3.forward*/, Vector3.up, out hit, Mathf.Infinity, layerMaskClimb))
        {
            if (hit.distance <= grabToHangDistance)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckIfClimbingBotRay()
    {
        float correctiveGrabDistance = 0f;
        if (currentModeMovement == ModeMovement.Climb) correctiveGrabDistance = 1f;

        RaycastHit hitBot;
        Debug.DrawRay(transform.position + botRay * transform.up, transform.forward * 10, Color.red);

        if (Physics.Raycast(transform.position + botRay * transform.up, transform.forward, out hitBot, 5f, layerMaskClimb))
        {
            //Debug.Log("BOT RAY HIT");

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
        if (Physics.Raycast(transform.position + 1.0f * transform.up, Vector3.up, out hit, Mathf.Infinity, layerMaskClimb))
        {
            //Debug.DrawRay(transform.position + 1.0f * transform.up, Vector3.up * hit.distance, Color.green, 0f);
            if (hit.distance <= 2f * grabToHangDistance)
            {
                return false;
            }
        }
        //else Debug.DrawRay(transform.position + 1.0f * transform.up, Vector3.up * 10f, Color.green, 0f);

        if (Physics.Raycast(transform.position + 1.0f * transform.up - 0.6f * transform.forward, Vector3.up, out hit, Mathf.Infinity, layerMaskClimb))
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

    #endregion CLIMB CHECKS

    #region MOVEMENT

    public void Move(float maxSpeed, float maxAcceleration, bool onGround)
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

        // Take into account ground angle to slow down speed the bigger the slope
        var groundAngleFromHorizontal = Vector3.Angle(Vector3.up, currentGroundNormal);

        // Change the velocity depending on the slope angle the player is walking on
        var playerInputNormalized = transform.forward;
        var groundAngleInfluence = Vector3.Dot(Vector3.ProjectOnPlane(currentGroundNormal, transform.right), new Vector3(playerInputNormalized.x, 0f, playerInputNormalized.z));
        desiredVelocity *= (1f + influenceOfSlopeOnSpeed * (groundAngleInfluence - 0.3f));

        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        // Apply gravity if not grounded

        //if (animator.GetBool("PlayerJumped"))
        if ((characterController.isGrounded || CheckIsGrounded()) && inputManager.IsSpaceDownFixed)
        {
            StartCoroutine(DelayGliderOn());
            velocity.y += jumpVerticalBoost;
        }
        else if (playerParameters.characterController.isGrounded)
            velocity.y = 0f;
        else
            velocity.y -= gravity * Time.deltaTime;

        // Move the player through its character controller
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Swim(float maxSpeed, float maxAcceleration, bool onGround)
    {
        Vector3 velocity = characterController.velocity;

        if (isInWaterNextFixedUpdate)
        {
            velocity.x *= 0.98f;
            velocity.z *= 0.98f;
        }

        // Check Input to determine direction
        Vector2 playerInput;
        playerInput.x = inputManager.HorizontalInput;
        playerInput.y = inputManager.VerticalInput;

        // Clamp it to disallow strafe walking
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        // Add camera angle to the input vector so that the player moves where the camera looks
        float targetAngle = Mathf.Atan2(playerInput.x, playerInput.y) * Mathf.Rad2Deg + camTrsf.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTimeSwim);

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
            velocity.y += jumpVerticalBoost;
        else if (playerParameters.characterController.isGrounded)
            velocity.y = 0f;
        else
            velocity.y -= gravity * Time.deltaTime;

        // Apply appropriate friction force depending if in water or not
        if (isInWaterNextFixedUpdate)
        {
            velocity.y += forceOfWater;
            velocity.y = Mathf.Clamp(velocity.y, 0f, 20f);
            velocity.y *= 0.9f;
        }

        // Move the player through its character controller
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Climb(float maxClimbSpeed, float maxClimbAcceleration)
    {
        if (inputManager.IsSpaceDownFixed && inputManager.VerticalInput > -0.1f && inputManager.VerticalInput < 0.1f)
        {
            StartCoroutine(DelayGliderOn());
            Debug.Log("DECOLLE DU MUR WESH");
            transform.rotation = Quaternion.LookRotation(-transform.forward, transform.up);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            var velocity = wallJumpVelocity * transform.forward;
            Vector3 displacement = velocity * Time.deltaTime;
            characterController.Move(displacement);
        }
        else if (!inputManager.IsSpaceDownFixed)
        {
            if (true)
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.forward, -currentNormalToClimb) , maxPlayerRotation * Time.deltaTime);
                //transform.rotation = Quaternion.FromToRotation(transform.forward, -currentNormalToClimb) * transform.rotation;

                //var quatTo = Quaternion.FromToRotation(transform.forward, -currentNormalToClimb) * transform.rotation;

                var quatFrom = transform.rotation;
                var rotationTowardNormalLockX = isDuringFirst2SecondsOfClimbing ? 1f : Mathf.Abs(inputManager.VerticalInput);
                var quatToX = Quaternion.FromToRotation(transform.forward, Vector3.ProjectOnPlane(-currentNormalToClimb, transform.right));
                transform.rotation = Quaternion.Lerp(quatFrom, quatToX * transform.rotation, rotationTowardNormalLockX * maxPlayerRotation * Time.deltaTime);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);

                quatFrom = transform.rotation;
                var rotationTowardNormalLockY = isDuringFirst2SecondsOfClimbing ? 1f : Mathf.Abs(inputManager.HorizontalInput);
                var quatToY = Quaternion.FromToRotation(transform.forward, Vector3.ProjectOnPlane(-currentNormalToClimb, transform.up));
                transform.rotation = Quaternion.Lerp(quatFrom, quatToY * transform.rotation, rotationTowardNormalLockY * maxPlayerRotation * Time.deltaTime);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);

                Vector3 velocity = characterController.velocity;
                Vector3 projectToForward = Vector3.Project(velocity, transform.forward);
                velocity -= projectToForward;

                Vector2 playerInput;
                playerInput.x = inputManager.HorizontalInput;
                playerInput.y = inputManager.VerticalInput;
                playerInput = Vector2.ClampMagnitude(playerInput, 1f);

                /*
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, 0.19f, transform.forward, out hit, grabToClimbDistance))
                {
                    if (Vector3.Distance(hit.point, transform.position) < distanceToGrabbedWallLimit - 0.19f)
                    {
                    }
                }
                */

                float normalToWallVelocity;
                if (distanceToGrabbedWall < grabToClimbDistance && currentModeMovement == ModeMovement.Climb)
                {
                    var diffToWall = distanceToGrabbedWall - distanceToGrabbedWallLimit;
                    normalToWallVelocity = diffToWall > stickingToSurfaceEpsilon ? Mathf.Min(stickingToSurfaceSpeed * Time.deltaTime, diffToWall) : diffToWall < -stickingToSurfaceEpsilon ? Mathf.Max(-stickingToSurfaceSpeed * Time.deltaTime, diffToWall) : 0f;
                }
                else
                {
                    normalToWallVelocity = 0f;
                }

                //TODO : sticking to surface potential bug

                Vector3 desiredVelocity = (playerInput.y * transform.up + playerInput.x * transform.right) * maxClimbSpeed;

                float maxSpeedChange = maxClimbAcceleration * Time.deltaTime;
                velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
                velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
                velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

                Vector3 displacement = velocity * Time.deltaTime + normalToWallVelocity * transform.forward;

                //currentClimbStamina -= displacement.magnitude;

                /*if (GetComponent<Rigidbody>() != null)
                {
                    rigidBodyPlayer.MovePosition(transform.position + displacement);
                }*/
                characterController.Move(displacement);
            }
        }
    }

    public void Fall(float maxSpeed, float maxAcceleration, bool onGround)
    {
        Vector3 velocity = characterController.velocity;

        // Check Input to determine direction
        Vector2 playerInput;
        if (true)
        {
            playerInput.x = inputManager.HorizontalInput;
            playerInput.y = inputManager.VerticalInput;
        }
        else
        {
            playerInput = Vector2.zero;
        }

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
        if (playerParameters.characterController.isGrounded)
            velocity.y = 0f;
        else
            velocity.y -= gravity * Time.deltaTime;

        // Apply appropriate friction force depending if in water or not

        if (isInWaterNextFixedUpdate)
        {
            velocity.y += forceOfWater;
            velocity.y *= 0.96f;
        }
        else velocity.y *= 0.999f;

        // Move the player through its character controller
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Roll(float maxSpeed, float maxSpeedIdle, float maxAcceleration, bool onGround)
    {
        Vector3 velocity = characterController.velocity;

        if (isInWaterNextFixedUpdate)
        {
            velocity.x *= 0.98f;
            velocity.z *= 0.98f;
        }

        // Check Input to determine direction
        Vector2 playerInput;

        playerInput.x = inputManager.HorizontalInput;
        playerInput.y = inputManager.VerticalInput;

        // Force the player to go forward during a roll even if he doesn't press any input
        if (playerInput.sqrMagnitude < 0.1f)
        {
            velocity = transform.forward * maxSpeedIdle;

            if (playerParameters.characterController.isGrounded)
                velocity.y = 0f;
            else
                velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
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

            // Take into account ground angle to slow down speed the bigger the slope
            var groundAngleFromHorizontal = Vector3.Angle(Vector3.up, currentGroundNormal);

            // Change the velocity depending on the slope angle the player is walking on
            var playerInputNormalized = transform.forward;
            var groundAngleInfluence = Vector3.Dot(Vector3.ProjectOnPlane(currentGroundNormal, transform.right), new Vector3(playerInputNormalized.x, 0f, playerInputNormalized.z));
            desiredVelocity *= (1f + influenceOfSlopeOnSpeed * (groundAngleInfluence - 0.3f));

            // Plot the angle of the ground when detected by the avatar
            var listLength = plotGroundAngleInfluence.keys.Length;
            if (listLength == 0 || plotGroundAngleInfluence.keys[listLength - 1].value != groundAngleInfluence) plotGroundAngleInfluence.AddKey(Time.time, groundAngleInfluence);

            float maxSpeedChange = maxAcceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

            // Apply gravity if not grounded
            if (playerParameters.characterController.isGrounded)
                velocity.y = 0f;
            else
                velocity.y -= gravity * Time.deltaTime;
        }

        // Apply appropriate friction force depending if in water or not
        velocity.y *= 0.999f;

        // Move the player through its character controller
        characterController.Move(velocity * Time.deltaTime);
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

    public void Glide(float maxSpeed, float maxAcceleration)
    {
        float horizontal = inputManager.HorizontalInput;
        float vertical = initialGlideDiveBlock ? 0f : inputManager.VerticalInput;

        if (horizontal > 0.1f)
        {
            gliderTurnSpeed = Mathf.Min(gliderTurnSpeed + gliderTurnAcceleration * Time.deltaTime * 60f, maxGliderTurnSpeed);
        }
        else if (horizontal < -0.1f)
        {
            gliderTurnSpeed = Mathf.Max(gliderTurnSpeed - gliderTurnAcceleration * Time.deltaTime * 60f, -maxGliderTurnSpeed);
        }
        else if (gliderTurnSpeed <= 0f)
        {
            gliderTurnSpeed = Mathf.Min(gliderTurnSpeed + 0.5f * gliderTurnAcceleration * Time.deltaTime * 60f, 0f);
        }
        else if (gliderTurnSpeed >= 0f)
        {
            gliderTurnSpeed = Mathf.Max(gliderTurnSpeed - 0.5f * gliderTurnAcceleration * Time.deltaTime * 60f, 0f);
        }

        if (horizontal > 0.1f)
        {
            gliderCameraTurnSpeed = Mathf.Min(gliderCameraTurnSpeed + 0.2f * gliderTurnAcceleration * Time.deltaTime * 60f, 3f * maxGliderTurnSpeed);
        }
        else if (horizontal < -0.1f)
        {
            gliderCameraTurnSpeed = Mathf.Max(gliderCameraTurnSpeed - 0.2f * gliderTurnAcceleration * Time.deltaTime * 60f, 3f * -maxGliderTurnSpeed);
        }
        else if (gliderCameraTurnSpeed <= 0f)
        {
            gliderCameraTurnSpeed = Mathf.Min(gliderCameraTurnSpeed + 0.5f * 0.2f * gliderTurnAcceleration * Time.deltaTime * 60f, 0f);
        }
        else if (gliderCameraTurnSpeed >= 0f)
        {
            gliderCameraTurnSpeed = Mathf.Max(gliderCameraTurnSpeed - 0.5f * 0.2f * gliderTurnAcceleration * Time.deltaTime * 60f, 0f);
        }

        //S_Debugger.UpdatableLog("gliderTurnSpeed", gliderTurnSpeed);

        //TODO: Glider rotation smoothness
        // Rotate the player around the z axis to go along the change of direction
        var eulerZ = transform.localRotation.eulerAngles.z > 180 ? transform.localRotation.eulerAngles.z - 360 : transform.localRotation.eulerAngles.z;

        if (horizontal > 0.1f) transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + gliderTurnSpeed, Mathf.Max(eulerZ - 0.4f * gliderTurnSpeed, -15f));
        else if (horizontal < -0.1f) transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + gliderTurnSpeed, Mathf.Min(eulerZ - 0.4f * gliderTurnSpeed, +15f));

        /*if (horizontal > 0.1f || horizontal < -0.1f)
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + gliderTurnSpeed, Mathf.Max(eulerZ - 0.4f * gliderTurnSpeed, Mathf.Sign(horizontal) * 15f));
        */
        else if (eulerZ > 0.2f) transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, eulerZ - 0.4f);
        else if (eulerZ < -0.2f) transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, eulerZ + 0.4f);

        // Rotate the player around the x axis to go along acceleration and deceleration
        if (vertical > 0.1f) // && gliderSpeed > 1f)
        {
            var eulerX = transform.localRotation.eulerAngles.x > 180f ? transform.localRotation.eulerAngles.x - 360f : transform.localRotation.eulerAngles.x;
            transform.localRotation = Quaternion.Euler(Mathf.Min(eulerX + 0.8f * Mathf.Min(1f, 0.2f * Mathf.Pow(gliderSpeed, 2f)), 45f), transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }
        else if (vertical < -0.1f)
        {
            var eulerX = transform.localRotation.eulerAngles.x > 180f ? transform.localRotation.eulerAngles.x - 360f : transform.localRotation.eulerAngles.x;
            transform.localRotation = Quaternion.Euler(Mathf.Max(eulerX - 0.8f, -45f), transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }

        // Apply effect of the wind force
        var windBoost = Vector3.Dot(windEffect, moveDirection.normalized);
        var windNormalToMovement = windEffect - windBoost * moveDirection.normalized;

        var acceleration = accelerationMaxGlider * Mathf.Cos(Mathf.Deg2Rad * Vector3.SignedAngle(playerParameters.transform.forward, Vector3.down, playerParameters.transform.right)) + windBoost;

        moveNormalToDirection += 0.1f * windNormalToMovement * Time.deltaTime;

        gliderSpeed = Mathf.Max(Mathf.Min(gliderSpeed + acceleration * Time.deltaTime, maxGliderSpeed), 0f);

        transform.localRotation = Quaternion.Euler(playerParameters.transform.localRotation.eulerAngles.x + (1f / (1f + gliderSpeed * gliderSpeed)) * 3f, playerParameters.transform.localRotation.eulerAngles.y, playerParameters.transform.localRotation.eulerAngles.z);

        moveDirection = gliderSpeed * (playerParameters.transform.forward + gliderDescentFactor * Vector3.down) * Time.deltaTime + moveNormalToDirection;
        characterController.Move(moveDirection);

        moveNormalToDirection *= 0.97f;
    }

    public void ChargeThrow()
    {
    }

    #endregion MOVEMENT

    #region GAME LOGIC

    private void CheckBlockGliderDive()
    {
        if (inputManager.VerticalInput < 0.1f) initialGlideDiveBlock = false;
    }

    public IEnumerator DelayGliderOn()
    {
        preventGliderOn = true;
        yield return new WaitForSeconds(preventGliderDelay);
        preventGliderOn = false;
    }

    public IEnumerator DelayGliderOff()
    {
        preventGliderOff = true;
        yield return new WaitForSeconds(preventGliderDelay);
        preventGliderOff = false;
    }

    public IEnumerator WaitBeforeThrow()
    {
        yield return new WaitForSeconds(timeBeforeThrow);
        ThrowSpear();
    }

    public void ThrowSpear()
    {
        var spear = Instantiate(prefabSpear, transform.position + transform.TransformDirection(spearPositionOffset), Quaternion.identity, transform.parent);
        //spear.transform.localPosition = spearTransform.position + transform.TransformDirection(spearPositionOffset);
        spear.transform.rotation = transform.rotation * Quaternion.Euler(spearInitialRotationEulerAngle);
        //var spear = Instantiate(prefabSpear, transform.position + transform.TransformDirection(spearPositionOffset), Quaternion.Euler(spearInitialRotationEulerAngle), transform.parent);

        spear.GetComponent<Rigidbody>().AddForce(throwPower * transform.forward);
        var spearBehaviour = spear.GetComponent<S_HarponBehaviour>();
        spearBehaviour.velocity = throwPower * transform.forward;
    }

    public IEnumerator ChangeBoolValueFor2Seconds()
    {
        isDuringFirst2SecondsOfClimbing = true;
        yield return new WaitForSeconds(2f);
        isDuringFirst2SecondsOfClimbing = false;
    }

    public void ResetPlayerCollider()
    {
        characterController.height = characterControlerHeightResetValue;
        characterController.center = 0.85f * Vector3.up;

        GetComponent<CapsuleCollider>().enabled = false;
    }

    public void SetPlayerColliderToClimb()
    {
        GetComponent<CapsuleCollider>().enabled = true;

        characterController.height = 0;
        characterController.center = 0.5f * Vector3.up;
    }

    #endregion GAME LOGIC

    #region PARAMETERS UPDATER

    public void UpdateIdleTransitionsParameters(string parameterName, float maxSpeedTransition)
    {
        animator.SetBool(parameterName, (characterController.velocity.x * Vector3.right + characterController.velocity.z * Vector3.forward).magnitude > maxSpeedTransition);
    }

    public void UpdateIsGrounded()
    {
        animator.SetBool("IsGrounded", (CheckIsGrounded() || characterController.isGrounded)); //&& Vector3.Angle(Vector3.up, currentGroundNormal) < 50f);
    }

    public void UpdateHasGroundBelow()
    {
        animator.SetBool("HasGroundBelow", CheckIsGrounded());
    }

    public void UpdateCanClimbTopToBot(bool updateCurrentNormal)
    {
        animator.SetBool("CanClimbTopToBot", CheckIfClimbingTopToBot(updateCurrentNormal));
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
        var x = inputManager.VerticalInput;
        var y = inputManager.HorizontalInput;
        animator.SetFloat("VerticalInput", x);
        animator.SetFloat("HorizontalInput", y);
        animator.SetFloat("Input", Vector2.ClampMagnitude(new Vector2(x, y), 1f).magnitude);
    }

    public void UpdateStartGlide()
    {
        //animator.SetBool("PlayerStartGlide", inputManager.IsSpaceJump);
    }

    public void UpdateTimeChargingThrow()
    {
        if (inputManager.IsChargingThrow)
        {
            timeChargingThrow += Time.deltaTime;
        }
        else
        {
            timeChargingThrow = 0f;
        }
    }

    public void UpdateTimeInAir()
    {
        if (!characterController.isGrounded && !CheckIsGrounded() && currentModeMovement != ModeMovement.Climb && currentModeMovement != ModeMovement.Climb)
        {
            timeInAir += Time.deltaTime;
        }
        else
        {
            timeInAir = 0f;
        }
    }

    #endregion PARAMETERS UPDATER
}