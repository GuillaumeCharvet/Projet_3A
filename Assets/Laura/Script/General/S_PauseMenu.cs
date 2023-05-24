using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuMain;
    [SerializeField] private bool isPaused;

   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }

        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        PauseMenuMain.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PauseMenuMain.SetActive(false);
        isPaused = false;
    }
}
