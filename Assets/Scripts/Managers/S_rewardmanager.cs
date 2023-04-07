using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_rewardmanager : MonoBehaviour
{
    [SerializeField]
    // REWARDS TECHNOS //
    /*
    public GameObject Ile1_stele_1;
    public GameObject Ile1_stele_2;
    public GameObject Ile1_stele_3;
    public GameObject Ile1_stele_4;
    */

    public GameObject Ile2_stele_1;
    public GameObject Ile2_stele_2;
    public GameObject Ile2_stele_3;
    public GameObject Ile2_stele_4;

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

    public GameObject dialogue0;
    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject dialogue1ET2;

    // booleans //

    private bool phraseZero = true;
    private bool phraseOne = false;
    private bool phraseTwo = false;
    private bool phraseOneAndTwo = false;

    private bool dialogueActif = false;
    private bool isIn = false;



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
        if (Ile2_stele_1.activeInHierarchy == false && Ile2_stele_2.activeInHierarchy == true)
        {
            Debug.Log("stele 1 a disparut");
            phraseZero = false;
            phraseOne = true;
        }
        if (Ile2_stele_2.activeInHierarchy == false && Ile2_stele_1.activeInHierarchy == true)
        {
            Debug.Log("stele 2 a disparut");
            phraseZero = false;
            phraseTwo = true;
        }

        if (Ile2_stele_2.activeInHierarchy == false && Ile2_stele_1.activeInHierarchy == false)
        {
            Debug.Log("stele 1 et 2 ont disparut");
            phraseZero = false;
            phraseOne = false;
            phraseTwo = false;
            phraseOneAndTwo = true;
        }





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

            if (phraseOne)
            {
                Debug.Log("okay 1 partie");
                dialogue1.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogue0.SetActive(false);
                    dialogueActif = false;
                    dialogueCanva.SetActive(false);
                }
            }
            if (phraseTwo)
            {
                Debug.Log("okay 2 partie");
                dialogue2.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogue2.SetActive(false);
                    dialogueActif = false;
                    dialogueCanva.SetActive(false);
                }
            }
            if (phraseOneAndTwo)
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


        }

    }

}
