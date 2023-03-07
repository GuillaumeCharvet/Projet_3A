using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ModeInput { Keyboard, Controller, Intern };
public class InputManager : Manager
{
    public ModeInput currentModeInput;

    private float horizontalInput;
    private float verticalInput;
    private float mouseXInput;
    private float mouseYInput;

    private bool isSpaceDown;
    private bool isSpaceDownFixed;

    private bool isSpaceJump;

    public float HorizontalInput { get => horizontalInput; }
    public float VerticalInput { get => verticalInput; }
    public float MouseXInput { get => mouseXInput; }
    public float MouseYInput { get => mouseYInput; }
    public bool IsSpaceDown { get => isSpaceDown; }
    public bool IsSpaceDownFixed { get => isSpaceDownFixed; }
    public bool IsSpaceJump { get => isSpaceJump; }

    private void Awake()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }

    void Update()
    {
        switch (currentModeInput)
        {
            case ModeInput.Keyboard:

                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");

                mouseXInput = Input.GetAxis("Mouse X");
                mouseYInput = Input.GetAxis("Mouse Y");

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isSpaceDown = true;
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
                }

                break;

            case ModeInput.Controller:

                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");

                mouseXInput = Input.GetAxis("Mouse X");
                mouseYInput = Input.GetAxis("Mouse Y");

                if (Input.GetButtonDown("XboxA"))
                {
                    isSpaceDown = true;
                }

                break;

            case ModeInput.Intern:

                horizontalInput = 0f;
                verticalInput = 1f;

                mouseXInput = 0f;
                mouseYInput = 0f;

                isSpaceDown = false;

                break;

            default:

                break;
        }
    }

    private void FixedUpdate()
    {
        switch (currentModeInput)
        {
            case ModeInput.Keyboard:

                if (isSpaceDown)
                {
                    isSpaceDownFixed = true;
                    isSpaceDown = false;
                }
                else
                {
                    isSpaceDownFixed = false;
                }

                isSpaceJump = Input.GetKey(KeyCode.Space);

                break;

            case ModeInput.Controller:

                if (isSpaceDown)
                {
                    isSpaceDownFixed = true;
                    isSpaceDown = false;
                }
                else
                {
                    isSpaceDownFixed = false;
                }

                isSpaceJump = Input.GetButton("XboxA");

                break;

            case ModeInput.Intern:

                isSpaceDown = false;

                break;

            default:

                break;
        }
    }
}
