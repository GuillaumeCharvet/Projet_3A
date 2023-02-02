using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    float maxSpeed = 50f;
    float maxAcceleration = 100f;

    [SerializeField] InputManager inputManager;

    Vector3 velocity;

    void Update()
    {
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

        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles + 2f * inputManager.MouseXInput * Time.deltaTime * 100f * Vector3.up + 2f * inputManager.MouseYInput * Time.deltaTime * 100f * Vector3.left);
        transform.localPosition = newPosition;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.localPosition += Vector3.up * 40f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.localPosition -= Vector3.up * 40f * Time.deltaTime;
        }

    }
}
