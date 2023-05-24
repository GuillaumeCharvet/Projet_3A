using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_QuitPopUpOverwrite : MonoBehaviour

{
    [SerializeField] private GameObject PopUpHolder;
    [SerializeField] private GameObject Save_Profil123;

    public void ActivatePopUp()
    {
        PopUpHolder.SetActive(true);
        Save_Profil123.SetActive(false);


    }

    public void DeactivatePopUp0()
    {
        PopUpHolder.SetActive(false);
        Save_Profil123.SetActive(true);

    }
}
