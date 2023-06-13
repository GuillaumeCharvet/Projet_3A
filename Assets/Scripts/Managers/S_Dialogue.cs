using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using Cinemachine;


public class S_Dialogue : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera camDialogueParent;

    public TextMeshProUGUI textComponentTechno;
    public string[] dialoguelines;
    public bool[] dialoguebools;
    public bool[] dialogueHBS; // dialogue has been said
    public float textSpeed;

    private bool r1_1HBS = false;
    private bool r1_2HBS = false;
    private bool r1_3HBS = false;
    private bool r1_4HBS = false;

    public GameObject dialogueCanva;

    // ile 1 reward //s
    public GameObject reward1_1;
    public GameObject reward1_2;
    public GameObject reward1_3;
    public GameObject reward1_4;
    public GameObject reward1_5;
    public GameObject reward1_6;


    // ile 3 rewards //
    public GameObject reward3_1;
    public GameObject reward3_2;
    public GameObject reward3_3;
    public GameObject reward3_4;
    public GameObject reward3_5;
    public GameObject reward3_6;



    // pages carnet //

    public GameObject carnetTXT1;
    public GameObject carnetTXT2;
    public GameObject carnetTXT3;
    public GameObject carnetTXT4;
    public GameObject carnetTXT5;
    public GameObject carnetTXT6;

    public GameObject carnetTXT7;
    public GameObject carnetTXT8;
    public GameObject carnetTXT9;
    public GameObject carnetTXT10;
    public GameObject carnetTXT11;
    public GameObject carnetTXT12;

    public GameObject carnetTXT13;
    public GameObject carnetTXT14;

    // sounds //

    public AudioClip nextTextSound;
    public AudioSource soundsource;

    public AudioClip blablaSound;

    //

    private int index;
    private bool dialogueIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        /*textComponentTechno.text = string.Empty;
        StartDialogue();
        dialogueIsActive = true;
        */
        
    }

    // Update is called once per frame
    void Update()
    {
        

        //Debug.Log("******* BOOL 1 : " + dialoguebools[1]);
        var orNTM = false;
        for (int i = 1; i < dialoguebools.Length; i++)
        {
            orNTM = orNTM || dialoguebools[i];
        }
        dialoguebools[0] = !orNTM;


        ///// activation de phrases //
        

        //phrases de l'ile 1 //

        if (!reward1_1.activeInHierarchy && !dialogueHBS[1] && !dialogueHBS[2])
        {
         
            dialoguebools[1] = true;
            dialoguebools[2] = true;
           
        }
        if (!reward1_2.activeInHierarchy && !dialogueHBS[3] && !dialogueHBS[4])
        {
            dialoguebools[3] = true;
            dialoguebools[4] = true;
        }
        if (!reward1_3.activeInHierarchy && !dialogueHBS[5] && !dialogueHBS[6])
        {
            dialoguebools[5] = true;
            dialoguebools[6] = true;
        }
        if (!reward1_4.activeInHierarchy && !dialogueHBS[7] && !dialogueHBS[8])
        {
            dialoguebools[7] = true;
            dialoguebools[8] = true;
           
        }
        if (!reward1_5.activeInHierarchy && !dialogueHBS[9] && !dialogueHBS[10])
        {
            dialoguebools[9] = true;
            dialoguebools[10] = true;
        }
        if (!reward1_6.activeInHierarchy && !dialogueHBS[11] && !dialogueHBS[12])
        {
            dialoguebools[11] = true;
            dialoguebools[12] = true;
        }

        //phrases de l'ile 3 //

        if (!reward3_1.activeInHierarchy && !dialogueHBS[13] && !dialogueHBS[14])
        {
            dialoguebools[13] = true;
            dialoguebools[14] = true;
        }
        if (!reward3_2.activeInHierarchy && !dialogueHBS[15] && !dialogueHBS[16])
        {
            dialoguebools[15] = true;
            dialoguebools[16] = true;
        }
        if (!reward3_3.activeInHierarchy && !dialogueHBS[17] && !dialogueHBS[18])
        {
            dialoguebools[17] = true;
            dialoguebools[18] = true;
        }
        if (!reward3_4.activeInHierarchy && !dialogueHBS[19] && !dialogueHBS[20])
        {
            dialoguebools[19] = true;
            dialoguebools[20] = true;
        }
        if (!reward3_5.activeInHierarchy && !dialogueHBS[21] && !dialogueHBS[22])
        {
            dialoguebools[21] = true;
            dialoguebools[22] = true;
        }
        if (!reward3_6.activeInHierarchy && !dialogueHBS[23] && !dialogueHBS[24])
        {
            dialoguebools[23] = true;
            dialoguebools[24] = true;
        }


        // dialogues bonus // 

        // phrase 1 / ile  1  ///
        if (dialogueHBS[2] && dialogueHBS[4] && dialogueHBS[6] && dialogueHBS[8] && dialogueHBS[10] && dialogueHBS[12] && !dialogueHBS[25])
        {
            dialoguebools[25] = true;
        }


        // phrase 3 / ile  3  ///
        if (dialogueHBS[14] && dialogueHBS[16] && dialogueHBS[18] && dialogueHBS[20] && dialogueHBS[22] && dialogueHBS[24] && !dialogueHBS[26])
        {
            dialoguebools[26] = true;
        }


        // phrase final //

        if (dialogueHBS[25] && dialogueHBS[26] && !dialogueHBS[27])
        {
            dialoguebools[27] = true;
        }


    // affichage dalogues dans le carnet //

        // ile  //
        if (dialogueHBS[2])
        {
            carnetTXT1.SetActive(true);
        }
        if (dialogueHBS[4])
        {
            carnetTXT2.SetActive(true);
        }
        if (dialogueHBS[6])
        {
            carnetTXT3.SetActive(true);
        }
        if (dialogueHBS[8])
        {
            carnetTXT4.SetActive(true);
        }

        // ile 2 //

        if (dialogueHBS[10])
        {
            carnetTXT5.SetActive(true);
        }
        if (dialogueHBS[12])
        {
            carnetTXT6.SetActive(true);
        }
        if (dialogueHBS[14])
        {
            carnetTXT7.SetActive(true);
        }
        if (dialogueHBS[16])
        {
            carnetTXT8.SetActive(true);
        }

        // ile 3 //

        if (dialogueHBS[18])
        {
            carnetTXT9.SetActive(true);
        }
       if (dialogueHBS[20])
            {
            carnetTXT10.SetActive(true);
        }
        if (dialogueHBS[22])
        {
            carnetTXT11.SetActive(true);
        }
        if (dialogueHBS[24])
        {
            carnetTXT12.SetActive(true);
        }


        // pharse bonus //

        if (dialogueHBS[25])
        {
            carnetTXT13.SetActive(true);
        }
        if (dialogueHBS[26])
        {
            carnetTXT14.SetActive(true);
        }
        
        ////////////// dialogue ////////////

        if (dialogueCanva.activeInHierarchy && !dialogueIsActive)
        {
            dialogueIsActive = true;
            textComponentTechno.text = string.Empty;
            StartDialogue();
            //dialogueIsActive = true;
        }
        if (Input.GetMouseButtonDown(0) && dialogueIsActive)
        {
            if (textComponentTechno.text == dialoguelines[index])
            {
                NextLine();              
            }
            else
            {
                StopAllCoroutines();
                textComponentTechno.text = dialoguelines[index];  
            }
        }
    }


    void StartDialogue()
    {
        camDialogueParent.Priority = 101;
        index = 0;
        var nextBoolFound = false;
        while (!nextBoolFound)
        {
            if (index < dialoguebools.Length - 1)
            {

                Debug.Log("ducoup le bool passe faux là");
                if (!dialoguebools[index])
                {
                    index++;
                }
                else
                {
                    nextBoolFound = true;
                }

            }
        }
        /* for (int i = 0; i < dialoguebools.Length; i++)
         {


             Debug.Log("i = " + i);
             if (!dialoguebools[i])
             {
                 index++;
             }
             if (dialoguebools[i])
             {
                 index = i;
                 Debug.Log("indexe donné");
                 break;
             }
         }   
        */
       
        StartCoroutine(Typeline());
        
    }
    IEnumerator Typeline()
    {
        Debug.Log("attend 1 second mon djo");
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("c'est bon ça fait 1 sec");
        foreach (char c in dialoguelines[index].ToCharArray())
        {
            //dialoguebools[index] = false;
            
            textComponentTechno.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
            Debug.Log("le premier message apparait");
            
            

        }
    }
    /*IEnumerator waiter()
    {
        Debug.Log("attend 5 second mon djo");
        yield return new WaitForSeconds(5);
        Debug.Log("c'est bon ça fait 5 sec");
    }
    */
    void NextLine()
    {
        //index = 0;
        //dialoguebools[index] = false;
        dialogueHBS[index] = true;
        bool nextLineFound = false;
        while (!nextLineFound)
        {
            dialoguebools[index] = false;
            if (index < dialoguelines.Length - 1)
            {
                Debug.Log("ducoup le bool passe faux là");
                if (dialoguebools[index + 1] && !dialogueHBS[index + 1])
                {
                    dialogueHBS[index + 1] = true;
                    index++;
                   
                    textComponentTechno.text = string.Empty;
                    StartCoroutine(Typeline());
                    nextLineFound = true;
                    //index = 0;
                    soundsource.PlayOneShot(nextTextSound);
                }
                else
                {
                    //Debug.Log("l'index grimpe mon pote");
                    index++;
                }
            }
            else
            {
                textComponentTechno.text = string.Empty;
                
                nextLineFound = true;
                dialogueIsActive = false;
                dialoguebools[0] = true;
                index = 0;
                Debug.Log("index à 0");
                dialogueCanva.SetActive(false);
                //Time.timeScale = 1;
                camDialogueParent.Priority = 0;
                ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = true ;

                
            }
        }        
    }
    
}
