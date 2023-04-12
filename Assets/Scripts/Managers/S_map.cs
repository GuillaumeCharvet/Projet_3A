using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_map : MonoBehaviour
    
{
    [SerializeField]


    // appel au scipt des dialogues pour ses booleans ////

    public S_rewardmanager s_RewardmanagerTechno;
    public S_rewardmanager s_RewardmanagerNature;

    // canvas fonds //////

    public GameObject canvaMap;
    public GameObject canvaC1; // CanvaC1: Canva Carnet 1 (première double page)
    public GameObject canvaC2;
    public GameObject canvaC3;
    //public GameObject canvaC4;
    //public GameObject canvaC5;
    //public GameObject canvaC6;

    // Textes et images des canvas //////////

    // double page 1 //////

    // textes //   nomenclature: dp1T1 = Double Page 1 Texte 1
    public GameObject dp1T1;
    public GameObject dp1T2;
    public GameObject dp1T3;
    public GameObject dp1T4;

    //images //  nomenclature: dp1I1 = Double Page 1 Image 1
    public GameObject dp1I1;
    public GameObject dp1I2;
    public GameObject dp1I3;
    public GameObject dp1I4;
    public GameObject dp1TB; // nomenclature: double page 1 texte bonus


    // double page 2 //////

    // textes //   nomenclature: dp2T1 = Double Page 2 Texte 1
    public GameObject dp2T1;
    public GameObject dp2T2;
    public GameObject dp2T3;
    public GameObject dp2T4;
    public GameObject dp2TB; // nomenclature: double page 2 texte bonus

    //images //  nomenclature: dp2I1 = Double Page 21 Image 1
    public GameObject dp2I1;
    public GameObject dp2I2;
    public GameObject dp2I3;
    public GameObject dp2I4;


    // double page 3 //////

    // textes //   nomenclature: dp2T1 = Double Page 2 Texte 1
    public GameObject dp3T1;
    public GameObject dp3T2;
    public GameObject dp3T3;
    public GameObject dp3T4;

    //images //  nomenclature: dp2I1 = Double Page 21 Image 1
    public GameObject dp3I1;
    public GameObject dp3I2;
    public GameObject dp3I3;
    public GameObject dp3I4;
    public GameObject dp3TB; // nomenclature: double page 3 texte bonus


    // double page 4 //////

    // double page 5 //////

    // double page 6 //////

    private bool carnetIsOpen = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.I) && !carnetIsOpen)
        {
            canvaMap.SetActive(true);
            carnetIsOpen = true;
            Debug.Log("Hop on ouvre le carnet");


            /*if (s_Rewardmanager.p1hbs)
            {

            }
            */

            /* if (Input.GetKeyDown(KeyCode.M))
             {
                 canvaMap.SetActive(false);
             }*/
        }
        //////////// LES TRANSITIONS DE PAGES DE CARNET //////////
        ///

        // Transition depuis map ///
        if (canvaMap.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaMap.SetActive(false);
            canvaC1.SetActive(true);
            Debug.Log("Double page 1 ");
        }

        // Transitions depuis Double Page 1 //////

        if (canvaC1.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC1.SetActive(false);
            canvaMap.SetActive(true);
        }

        if (canvaC1.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC1.SetActive(false);
            canvaC2.SetActive(true);
            Debug.Log("Double page 2 ");
        }

        // Transitions depuis Double Page 2 //////

        if (canvaC2.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC2.SetActive(false);
            canvaC1.SetActive(true);
        }
        
        if (canvaC2.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC2.SetActive(false);
            canvaC3.SetActive(true);
            Debug.Log("Double page 3 ");
        }
        // Transitions depuis Double Page 3 //////

        if (canvaC3.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC3.SetActive(false);
            canvaC2.SetActive(true);
        }
        // a Réactiver quadn les double pages natures seront là
        /*
        if (canvaC3.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC3.SetActive(false);
            canvaC4.SetActive(true);
            Debug.Log("Double page 4 ");
        }
        // Transitions depuis Double Page 4 //////

        if (canvaC4.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC4.SetActive(false);
            canvaC3.SetActive(true);
        }

        if (canvaC4.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC4.SetActive(false);
            canvaC5.SetActive(true);
            Debug.Log("Double page 5 ");
        }
        // Transitions depuis Double Page 5 //////

        if (canvaC5.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC5.SetActive(false);
            canvaC4.SetActive(true);
        }

        if (canvaC5.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC5.SetActive(false);
            canvaC6.SetActive(true);
            Debug.Log("Double page 6 ");
        }
        // Transitions depuis Double Page 6 //////

        if (canvaC6.activeInHierarchy == true && Input.GetKeyDown(KeyCode.A))
        {
            canvaC6.SetActive(false);
            canvaC5.SetActive(true);
        }
        */

        // Transition quitte le carnet ////////

        if (canvaMap.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape) 
            || canvaC1.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape)
            || canvaC2.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape)
            || canvaC3.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape)
            /*|| canvaC4.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape)
            || canvaC5.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape)
            || canvaC6.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape)*/)
        {
            
            canvaMap.SetActive(false);
            canvaC1.SetActive(false);
            canvaC2.SetActive(false);
            carnetIsOpen = false;
            Debug.Log("le carnet vient de se fermer");
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
        if (s_RewardmanagerTechno.p1_1hbs && s_RewardmanagerTechno.p1_2hbs && s_RewardmanagerTechno.p1_3hbs && s_RewardmanagerTechno.p1_4hbs)
        {
            dp1TB.SetActive(true);
        }

        // ajout d'informations Double page 2 ////

        if (s_RewardmanagerTechno.p2_1hbs) 
        {
            dp2T1.SetActive(true);
            dp2I1.SetActive(true);
        }
        if (s_RewardmanagerTechno.p2_2hbs)
        {
            dp2T2.SetActive(true);
            dp2I2.SetActive(true);
        }
        if (s_RewardmanagerTechno.p2_3hbs)
        {
            dp2T3.SetActive(true);
            dp2I3.SetActive(true);
        }
        if (s_RewardmanagerTechno.p2_4hbs)
        {
            dp2T4.SetActive(true);
            dp2I4.SetActive(true);
        }
        if (s_RewardmanagerTechno.p2_1hbs && s_RewardmanagerTechno.p2_2hbs && s_RewardmanagerTechno.p2_3hbs && s_RewardmanagerTechno.p2_4hbs)
        {
            dp2TB.SetActive(true);
        }

        // ajout d'informations Double page 3 ////

        if (s_RewardmanagerTechno.p3_1hbs)
        {
            dp3T1.SetActive(true);
            dp3I1.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_2hbs)
        {
            dp3T2.SetActive(true);
            dp3I2.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_3hbs)
        {
            dp3T3.SetActive(true);
            dp3I3.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_4hbs)
        {
            dp3T4.SetActive(true);
            dp3I4.SetActive(true);
        }
        if (s_RewardmanagerTechno.p3_1hbs && s_RewardmanagerTechno.p3_2hbs && s_RewardmanagerTechno.p3_3hbs && s_RewardmanagerTechno.p3_4hbs)
        {
            dp3TB.SetActive(true);
        }
        // information nature
        // ajout d'informations Double page 4 ////

        if (s_RewardmanagerNature.p1_1hbs)
        {
            dp1T1.SetActive(true);
            dp1I1.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_2hbs)
        {
            dp1T2.SetActive(true);
            dp1I2.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_3hbs)
        {
            dp1T3.SetActive(true);
            dp1I3.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_4hbs)
        {
            dp1T4.SetActive(true);
            dp1I4.SetActive(true);
        }
        if (s_RewardmanagerNature.p1_1hbs && s_RewardmanagerNature.p1_2hbs && s_RewardmanagerNature.p1_3hbs && s_RewardmanagerNature.p1_4hbs)
        {
            dp1TB.SetActive(true);
        }

        // ajout d'informations Double page 5 ////

        if (s_RewardmanagerNature.p2_1hbs)
        {
            dp2T1.SetActive(true);
            dp2I1.SetActive(true);
        }
        if (s_RewardmanagerNature.p2_2hbs)
        {
            dp2T2.SetActive(true);
            dp2I2.SetActive(true);
        }
        if (s_RewardmanagerNature.p2_3hbs)
        {
            dp2T3.SetActive(true);
            dp2I3.SetActive(true);
        }
        if (s_RewardmanagerNature.p2_4hbs)
        {
            dp2T4.SetActive(true);
            dp2I4.SetActive(true);
        }
        if (s_RewardmanagerNature.p2_1hbs && s_RewardmanagerNature.p2_2hbs && s_RewardmanagerNature.p2_3hbs && s_RewardmanagerNature.p2_4hbs)
        {
            dp2TB.SetActive(true);
        }

        // ajout d'informations Double page 6 ////

        if (s_RewardmanagerNature.p3_1hbs)
        {
            dp3T1.SetActive(true);
            dp3I1.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_2hbs)
        {
            dp3T2.SetActive(true);
            dp3I2.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_3hbs)
        {
            dp3T3.SetActive(true);
            dp3I3.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_4hbs)
        {
            dp3T4.SetActive(true);
            dp3I4.SetActive(true);
        }
        if (s_RewardmanagerNature.p3_1hbs && s_RewardmanagerNature.p3_2hbs && s_RewardmanagerNature.p3_3hbs && s_RewardmanagerNature.p3_4hbs)
        {
            dp3TB.SetActive(true);
        }
    }
    
}
