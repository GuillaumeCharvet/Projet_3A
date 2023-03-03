using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    //public void Start()
    //{
        // Toggle fullscreen
        //Screen.fullScreen = true;
        //Screen.fullScreen = !Screen.fullScreen;
    //}

    public void SetFullScreen(bool fullScreenValue)
    {
        Screen.fullScreen = fullScreenValue;
        if (!fullScreenValue)
        {
            Resolution resolution = Screen.currentResolution;
            Screen.SetResolution(resolution.width, resolution.height, fullScreenValue);
        }

    }

}

