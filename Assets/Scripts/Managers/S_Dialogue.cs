using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;


public class S_Dialogue : MonoBehaviour
{
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
    public GameObject reward1_1;
    public GameObject reward1_2;
    public GameObject reward1_3;
    public GameObject reward1_4;

    public AudioClip nextTextSound;
    public AudioSource soundsource;

    public AudioClip blablaSound;


    

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

        var orNTM = false;
        for (int i = 1; i < dialoguebools.Length; i++)
        {
            orNTM = orNTM || dialoguebools[i];
        }
        dialoguebools[0] = !orNTM;


        ///// activation de phrases //
        if (!reward1_1.activeInHierarchy && !r1_1HBS)
        {
           // Debug.Log("tu as dégagé le booléen mon pote");
            dialoguebools[1] = true;
        }
        if (!reward1_2.activeInHierarchy && !r1_2HBS)
        {
            dialoguebools[2] = true;
        }
        if (!reward1_3.activeInHierarchy && !r1_3HBS)
        {
            dialoguebools[3] = true;
        }
        if (!reward1_4.activeInHierarchy && !r1_4HBS)
        {
            dialoguebools[4] = true;
            //Debug.Log("phrase 4 activé");
        }

        //// desactivation de phrases ///////////
        /*
        var orNTM = false;
        for (int i = 1; i < 9; i++)
        {
            orNTM = orNTM || dialoguebools[i];
        }

        if (orNTM)
        {
            dialoguebools[0] = false;
            
        }
        if (!orNTM)
        {
            dialoguebools[0] = true ;

        }
        */

        if (dialogueHBS[1])
        {
            r1_1HBS = true;
        }

        if (dialogueHBS[2])
        {
            r1_2HBS = true;
        }

        if (dialogueHBS[3])
        {
            r1_3HBS = true;
        }

        if (dialogueHBS[4])
        {
            r1_4HBS = true;
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
        yield return new WaitForSeconds(1);
        Debug.Log("c'est bon ça fait 1 sec");
        foreach (char c in dialoguelines[index].ToCharArray())
        {
            //dialoguebools[index] = false;
            
            textComponentTechno.text += c;
            yield return new WaitForSeconds(textSpeed);
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
        
        bool nextLineFound = false;
        while (!nextLineFound)
        {
            if (index < dialoguelines.Length - 1)
            {
                
                Debug.Log("ducoup le bool passe faux là");
                if (dialoguebools[index + 1] && !dialogueHBS[index + 1])
                {
                    dialoguebools[index] = false;
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
                    dialoguebools[index] = false;
                    index++;
                }
            }
            else
            {
                dialoguebools[index] = false;
                textComponentTechno.text = string.Empty;
                
                nextLineFound = true;
                dialogueIsActive = false;
                dialoguebools[0] = true;
                index = 0;
                Debug.Log("index à 0");
                dialogueCanva.SetActive(false);

            }
        }        
    }
    
}
