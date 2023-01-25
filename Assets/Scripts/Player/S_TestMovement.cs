using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TestMovement : MonoBehaviour
{
    private MovementParameters playerParameters;
    private CharacterController characterController;
    private InputManager inputManager;

    [SerializeField] private Transform camTrsf;
    private Vector3 displacement = Vector3.zero;

    private float maxSpeed = 10f;
    private float maxAcceleration = 200f;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputManager = ManagerManager.Instance.GetManager<InputManager>();
        playerParameters = GetComponent<MovementParameters>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = characterController.velocity;

        Vector2 playerInput;
        playerInput.x = inputManager.HorizontalInput;
        playerInput.y = inputManager.VerticalInput;
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        characterController.Move(velocity * Time.deltaTime);
        
        if (playerInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg + camTrsf.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            displacement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Debug.Log("displacement : " + playerInput);
            S_Debugger.UpdatableLog("displacement", displacement, Color.magenta);

            //characterController.Move(transform.TransformDirection(displacement));
            characterController.Move(displacement * velocity.magnitude * Time.fixedDeltaTime);
        }
        else
        {
            characterController.Move(displacement * velocity.magnitude * Time.fixedDeltaTime);
        }
        
    }
}
