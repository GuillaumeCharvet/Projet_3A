using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : Manager
{

    public GameObject menuMainCanva;
    //float mouseVertical = 0f, mouseHorizontal = 0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (menuMainCanva.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    /*void OnGUI()
    {
        //Press this button to lock the Cursor
        if (GUI.Button(new Rect(0, 0, 100, 50), "Lock Cursor"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Press this button to confine the Cursor within the screen
        if (GUI.Button(new Rect(125, 0, 100, 50), "Confine Cursor"))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }*/

    /*private void FirstPersonCursorCtrl()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            mouseVertical -= Input.GetAxis("Mouse Y") * Time.deltaTime * 100f;
            mouseHorizontal += Input.GetAxis("Mouse X") * Time.deltaTime * 100f;



            camRotation = Quaternion.Euler(mouseVertical, mouseHorizontal, 0f);

            playerRigs.transform.rotation = camRotation;

        }
    }*/
}