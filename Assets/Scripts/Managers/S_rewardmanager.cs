using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class S_rewardmanager : MonoBehaviour
{
    [SerializeField]
    // REWARDS TECHNOS //
    //ile 1 //

    public GameObject Ile1_reward_1;
    public GameObject Ile1_reward_2;
    public GameObject Ile1_reward_3;
    public GameObject Ile1_reward_4;
    public GameObject Ile1_reward_5;
    public GameObject Ile1_reward_6;

    //ile 3 //
    public GameObject Ile3_reward_1;
    public GameObject Ile3_reward_2;
    public GameObject Ile3_reward_3;
    public GameObject Ile3_reward_4;
    public GameObject Ile3_reward_5;
    public GameObject Ile3_reward_6;


    //Canva // 

    public GameObject talkCanva;
    public GameObject dialogueCanva;


         
    // texte //
    //dialogue techno //
   

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;

    public GameObject image7;
    public GameObject image8;
    public GameObject image9;
    public GameObject image10;
    public GameObject image11;
    public GameObject image12;



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
    public bool p1_5hbs = false;
    public bool p1_6hbs = false;


    public bool p3_1hbs = false;
    public bool p3_2hbs = false;
    public bool p3_3hbs = false;
    public bool p3_4hbs = false;
    public bool p3_5hbs = false;
    public bool p3_6hbs = false;


    ////////////////////// existe encore /////////////////////////

    // ile 1 rewards technos // 
    private bool r1_1exist = true;
    private bool r1_2exist = true;
    private bool r1_3exist = true;
    private bool r1_4exist = true;
    private bool r1_5exist = true;
    private bool r1_6exist = true;


    // ile 3 rewards technos // 
    private bool r3_1exist = true;
    private bool r3_2exist = true;
    private bool r3_3exist = true;
    private bool r3_4exist = true;
    private bool r3_5exist = true;
    private bool r3_6exist = true;







    // sons // 
    public AudioClip talkedSound;
    public AudioSource talksource;



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


        /// île 1 //////

        if (Ile1_reward_1.activeInHierarchy == false && r1_1exist)
        {
            //Debug.Log("stele 1 a disparut");
            phraseZero = false;
            phraseOne = true;
            r1_1exist = false;
            image1.SetActive(true);
        }
        if (Ile1_reward_2.activeInHierarchy == false && r1_2exist)
        {          
            phraseZero = false;
            phraseTwo = true;
            r1_2exist = false;
            image2.SetActive(true);
        }

        if (Ile1_reward_3.activeInHierarchy == false && r1_3exist)
        {
            phraseZero = false;
            phraseThree = true;
            r1_3exist = false;
            image3.SetActive(true);
        }
        if (Ile1_reward_4.activeInHierarchy == false && r1_4exist)
        {
            phraseZero = false;
            phraseFour = true;
            r1_4exist = false;
            image4.SetActive(true);
        }
        if (Ile1_reward_5.activeInHierarchy == false && r1_5exist)
        {
            //Debug.Log("stele 5 a disparut");
            phraseZero = false;
            phraseFive = true;
            r1_5exist = false;
            image5.SetActive(true);
        }
        if (Ile1_reward_6.activeInHierarchy == false && r1_6exist)
        {
            
            phraseZero = false;
            phraseSix = true;
            r1_6exist = false;
            image6.SetActive(true);
        }

 
        /// île 3 //////

        if (Ile3_reward_1.activeInHierarchy == false && r3_1exist)
        {
            //Debug.Log("stele 1 a disparut");
            phraseZero = false;
            phraseNine = true;
            r3_1exist = false;
            image7.SetActive(true);
        }
        if (Ile3_reward_2.activeInHierarchy == false && r3_2exist)
        {
            phraseZero = false;
            phraseTen = true;
            r3_2exist = false;
            image8.SetActive(true);
        }

        if (Ile3_reward_3.activeInHierarchy == false && r3_3exist)
        {
            phraseZero = false;
            phraseEleven = true;
            r3_3exist = false;
            image9.SetActive(true);
        }
        if (Ile3_reward_4.activeInHierarchy == false && r3_4exist)
        {
            phraseZero = false;
            phraseTwelve = true;
            r3_4exist = false;
            image10.SetActive(true);
        }
        if (Ile3_reward_5.activeInHierarchy == false && r3_5exist)
        {
            phraseZero = false;
            phraseTwelve = true;
            r3_5exist = false;
            image11.SetActive(true);
        }
        if (Ile3_reward_6.activeInHierarchy == false && r3_6exist)
        {
            phraseZero = false;
            phraseTwelve = true;
            r3_6exist = false;
            image12.SetActive(true);
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
            talksource.PlayOneShot(talkedSound);
            dialogueCanva.SetActive(true);
            dialogueActif = true;
            ManagerManager.Instance.GetComponent<UpdateManager>().updateActivated = false;
        }

         /*               annulé  car changement de méthode 

       // choix du dialogue selon les stèles ramassées//
       if ( dialogueActif  )
       {
           if (phraseZero)
           {
               //Debug.Log("tu n'as pas d'objet je parle pas");
               dialogue0.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue0.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
               }
           }
           //île 1 //

           if (phraseOne && !p1_1hbs)
           {
               //Debug.Log("okay 1 partie");
               dialogue1.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue1.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
                   p1_1hbs = true;
                   phraseOne = false;
               }
           }
           if (phraseTwo && !p1_2hbs)
           {
               //Debug.Log("okay 1 partie");
               dialogue2.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue2.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
                   p1_2hbs = true;
                   phraseTwo = false;
               }
           }
           if (phraseThree && !p1_3hbs)
           {
               //Debug.Log("okay 1 partie");
               dialogue3.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue3.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
                   p1_3hbs = true;
                   phraseThree = false;
               }
           }

           if (phraseFour && !p1_4hbs)
           {
               //Debug.Log("okay 1 partie");
               dialogue4.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue4.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
                   p1_4hbs = true;
                   phraseFour = false;
               }
           }

           // île 2 //

           if (phraseFive && !p2_1hbs)
           {
               //Debug.Log("okay 1 partie");
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

           //île 3 //

           if (phraseNine && !p3_1hbs)
           {
               dialogue9.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue9.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
                   p3_1hbs = true;
                   phraseNine = false;
               }
           }

           if (phraseTen && !p3_2hbs)
           {
               dialogue10.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue10.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
                   p3_2hbs = true;
                   phraseTen = false;
               }
           }

           if (phraseEleven && !p3_3hbs)
           {
               dialogue11.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue11.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
                   p3_3hbs = true;
                   phraseEleven = false;
               }
           }

           if (phraseTwelve && !p3_4hbs)
           {
               dialogue12.SetActive(true);
               if (Input.GetKeyDown(KeyCode.E))
               {
                   dialogue12.SetActive(false);
                   dialogueActif = false;
                   dialogueCanva.SetActive(false);
                   p3_4hbs = true;
                   phraseTwelve = false;
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
