using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TriggerDrop : MonoBehaviour
{

    public GameObject EnvStele_1;
    public GameObject EnvStele_2;
    public GameObject EnvStele_3;
    public GameObject EnvStele_4;
    public GameObject EnvStele_5;
    public GameObject EnvStele_6;

    public GameObject finishStele_1;
    public GameObject finishStele_2;
    public GameObject finishStele_3;
    public GameObject finishStele_4;
    public GameObject finishStele_5;
    public GameObject finishStele_6;

    public GameObject canvaDrop;

    private bool stele1 = false;
    private bool stele2 = false;
    private bool stele3 = false;
    private bool stele4 = false;
    private bool stele5 = false;
    private bool stele6 = false;

    private bool allHBS = false;
    private bool isIn = false;
    public bool stoneFinished = false;


    


    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && stele1 && !allHBS)
            || (other.CompareTag("Player") && stele2 && !allHBS)
            || (other.CompareTag("Player") && stele3 && !allHBS)
            || (other.CompareTag("Player") && stele4 && !allHBS)
            || (other.CompareTag("Player") && stele5 && !allHBS)
            || (other.CompareTag("Player") && stele6) && !allHBS)
        {
           
            canvaDrop.SetActive(true);
            isIn = true;
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isIn)
        {
            canvaDrop.SetActive(false);
            isIn = false;
        }
    }
   
    
    void Update()
    {
        if (!EnvStele_1.activeInHierarchy)
        {
            stele1 = true;
        }
        if (!EnvStele_2.activeInHierarchy)
        {
            stele2 = true;
        }
        if (!EnvStele_3.activeInHierarchy)
        {
            stele3 = true;
        }
        if (!EnvStele_4.activeInHierarchy)
        {
            stele4 = true;
        }
        if (!EnvStele_5.activeInHierarchy)
        {
            stele5 = true;
        }
        if (!EnvStele_6.activeInHierarchy)
        {
            stele6 = true;
        }

        if (isIn && Input.GetKeyDown(KeyCode.F))
        {
            canvaDrop.SetActive(false);

            if (stele1)
            {
                finishStele_1.SetActive(true);
            }
            if (stele2)
            {
                finishStele_2.SetActive(true);
            }
            if (stele3)
            {
                finishStele_3.SetActive(true);
            }
            if (stele4)
            {
                finishStele_4.SetActive(true);
            }
            if (stele5)
            {
                finishStele_5.SetActive(true);
            }
            if (stele6)
            {
                finishStele_6.SetActive(true);
            }
        }
        if (finishStele_1.activeInHierarchy &&
            finishStele_2.activeInHierarchy &&
            finishStele_3.activeInHierarchy &&
            finishStele_4.activeInHierarchy &&
            finishStele_5.activeInHierarchy &&
            finishStele_6.activeInHierarchy)
        {
            stoneFinished = true;
            allHBS = true;
        }
    }   
}
