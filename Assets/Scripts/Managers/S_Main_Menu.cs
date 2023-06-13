using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class S_Main_Menu : MonoBehaviour
{

    public CursorManager cursor;
    public GameObject itemsForMenu;
    public GameObject menuMainCanva;
    public GameObject menuMainOptions;
    public GameObject menuOptions;
    public GameObject menuControls;
    public GameObject credits;


    //[SerializeField] public CinemachineVirtualCamera cam11;

    void Start()
    {
        menuMainCanva.SetActive(true);
        //cam11.Priority = 101;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// Dépliage de canva //////
    public void playGame()
    {
        menuMainCanva.SetActive(false);
        //cam11.Priority = 0;
    }

    public void playMainMenu()
    {
        menuMainCanva.SetActive(true);
        //cam11.Priority = 101;
    }
    public void playMainOptions()
    {
        menuMainCanva.SetActive(false);
        menuMainOptions.SetActive(true);
    }
    public void playOptions()
    {
        menuMainOptions.SetActive(false);
        menuOptions.SetActive(true);
    }
    public void playControls()
    {
        menuMainOptions.SetActive(false);
        menuControls.SetActive(true);
    }

    public void fromMainOptionsToMainMenu()
    {
       
        menuMainOptions.SetActive(false);
        menuMainCanva.SetActive(true);
    }
    /// Return to ancien canva ////
}
