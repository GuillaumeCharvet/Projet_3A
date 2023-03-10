using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ModeInput { Keyboard, Controller, Intern };
public class InputManager : Manager
{
    public ModeInput currentModeInput;

    [SerializeField] private Transform trsfCams;

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

    private void Start()
    {
        switch (currentModeInput)
        {
            case ModeInput.Keyboard:

                Transform[] allChildren = trsfCams.GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    var cineFree = child.GetComponent<CinemachineFreeLook>();
                    if (cineFree != null)
                    {
                        child.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "Mouse Y";
                        child.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
                    }
                }

                break;

            case ModeInput.Controller:

                Transform[] allChildren2 = trsfCams.GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren2)
                {
                    var cineFree = child.GetComponent<CinemachineFreeLook>();
                    if (cineFree != null)
                    {
                        child.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "HorizontalR";
                        child.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "VerticalR";
                    }
                }

                break;

            case ModeInput.Intern:

                break;

            default:

                break;
        }
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

                horizontalInput = Input.GetAxis("HorizontalL");
                verticalInput = Input.GetAxis("VerticalL");

                mouseXInput = Input.GetAxis("HorizontalR");
                mouseYInput = Input.GetAxis("VerticalR");

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
