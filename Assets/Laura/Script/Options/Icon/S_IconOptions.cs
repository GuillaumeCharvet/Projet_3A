using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_IconOptions : MonoBehaviour
{
    [SerializeField] private GameObject Display;
    [SerializeField] private GameObject Sounds;
    [SerializeField] private GameObject ControlsController;
    [SerializeField] private GameObject ControlsKeyboard;

    public void ActivateMenuDisplay()
    {
        Display.SetActive(true);
        Sounds.SetActive(false);
        ControlsController.SetActive(false);
        ControlsKeyboard.SetActive(false);
    }

    public void ActivateMenuSounds()
    {
        Display.SetActive(false);
        Sounds.SetActive(true);
        ControlsController.SetActive(false);
        ControlsKeyboard.SetActive(false);
    }

    public void ActivateMenuControlsController()
    {
        Display.SetActive(false);
        Sounds.SetActive(false);
        ControlsController.SetActive(true);
        ControlsKeyboard.SetActive(false);
    }

    public void ActivateMenuControlsKeyboard()
    {
        Display.SetActive(false);
        Sounds.SetActive(false);
        ControlsController.SetActive(false);
        ControlsKeyboard.SetActive(true);
    }
}