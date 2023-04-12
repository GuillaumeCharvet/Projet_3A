using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TriggerNoWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PAS DE PLOUF ICI");

            other.GetComponent<StateMachineParameters>().isInNoWaterZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") other.GetComponent<StateMachineParameters>().isInNoWaterZone = false;
    }
}
