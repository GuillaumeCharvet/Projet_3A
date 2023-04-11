using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_map : MonoBehaviour
    
{
    [SerializeField]


    // appel au scipt des dialogues pour ses booleans ////

    public S_rewardmanager s_Rewardmanager;

    // canvas fonds //////

    public GameObject canvaMap;
    public GameObject canvaC1; // CanvaC1: Canva Carnet 1 (première double page)
    public GameObject canvaC2;
    public GameObject canvaC3;
    public GameObject canvaC4;
    public GameObject canvaC5;
    public GameObject canvaC6;

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


    // double page 2 //////

    // textes //   nomenclature: dp2T1 = Double Page 2 Texte 1
    public GameObject dp2T1;
    public GameObject dp2T2;
    public GameObject dp2T3;
    public GameObject dp2T4;

    //images //  nomenclature: dp2I1 = Double Page 21 Image 1
    public GameObject dp2I1;
    public GameObject dp2I2;
    public GameObject dp2I3;
    public GameObject dp2I4;

    // double page 3 //////

    // double page 4 //////

    // double page 5 //////

    // double page 6 //////



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.I))
        {
            canvaMap.SetActive(true);
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
        /*
        if (canvaC2.activeInHierarchy == true && Input.GetKeyDown(KeyCode.D))
        {
            canvaC2.SetActive(false);
            canvaC3.SetActive(true);
            Debug.Log("Double page 3 ");
        }
        */
        // Transition quitte le carnet ////////

        if (canvaMap.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape) 
            || canvaC1.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape)
            || canvaC2.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
        {
            canvaMap.SetActive(false);
            canvaC1.SetActive(false);
            canvaC2.SetActive(false);
            Debug.Log("le carnet vient de se fermer");
        }

    }
    
}
