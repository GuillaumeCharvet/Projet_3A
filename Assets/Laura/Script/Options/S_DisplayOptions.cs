using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DisplayOptions : MonoBehaviour
{
    [SerializeField] private GameObject OptionsDisplayBase;
    [SerializeField] private GameObject OptionsSoundBase;
    [SerializeField] private GameObject OptionsControlsBase;

    public void ActivateMenu()
    {
        OptionsDisplayBase.SetActive(true);
        OptionsSoundBase.SetActive(false);
        OptionsControlsBase.SetActive(false);
    }

    public void DeactivateMenu()
    {
        OptionsSoundBase.SetActive(false);
        OptionsControlsBase.SetActive(false);
        OptionsDisplayBase.SetActive(true);

    }
}