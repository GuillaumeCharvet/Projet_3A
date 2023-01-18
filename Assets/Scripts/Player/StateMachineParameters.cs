using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public enum ModeMovement { Idle, Walk, Run, Jump, Swim, Climb, Slide, Glide, GrabLedge, Hang, Fall };

public class StateMachineParameters : MonoBehaviour
{
    private Animator animator;
    private MovementParameters playerParameters;
    private CharacterController characterController;

    public ModeMovement currentModeMovement;

    // PARAMETERS FOR CHECKISGROUNDED
    private float radius;
    [SerializeField] private LayerMask layerMask;
    public float distanceDefiningGroundedState = 0f;//3f;
    public float epsilonCheckGrounded = 0.001f;
    private InputManager inputManager;

    // PARAMETERS FOR CHECKIFCLIMBINGTOPTOBOT
    private float grabToClimbDistance = 2f;
    private float grabToHangDistance = 0.9f;
    public float distanceToGrabbedWall = 0f;
    public float distanceToGrabbedWallLimit = 0.5f;
    public bool middleOfClimbing = false;

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
        animator.SetBool("IsGrounded", CheckIsGrounded() || characterController.isGrounded);
        //animator.SetBool("PlayerJumped", (characterController.isGrounded || CheckIsGrounded()) && inputManager.IsSpaceJump);

        animator.SetBool("CanClimbTopToBot", CheckIfClimbingTopToBot());
        animator.SetBool("CanClimbBotRay", CheckIfClimbingBotRay());
        animator.SetBool("CanClimbTopRay", CheckIfClimbingTopRay());
        animator.SetBool("CanClimbUp", CheckIfClimbingUp());
        animator.SetBool("StopHanging", CheckIfStopHanging());

        animator.SetFloat("ForwardSpeed", (characterController.velocity.x * Vector3.right + characterController.velocity.z * Vector3.forward).magnitude);//Vector3.Dot(characterController.velocity, transform.forward));
        //Debug.Log("ForwardSpeed : " + (characterController.velocity.x * Vector3.right + characterController.velocity.z * Vector3.forward).magnitude);
        animator.SetFloat("VerticalSpeed", Vector3.Dot(characterController.velocity, Vector3.up));

        animator.SetFloat("VerticalInput", inputManager.VerticalInput);
        animator.SetFloat("HorizontalInput", inputManager.HorizontalInput);
    }

    void FixedUpdate()
    {
        animator.SetBool("PlayerJumped", (characterController.isGrounded || CheckIsGrounded()) && inputManager.IsSpaceJump);
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
                    if (normalHit != playerParameters.currentNormalToClimb)
                    {
                        playerParameters.currentNormalToClimb = normalHit;
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

                        if (normalHit != playerParameters.currentNormalToClimb)
                        {
                            playerParameters.currentNormalToClimb = normalHit;
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

            if (Physics.Raycast(transform.position + -0.80f * transform.up, transform.forward, out hitBot, 5f, layerMask))
            {
                //Debug.Log("BOT RAY HIT");
                //Debug.DrawRay(transform.position + -0.80f * transform.up, transform.forward * hitBot.distance, Color.red);

                if (hitBot.distance <= grabToClimbDistance + correctiveGrabDistance)
                {
                    //Debug.Log("BOT RAY CLOSE ENOUGH : " + (grabToClimbDistance + correctiveGrabDistance - hitBot.distance));
                    var normalHit = hitBot.normal;
                    if (Vector3.Angle(normalHit, Vector3.up) > 50f)
                    {
                        distanceToGrabbedWall = hitBot.distance;
                        if (normalHit != playerParameters.currentNormalToClimb)
                        {
                            playerParameters.currentNormalToClimb = normalHit;
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
                    if (normalHit != playerParameters.currentNormalToClimb)
                    {
                        playerParameters.currentNormalToClimb = normalHit;
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
            //Debug.DrawRay(player.transform.position + 1.0f * Vector3.up + 0.25f * Vector3.forward, player.transform.TransformDirection(Vector3.up) * hit.distance, Color.green, 0f);
            //Debug.DrawRay(transform.position + 1.0f * transform.up /*+ 0.25f * Vector3.forward*/, Vector3.up * hit.distance, Color.green, 0f);
            //if (hit.distance > 2f * grabToClimbDistance) return true;
            if (hit.distance <= grabToHangDistance)
            {
                if (Physics.Raycast(transform.position + 1.0f * transform.up + 0.25f * Vector3.forward, Vector3.up, out hit, Mathf.Infinity, layerMask))
                {
                    return true;
                }
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

        if (Physics.Raycast(transform.position + -0.85f * transform.up, transform.forward, out hitBot, 5f, layerMask))
        {
            //Debug.Log("BOT RAY HIT");
            //Debug.DrawRay(transform.position + -0.85f * transform.up, transform.forward * hitBot.distance, Color.red);

            if (hitBot.distance <= grabToClimbDistance + correctiveGrabDistance)
            {
                //Debug.Log("BOT RAY CLOSE ENOUGH : " + (grabToClimbDistance + correctiveGrabDistance - hitBot.distance));
                var normalHit = hitBot.normal;
                if (Vector3.Angle(normalHit, Vector3.up) > 50f)
                {
                    distanceToGrabbedWall = hitBot.distance;
                    if (normalHit != playerParameters.currentNormalToClimb)
                    {
                        playerParameters.currentNormalToClimb = normalHit;
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
}
