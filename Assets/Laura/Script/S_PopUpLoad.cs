using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_PopUpLoad : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
        }
    }
}
