using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MenuOptions : MonoBehaviour

    {
        [SerializeField] private GameObject OptionsHolder;
        [SerializeField] private GameObject MenuHolder;
        [SerializeField] private GameObject ProfilHolder;
         public void ActivateMenuOptions()
         {
            OptionsHolder.SetActive(true);
            MenuHolder.SetActive(false);
            ProfilHolder.SetActive(false);
         }

        public void ActivateMenu()
        {
            OptionsHolder.SetActive(false);
            MenuHolder.SetActive(true);
            ProfilHolder.SetActive(false);
        }

        public void ActivateProfilMenu()
        {
            OptionsHolder.SetActive(false);
            MenuHolder.SetActive(false);
            ProfilHolder.SetActive(true);
        }
}