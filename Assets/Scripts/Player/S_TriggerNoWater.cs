using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TriggerNoWater : MonoBehaviour
{
    private StateMachineParameters stateMachineParameters;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PAS DE PLOUF ICI");

            stateMachineParameters = other.GetComponent<StateMachineParameters>();
            stateMachineParameters.isInNoWaterZone = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        stateMachineParameters.isInNoWaterZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") other.GetComponent<StateMachineParameters>().isInNoWaterZone = false;
    }
}