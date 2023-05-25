using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ProfilLoad : MonoBehaviour

{
    [SerializeField] private GameObject ProfilHolder;
    [SerializeField] private GameObject LoadHolder_Profil1;
    [SerializeField] private GameObject LoadHolder_Profil2;
    [SerializeField] private GameObject LoadHolder_Profil3;

    public void ActivateProfilMenu()
    {        
        ProfilHolder.SetActive(true);
        LoadHolder_Profil1.SetActive(false);
        LoadHolder_Profil2.SetActive(false);
        LoadHolder_Profil3.SetActive(false);

    }

    public void ActivateProfil1()
    {
        ProfilHolder.SetActive(false);
        LoadHolder_Profil1.SetActive(true);
        LoadHolder_Profil2.SetActive(false);
        LoadHolder_Profil3.SetActive(false);

    }

    public void ActivateProfil2()
    {
        ProfilHolder.SetActive(false);
        LoadHolder_Profil1.SetActive(false);
        LoadHolder_Profil2.SetActive(true);
        LoadHolder_Profil3.SetActive(false);

    }
    public void ActivateProfil3()
    {
        ProfilHolder.SetActive(false);
        LoadHolder_Profil1.SetActive(false);
        LoadHolder_Profil2.SetActive(false);
        LoadHolder_Profil3.SetActive(true);

    }
}