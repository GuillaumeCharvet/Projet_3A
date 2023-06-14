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

    public GameObject manette;
    public GameObject keyboard;

    public bool menuIsActive = false;
    public bool playStart = false;
    

    private bool menuIsOpen = true;
    public GameObject plateformeWait;


    public GameObject dialogueIntro;
    public GameObject dialogueIntroNature;
    public GameObject dialogueIntroTechno;
    public GameObject dialogueNature;
    public GameObject dialogueTechno;
    public GameObject map;
    public GameObject Carnet1;
    public GameObject Carnet2;
    public GameObject Carnet3;
    public GameObject Carnet4;
    public GameObject tutoWalk;
    public GameObject tutoJump;
    public GameObject tutoClimb;
    public GameObject tutoGlide;
    public GameObject tutoMap;
    public GameObject tutoCloseMap;
    public GameObject getObject;
    public GameObject dropObject;
    public GameObject Talk;


    private bool dialogueIntroWasOpen = false;
    private bool dialogueIntroNatureWasOpen = false;
    private bool dialogueIntroTechnoWasOpen = false;
    private bool dialogueNatureWasOpen = false;
    private bool dialogueTechnoWasOpen = false;
    private bool mapWasOpen = false;
    private bool Carnet1WasOpen = false;
    private bool Carnet2WasOpen = false;
    private bool Carnet3WasOpen = false;
    private bool Carnet4WasOpen = false;
    private bool tutoWalkWasOpen = false;
    private bool tutoJumpWasOpen = false;
    private bool tutoClimbWasOpen = false;
    private bool tutoGlideWasOpen = false;
    private bool tutoMapWasOpen = false;
    private bool tutoCloseMapWasOpen = false;
    private bool getObjectWasOpen = false;
    private bool dropObjectWasOpen = false;
    private bool TalkWasOpen = false;

    public bool pause = false;
   



    [SerializeField] public CinemachineVirtualCamera camMenu;

    void Start()
    {
        //camMenu.Priority = 101;
      
        

        //cam11.Priority = 101;
       
    }

    // Update is called once per frame
    void Update()
    {

        /// condition canva /// 
        
        // dialogue intro
        if (dialogueIntro.activeInHierarchy)
        {
            dialogueIntroWasOpen = true;
        }
        if (!dialogueIntro.activeInHierarchy && !pause)
        {
            dialogueIntroWasOpen = false;
        }
        // dialogue introNature
        if (dialogueIntroNature.activeInHierarchy)
        {
            dialogueIntroNatureWasOpen = true;
        }
        if (!dialogueIntroNature.activeInHierarchy && !pause)
        {
            dialogueIntroNatureWasOpen = false;
        }
        // dialogue introTechno
        if (dialogueIntroTechno.activeInHierarchy)
        {
            dialogueIntroTechnoWasOpen = true;
        }
        if (!dialogueIntroTechno.activeInHierarchy && !pause)
        {
            dialogueIntroTechnoWasOpen = false;
        }
        // dialogue dialogueNature
        if (dialogueNature.activeInHierarchy)
        {
            dialogueIntroNatureWasOpen = true;
        }
        if (!dialogueNature.activeInHierarchy && !pause)
        {
            dialogueIntroNatureWasOpen = false;
        }
        // dialogue dialogueTechno
        if (dialogueTechno.activeInHierarchy)
        {
            dialogueTechnoWasOpen = true;
        }
        if (!dialogueTechno.activeInHierarchy && !pause)
        {
            dialogueTechnoWasOpen = false;
        }
        // map
        if (map.activeInHierarchy)
        {
            mapWasOpen = true;
        }
        if (!map.activeInHierarchy && !pause)
        {
            mapWasOpen = false;
        }
        // carnet1
        if (Carnet1.activeInHierarchy)
        {
            Carnet1WasOpen = true;
        }
        if (!Carnet1.activeInHierarchy && !pause)
        {
            Carnet1WasOpen = false;
        }
        // carnet2
        if (Carnet2.activeInHierarchy)
        {
            Carnet2WasOpen = true;
        }
        if (!Carnet2.activeInHierarchy && !pause)
        {
            Carnet2WasOpen = false;
        }
        // carnet3
        if (Carnet3.activeInHierarchy)
        {
            Carnet3WasOpen = true;
        }
        if (!Carnet3.activeInHierarchy && !pause)
        {
            Carnet3WasOpen = false;
        }
        // carnet1
        if (Carnet4.activeInHierarchy)
        {
            Carnet4WasOpen = true;
        }
        if (!Carnet4.activeInHierarchy && !pause)
        {
            Carnet4WasOpen = false;
        }
        // tuto walk
        if (tutoWalk.activeInHierarchy)
        {
            tutoWalkWasOpen = true;
        }
        if (!tutoWalk.activeInHierarchy && !pause)
        {
            tutoWalkWasOpen = false;
        }
        // tuto jump
        if (tutoJump.activeInHierarchy)
        {
            tutoJumpWasOpen = true;
        }
        if (!tutoJump.activeInHierarchy && !pause)
        {
            tutoJumpWasOpen = false;
        }
        // tuto climb
        if (tutoClimb.activeInHierarchy)
        {
            tutoClimbWasOpen = true;
        }
        if (!tutoClimb.activeInHierarchy && !pause)
        {
            tutoClimbWasOpen = false;
        }
        // tuto climb
        if (tutoClimb.activeInHierarchy)
        {
            tutoClimbWasOpen = true;
        }
        if (!tutoClimb.activeInHierarchy && !pause)
        {
            tutoClimbWasOpen = false;
        }
        // tuto glide
        if (tutoGlide.activeInHierarchy)
        {
            tutoGlideWasOpen = true;
        }
        if (!tutoGlide.activeInHierarchy && !pause)
        {
            tutoGlideWasOpen = false;
        }
        // tuto map
        if (tutoMap.activeInHierarchy)
        {
            tutoMapWasOpen = true;
        }
        if (!tutoMap.activeInHierarchy && !pause)
        {
            tutoMapWasOpen = false;
        }
        // tuto close map
        if (tutoCloseMap.activeInHierarchy)
        {
            tutoCloseMapWasOpen = true;
        }
        if (!tutoCloseMap.activeInHierarchy && !pause)
        {
            tutoCloseMapWasOpen = false;
        }
        // tuto close map
        if (tutoCloseMap.activeInHierarchy)
        {
            tutoCloseMapWasOpen = true;
        }
        if (!tutoCloseMap.activeInHierarchy && !pause)
        {
            tutoCloseMapWasOpen = false;
        }
        // getObject
        if (getObject.activeInHierarchy)
        {
            getObjectWasOpen = true;
        }
        if (!getObject.activeInHierarchy && !pause)
        {
            getObjectWasOpen = false;
        }
        // talk
        if (Talk.activeInHierarchy)
        {
            TalkWasOpen = true;
        }
        if (!Talk.activeInHierarchy && !pause)
        {
            TalkWasOpen = false;
        }
        // drop object
        if (dropObject.activeInHierarchy)
        {
            dropObjectWasOpen = true;
        }
        if (!dropObject.activeInHierarchy && !pause)
        {
            dropObjectWasOpen = false;
        }













        /// reopen menu ///
        if (Input.GetKeyDown(KeyCode.Escape) && !menuIsOpen)
            {
            Debug.Log("menu on");
            menuMainCanva.SetActive(true);
            menuIsOpen = true;
            camMenu.Priority = 101;
            itemsForMenu.SetActive(true);

            // dialogues //

            if(dialogueNatureWasOpen)
            {
               pause = true;
               dialogueIntroNature.SetActive(false);
            }

            if (dialogueIntroWasOpen)
            {
                pause = true;
                dialogueIntro.SetActive(false);
            }

            if (dialogueIntroTechnoWasOpen)
            {
                pause = true;
                dialogueTechno.SetActive(false);
            }
            if (dialogueNatureWasOpen)
            {
                pause = true;
                dialogueNature.SetActive(false);
            }
            if (dialogueTechnoWasOpen)
            {
                pause = true;
                dialogueTechno.SetActive(false);
            }
            if (mapWasOpen)
            {
                pause = true;
                map.SetActive(false);
            }
            if (Carnet1WasOpen)
            {
                pause = true;
                Carnet1.SetActive(false);
            }
            if (Carnet2WasOpen)
            {
                pause = true;
                Carnet2.SetActive(false);
            }
            if (Carnet3WasOpen)
            {
                pause = true;
                Carnet3.SetActive(false);
            }
            if (Carnet4WasOpen)
            {
                pause = true;
                Carnet4.SetActive(false);
            }
            if (tutoWalkWasOpen)
            {
                pause = true;
                tutoWalk.SetActive(false);
            }
            if (tutoJumpWasOpen)
            {
                pause = true;
                tutoJump.SetActive(false);
            }
            if (tutoClimbWasOpen)
            {
                pause = true;
                tutoClimb.SetActive(false);
            }
            if (tutoGlideWasOpen)
            {
                pause = true;
                tutoGlide.SetActive(false);
            }
            if (tutoMapWasOpen)
            {
                pause = true;
                tutoMap.SetActive(false);
            }
            if (tutoCloseMapWasOpen)
            {
                pause = true;
                tutoCloseMap.SetActive(false);
            }
            if (getObjectWasOpen)
            {
                pause = true;
                getObject.SetActive(false);
            }
            if (dropObjectWasOpen)
            {
                pause = true;
                dropObject.SetActive(false);
            }
            if (TalkWasOpen)
            {
                pause = true;
                Talk.SetActive(false);
            }
        }
        



    }
    /// Dépliage de canva //////
    public void playGame()
    {
        Debug.Log("menu off");
        menuMainCanva.SetActive(false);
        plateformeWait.SetActive(false);
        camMenu.Priority = 0;
        menuIsOpen = false;
        itemsForMenu.SetActive(false);
        playStart = true;
        Debug.Log("l'intro doit se lancer");

        if (dialogueIntroWasOpen)
        {
            dialogueIntro.SetActive(true);
            pause = false;
        }
        if (dialogueIntroNatureWasOpen)
        {
            dialogueIntroNature.SetActive(true);
            pause = false;
        }
        if (dialogueIntroTechnoWasOpen)
        {
            dialogueIntroTechno.SetActive(true);
            pause = false;
        }
        if (dialogueNatureWasOpen)
        {
            dialogueNature.SetActive(true);
            pause = false;
        }
        if (dialogueTechnoWasOpen)
        {
            dialogueTechno.SetActive(true);
            pause = false;
        }
        if (mapWasOpen)
        {
            map.SetActive(true);
            pause = false;
        }
        if (Carnet1WasOpen)
        {
            Carnet1.SetActive(true);
            pause = false;
        }
        if (Carnet2WasOpen)
        {
            Carnet2.SetActive(true);
            pause = false;
        }
        if (Carnet3WasOpen)
        {
            Carnet3.SetActive(true);
            pause = false;
        }
        if (Carnet4WasOpen)
        {
            Carnet4.SetActive(true);
            pause = false;
        }
        if (tutoWalkWasOpen)
        {
            tutoWalk.SetActive(true);
            pause = false;
        }
        if (tutoJumpWasOpen)
        {
            tutoJump.SetActive(true);
            pause = false;
        }
        if (tutoClimbWasOpen)
        {
            tutoClimb.SetActive(true);
            pause = false;
        }
        if (tutoGlideWasOpen)
        {
            tutoGlide.SetActive(true);
            pause = false;
        }
        if (tutoMapWasOpen)
        {
            tutoMap.SetActive(true);
            pause = false;
        }
        if (tutoCloseMapWasOpen)
        {
            tutoCloseMap.SetActive(true);
            pause = false;
        }
        if (getObjectWasOpen)
        {
            getObject.SetActive(true);
            pause = false;
        }
        if (dropObjectWasOpen)
        {
            dropObject.SetActive(true);
            pause = false;
        }
        if (TalkWasOpen)
        {
            Talk.SetActive(true);
            pause = false;
        }







      

  
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

    public void playCredit()
    {
        menuMainOptions.SetActive(false);
        credits.SetActive(true);
    }

    




    /// Return to ancien canva ////

    public void fromMainOptionsToMainMenu()
    {

        menuMainOptions.SetActive(false);
        menuMainCanva.SetActive(true);
    }

    public void fromControlsToOptions()
    {
        if (keyboard.activeInHierarchy)
        {
            keyboard.SetActive(false);
        }
        if (manette.activeInHierarchy)
        {
            manette.SetActive(false);
        }
        menuControls.SetActive(false);
        menuMainOptions.SetActive(true); ;

    }

    public void fromCreditToMainOptions()

    {      
        credits.SetActive(false);
        menuMainOptions.SetActive(true);
    }

    /// change info ///
    
    public void activePad()
    {
        if (keyboard.activeInHierarchy)
        {
            keyboard.SetActive(false);
        }
        manette.SetActive(true);
    }

    public void activeKeyboard()
    {
        if (manette.activeInHierarchy)
        {
            manette.SetActive(false);
        }
        keyboard.SetActive(true);
    }

    /// quite le jeu///
     
    public void quitGame()
    {
        Application.Quit();
    }


    



}
