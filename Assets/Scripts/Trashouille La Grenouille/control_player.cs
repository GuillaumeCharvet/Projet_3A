using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_player : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;

    private InputManager inputManager;

    Vector3 velocity;

    private void Start()
    {
        inputManager = ManagerManager.Instance.GetManager<InputManager>();
    }

    void Update()
    {

        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles + 4f * inputManager.MouseXInput * Time.deltaTime * 100f * Vector3.up + 3f * inputManager.MouseYInput * Time.deltaTime * 100f * Vector3.left);

        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + transform.TransformDirection(displacement);

        transform.localPosition = newPosition;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.localPosition += Vector3.up * maxSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.localPosition -= Vector3.up * maxSpeed * Time.deltaTime;
        }
    }
}
