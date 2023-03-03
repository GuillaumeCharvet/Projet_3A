using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour
    {
        [SerializeField] private GameObject OptionsBase;
        [SerializeField] private GameObject MenuBase;

        public void ActivateMenu()
        {
            OptionsBase.SetActive(true);
            MenuBase.SetActive(false);
        }

        public void DeactivateMenu()
        {
            OptionsBase.SetActive(false);
            MenuBase.SetActive(true);
        }
}