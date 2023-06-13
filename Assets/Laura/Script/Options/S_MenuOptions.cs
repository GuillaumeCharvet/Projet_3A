using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MenuOptions : MonoBehaviour

    {
        [SerializeField] private GameObject OptionsHolder;
        [SerializeField] private GameObject MainMenuHolder;
        
         public void ActivateMenuOptions()
         {
            OptionsHolder.SetActive(true);
            MainMenuHolder.SetActive(false);
 
         }

        public void ActivateMainMenu()
        {
            OptionsHolder.SetActive(false);
            MainMenuHolder.SetActive(true);
  
        }

        public void ActivateProfilMenu()
        {
            OptionsHolder.SetActive(false);   
        }

}