using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_IconOptions : MonoBehaviour
{
    [SerializeField] private GameObject Display;
    [SerializeField] private GameObject Sounds;
    [SerializeField] private GameObject Controls;

    public void ActivateMenuDisplay()
    {
        Display.SetActive(true);
        Sounds.SetActive(false);
        Controls.SetActive(false);
    }

    public void ActivateMenuSounds()
    {
        Display.SetActive(false);
        Sounds.SetActive(true);
        Controls.SetActive(false);
    }

    public void ActivateMenuControls()
    {
        Display.SetActive(false);
        Sounds.SetActive(false);
        Controls.SetActive(true);
    }
}