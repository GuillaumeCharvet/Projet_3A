using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_QuitGame : MonoBehaviour
{
    public void Quit(string QuitGame)
    {
        Application.Quit();
        Debug.Log("Game off");
    }
}

