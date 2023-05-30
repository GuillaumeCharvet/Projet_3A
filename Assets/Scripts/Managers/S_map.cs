using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class S_map : MonoBehaviour

{
    [SerializeField]
    public S_rewardmanager s_RewardmanagerTechno;

    public S_rewardmanager s_RewardmanagerNature;

    // canvas fonds //////

    public GameObject canvaMap;
    public GameObject canvaC1; // CanvaC1: Canva Carnet 1 (première double page)
    public GameObject canvaC2;
    public GameObject canvaC3;
    public GameObject canvaC4;


    // Textes et images des canvas //////////

    // double page 1 //////

    // textes //   nomenclature: dp1T1 = Double Page 1 Texte 1

    public GameObject dp1T1;
    public GameObject dp1T2;
    public GameObject dp1T3;
    public GameObject dp1T4;
    public GameObject dp1T5;
    public GameObject dp1T6;

    //images //  nomenclature: dp1I1 = Double Page 1 Image 1

    public GameObject dp1I1;
    public GameObject dp1I2;
    public GameObject dp1I3;
    public GameObject dp1I4;
    public GameObject dp1I5;
    public GameObject dp1I6;
    public GameObject dp1TB; // nomenclature: double page 1 texte bonus
   

    // double page 2 //////

    // textes //   nomenclature: dp2T1 = Double Page 2 Texte 1

    public GameObject dp2T1;
    public GameObject dp2T2;
    public GameObject dp2T3;
    public GameObject dp2T4;
    public GameObject dp2T5;
    public GameObject dp2T6;
    public GameObject dp2TB; // nomenclature: double page 2 texte bonus

    //images //  nomenclature: dp2I1 = Double Page 21 Image 1

    public GameObject dp2I1;
    public GameObject dp2I2;
    public GameObject dp2I3;
    public GameObject dp2I4;
    public GameObject dp2I5;
    public GameObject dp2I6;

    // double page 3 //////

    // textes //   nomenclature: dp2T1 = Double Page 2 Texte 1

    public GameObject dp3T1;
    public GameObject dp3T2;
    public GameObject dp3T3;
    public GameObject dp3T4;
    public GameObject dp3T5;
    public GameObject dp3T6;

    //images //  nomenclature: dp2I1 = Double Page 21 Image 1

    public GameObject dp3I1;
    public GameObject dp3I2;
    public GameObject dp3I3;
    public GameObject dp3I4;
    public GameObject dp3I5;
    public GameObject dp3I6;
    public GameObject dp3TB; // nomenclature: double page 3 texte bonus

    // double page 4 //////

    // textes //

    public GameObject dp4T1;
    public GameObject dp4T2;
    public GameObject dp4T3;
    public GameObject dp4T4;
    public GameObject dp4T5;
    public GameObject dp4T6;

    //images //

    public GameObject dp4I1;
    public GameObject dp4I2;
    public GameObject dp4I3;
    public GameObject dp4I4;
    public GameObject dp4I5;
    public GameObject dp4I6;
    public GameObject dp4TB;
  


    private bool carnetIsOpen = false;
    private bool keyIUp = true;

    //sons //

    public AudioClip bookOpen;
    public AudioClip pageTurn;
    public AudioClip closeBook;

    public AudioSource owl;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !carnetIsOpen)
        {
            canvaMap.SetActive(true);
            carnetIsOpen = true;
            owl.PlayOneShot(bookOpen);
            Debug.Log("Hop on ouvre le carnet");

            keyIUp = false;

            Debug.Log("******* I : KEY DOWN - OPEN INVENTORY ******* " + keyIUp);
            /*if (s_Rewardmanager.p1hbs)
            {
            }
            */

            /* if (Input.GetKeyDown(KeyCode.M))
             {
                 canvaMap.SetActive(false);
             }*/
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            keyIUp = true;
            Debug.Log("******* I : KEY UP ******* " + keyIUp);
        }

        /*
        if (Input.GetKeyDown(KeyCode.I) && carnetHBS)
        {
            canvaMap.SetActive(false);
            carnetHBS = false;
            owl.PlayOneShot(bookOpen);
            //Debug.Log("Hop on ferme le carnet");
        }
        */
        //////////// LES TRANSITIONS DE PAGES DE CARNET //////////
        ///

        // Transition depuis map ///
        if (canvaMap.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaMap.SetActive(false);
            canvaC1.SetActive(true);
            owl.PlayOneShot(pageTurn);           
        }

        // Transitions depuis Double Page 1 //////

        if (canvaC1.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC1.SetActive(false);
            canvaMap.SetActive(true);
            owl.PlayOneShot(pageTurn);
        }

        if (canvaC1.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC1.SetActive(false);
            canvaC2.SetActive(true);
            owl.PlayOneShot(pageTurn);        
        }

        // Transitions depuis Double Page 2 //////

        if (canvaC2.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC2.SetActive(false);
            canvaC1.SetActive(true);
            owl.PlayOneShot(pageTurn);
        }

        if (canvaC2.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC2.SetActive(false);
            canvaC3.SetActive(true);
            owl.PlayOneShot(pageTurn);
        }
        // Transitions depuis Double Page 3 //////

        if (canvaC3.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC3.SetActive(false);
            canvaC2.SetActive(true);
            owl.PlayOneShot(pageTurn);
        }

        if (canvaC3.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC3.SetActive(false);
            canvaC4.SetActive(true);
            owl.PlayOneShot(pageTurn);
            Debug.Log("Double page 4 ");
        }
        // Transitions depuis Double Page 4 //////

        if (canvaC4.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC4.SetActive(false);
            canvaC3.SetActive(true);
            owl.PlayOneShot(pageTurn);
        }

        // Transition quitte le carnet ////////

        if ((canvaMap.activeInHierarchy == true && Input.GetKeyDown(KeyCode.I) && keyIUp)
            || (canvaC1.activeInHierarchy == true && Input.GetKeyDown(KeyCode.I) && keyIUp)
            || (canvaC2.activeInHierarchy == true && Input.GetKeyDown(KeyCode.I) && keyIUp)
            || (canvaC3.activeInHierarchy == true && Input.GetKeyDown(KeyCode.I) && keyIUp)
            || (canvaC4.activeInHierarchy == true && Input.GetKeyDown(KeyCode.I) && keyIUp))
        {
            owl.PlayOneShot(closeBook);
            canvaMap.SetActive(false);
            canvaC1.SetActive(false);
            canvaC2.SetActive(false);
            canvaC3.SetActive(false);
            canvaC4.SetActive(false);
            carnetIsOpen = false;

            //Debug.Log("******* I : KEY DOWN - CLOSE INVENTORY ******* " + keyIUp);

            //Debug.Log("le carnet vient de se fermer");
        }

        // ajout d'informations Double page 1 ////

        if (s_RewardmanagerTechno.p1_1hbs)
        {
            dp1T1.SetActive(true);
            dp1I1.SetActive(true);
        }
        if (s_RewardmanagerTechno.p1_2hbs)
        {
            dp1T2.SetActive(true);
            dp1I2.SetActive(true);
        }
        if (s_RewardmanagerTechno.p1_3hbs)
        {
            dp1T3.SetActive(true);
            dp1I3.SetActive(true);
        }
        if (s_RewardmanagerTechno.p1_4hbs)
        {
            dp1T4.SetActive(true);
            dp1I4.SetActive(true);
        }
        if (s_RewardmanagerTechno.p1_5hbs)
        {
            dp1T5.SetActive(true);
            dp1I5.SetActive(true);
        }
        if (s_RewardmanagerTechno.p1_6hbs)
        {
            dp1T6.SetActive(true);
            dp1I6.SetActive(true);
        }
        if (s_RewardmanagerTechno.p1_1hbs && s_RewardmanagerTechno.p1_2hbs && s_RewardmanagerTechno.p1_3hbs && s_RewardmanagerTechno.p1_4hbs && s_RewardmanagerTechno.p1_5hbs && s_RewardmanagerTechno.p1_6hbs)
        {
            dp1TB.SetActive(true);
        }

        // ajout d'informations Double page 2 ////

        if (s_RewardmanagerTechno.p3_1hbs)
        {
            dp2T1.SetActive(true);
            dp2I1.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_2hbs)
        {
            dp2T2.SetActive(true);
            dp2I2.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_3hbs)
        {
            dp2T3.SetActive(true);
            dp2I3.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_4hbs)
        {
            dp2T4.SetActive(true);
            dp2I4.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_5hbs)
        {
            dp2T5.SetActive(true);
            dp2I5.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_6hbs)
        {
            dp2T6.SetActive(true);
            dp2I6.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_1hbs && s_RewardmanagerTechno.p3_2hbs && s_RewardmanagerTechno.p3_3hbs && s_RewardmanagerTechno.p3_4hbs && s_RewardmanagerTechno.p3_5hbs && s_RewardmanagerTechno.p3_6hbs)
        {
            dp2TB.SetActive(true);
        }
        // information nature
        // ajout d'informations Double page 3 ////

        if (s_RewardmanagerNature.p1_1hbs)
        {
            dp3T1.SetActive(true);
            dp3I1.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_2hbs)
        {
            dp3T2.SetActive(true);
            dp3I2.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_3hbs)
        {
            dp3T3.SetActive(true);
            dp3I3.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_4hbs)
        {
            dp3T4.SetActive(true);
            dp3I4.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_5hbs)
        {
            dp3T5.SetActive(true);
            dp3I5.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_6hbs)
        {
            dp3T6.SetActive(true);
            dp3I6.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_1hbs && s_RewardmanagerNature.p1_2hbs && s_RewardmanagerNature.p1_3hbs && s_RewardmanagerNature.p1_4hbs && s_RewardmanagerNature.p1_5hbs && s_RewardmanagerNature.p1_6hbs)
        {
            dp4TB.SetActive(true);
        }

        // ajout d'informations Double page 4 ////

        if (s_RewardmanagerNature.p3_1hbs)
        {
            dp4T1.SetActive(true);
            dp4I1.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_2hbs)
        {
            dp4T2.SetActive(true);
            dp4I2.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_3hbs)
        {
            dp4T3.SetActive(true);
            dp4I3.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_4hbs)
        {
            dp4T4.SetActive(true);
            dp4I4.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_5hbs)
        {
            dp4T5.SetActive(true);
            dp4I5.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_6hbs)
        {
            dp4T6.SetActive(true);
            dp4I6.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_1hbs && s_RewardmanagerNature.p3_2hbs && s_RewardmanagerNature.p3_3hbs && s_RewardmanagerNature.p3_4hbs && s_RewardmanagerNature.p3_5hbs && s_RewardmanagerNature.p3_6hbs)
        {
            dp4TB.SetActive(true);
        }
    }
}