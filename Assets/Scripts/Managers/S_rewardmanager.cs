using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_rewardmanager : MonoBehaviour
{
    [SerializeField]
    // REWARDS TECHNOS //
    //ile 1 //

    public GameObject Ile1_stele_1;
    public GameObject Ile1_stele_2;
    public GameObject Ile1_stele_3;
    public GameObject Ile1_stele_4;
    
    //ile 2 //

    public GameObject Ile2_stele_1;
    public GameObject Ile2_stele_2;
    public GameObject Ile2_stele_3;
    public GameObject Ile2_stele_4;

    //ile 3 //
    public GameObject Ile3_stele_1;
    public GameObject Ile3_stele_2;
    public GameObject Ile3_stele_3;
    public GameObject Ile3_stele_4;

    // REWARDS NATURE //
    /*
    public GameObject Ile1_nature_1;
    public GameObject Ile1_nature_2;
    public GameObject Ile1_nature_3;
    public GameObject Ile1_nature_4;

    public GameObject Ile2_nature_1;
    public GameObject Ile2_nature_2;
    public GameObject Ile2_nature_3;
    public GameObject Ile2_nature_4;
    */
    //Canva // 

    public GameObject talkCanva;
    public GameObject dialogueCanva;

    // texte //
    //dialogue techno //
    public GameObject dialogue0;

    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject dialogue3;
    public GameObject dialogue4;

    public GameObject dialogue5;
    public GameObject dialogue6;
    public GameObject dialogue7;
    public GameObject dialogue8;

    public GameObject dialogue9;
    public GameObject dialogue10;
    public GameObject dialogue11;
    public GameObject dialogue12;
    //public GameObject dialogue1ET2;


    ////////////// booleans //////////////////////

    // Phrase active //

    private bool phraseZero = true;
    private bool phraseOne = false;
    private bool phraseTwo = false;
    private bool phraseThree = false;
    private bool phraseFour = false;

    private bool phraseFive = false;
    private bool phraseSix = false;
    private bool phraseSeven = false;
    private bool phraseEight = false;

    private bool phraseNine = false;
    private bool phraseTen = false;
    private bool phraseEleven = false;
    private bool phraseTwelve= false;



    private bool dialogueActif = false;
    private bool isIn = false;

    /// has been said ///
    /// technos

    public bool p1_1hbs = false;
    public bool p1_2hbs = false;
    public bool p1_3hbs = false;
    public bool p1_4hbs = false;


    public bool p2_1hbs = false;
    public bool p2_2hbs = false;
    public bool p2_3hbs = false;
    public bool p2_4hbs = false;

    public bool p3_1hbs = false;
    public bool p3_2hbs = false;
    public bool p3_3hbs = false;
    public bool p3_4hbs = false;


    ////////////////////// existe encore /////////////////////////

    // ile 1 rewards technos // 
    private bool s1_1exist = true;
    private bool s1_2exist = true;
    private bool s1_3exist = true;
    private bool s1_4exist = true;

    // ile 2 rewards technos // 
    private bool s2_1exist = true;
    private bool s2_2exist = true;
    private bool s2_3exist = true;
    private bool s2_4exist = true;

    // ile 3 rewards technos // 
    private bool s3_1exist = true;
    private bool s3_2exist = true;
    private bool s3_3exist = true;
    private bool s3_4exist = true;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("est rentré dans le collider");
            talkCanva.SetActive(true);
            isIn = true; 
        }      
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("sort du collider");
            talkCanva.SetActive(false);
            dialogueActif = false;
            isIn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //stèles présentent dans la scène //
        if (Ile2_stele_1.activeInHierarchy == false && s2_1exist )
        {
            Debug.Log("stele 1 a disparut");
            phraseZero = false;
            phraseFive = true;
            s2_1exist = false;
        }
        if (Ile2_stele_2.activeInHierarchy == false && s2_2exist)
        {
            Debug.Log("stele 2 a disparut");
            phraseZero = false;
            phraseSix = true;
            s2_2exist = false;
        }

        if (Ile2_stele_3.activeInHierarchy == false && s2_3exist)
        {
            Debug.Log("le morceau 3 est prit");
            phraseZero = false;
            phraseSeven = true;
            s2_3exist = false;
        }
        if (Ile2_stele_4.activeInHierarchy == false && s2_4exist)
        {
            Debug.Log("yes bébé, la 4ème stèle est ramassé");
            phraseZero = false;
            phraseEight = true;
            s2_4exist = false;
        }

        ////// combinaisons ////////

        /*if (Ile2_stele_2.activeInHierarchy == false && Ile2_stele_1.activeInHierarchy == false)
        {
            Debug.Log("stele 1 et 2 ont disparut");
            phraseZero = false;
            phraseOne = false;
            phraseTwo = false;
            phraseOneAndTwo = true;
        }
        */


        // active le dialogue // 
        if (isIn && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("je veux te parler");
            talkCanva.SetActive(false);
            dialogueCanva.SetActive(true);
            dialogueActif = true;
        }


        // choix du dialogue selon les stèles ramassées//
        if ( dialogueActif  )
        {
            if (phraseZero)
            {
                Debug.Log("tu n'as pas d'objet je parle pas");
                dialogue0.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogue0.SetActive(false);
                    dialogueActif = false;
                    dialogueCanva.SetActive(false);
                }
            }

            if (phraseFive && !p2_1hbs)
            {
                Debug.Log("okay 1 partie");
                dialogue5.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogue5.SetActive(false);
                    dialogueActif = false;
                    dialogueCanva.SetActive(false);
                    p2_1hbs = true;
                    phraseFive = false;
                }
            }
            if (phraseSix && !p2_2hbs)
            {
                Debug.Log("okay 2 partie");
                dialogue6.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogue6.SetActive(false);
                    dialogueActif = false;
                    dialogueCanva.SetActive(false);
                    p2_2hbs = true;
                    phraseSix = false;
                }
            }
            if (phraseSeven && !p2_3hbs)
            {
                 Debug.Log("okay 3 partie");
                 dialogue7.SetActive(true);
                 if (Input.GetKeyDown(KeyCode.E))
                 {
                     dialogue7.SetActive(false);
                     dialogueActif = false;
                     dialogueCanva.SetActive(false);
                     p2_3hbs = true;
                     phraseSeven = false;
                }
            }
            if (phraseEight && !p2_4hbs)
            {
                 Debug.Log("okay 4 partie");
                 dialogue8.SetActive(true);
                 if (Input.GetKeyDown(KeyCode.E))
                 {
                     dialogue8.SetActive(false);
                     dialogueActif = false;
                     dialogueCanva.SetActive(false);
                     p2_4hbs = true;
                     phraseEight = false;
                 }
            }
            /*if (phraseOneAndTwo)
            {
                Debug.Log("okay 2 partie");
                dialogue1ET2.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogue1ET2.SetActive(false);
                    dialogueActif = false;
                    dialogueCanva.SetActive(false);
                }
            }
            */  
        }
    }
}