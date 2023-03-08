using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyEffect : MonoBehaviour
{
    private BoxCollider colliderBox;
    private float waterHeight;
    private float waterDensity = 1.35f / 3.5f;
    private Collider playerNextFixedUpdate;
    private StateMachineParameters moveOther;
    private CharacterController capsule;

    private void Start()
    {
        colliderBox = GetComponent<BoxCollider>();
        waterHeight = transform.position.y + transform.localScale.y / 2f + colliderBox.center.y + 0.5f * colliderBox.size.y;
        Debug.Log("waterHeight" + waterHeight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PLOUF");
            playerNextFixedUpdate = other;

            moveOther = other.GetComponent<StateMachineParameters>();
            moveOther.isInWaterNextFixedUpdate = true;

            capsule = playerNextFixedUpdate.gameObject.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("GROS PLOUF");
            var playerTop = capsule.transform.position.y + capsule.center.y + 0.5f * capsule.height + 0.7f;
            var playerBot = capsule.transform.position.y + capsule.center.y - 0.5f * capsule.height + 0.7f;

            Debug.Log("playerTop" + playerTop);
            Debug.Log("playerBot" + playerBot);
            if (playerTop <= waterHeight)
            {
                //ApplyBuoyancyForce(cc, waterDensity * capsule.height);
                moveOther.forceOfWater = waterDensity * capsule.height;
            }
            else
            {
                Debug.Log("PAS FULL IMMERGE");
                //ApplyBuoyancyForce(cc, waterDensity * (waterHeight - playerBot));
                moveOther.forceOfWater = waterDensity * (waterHeight - playerBot);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<StateMachineParameters>().isInWaterNextFixedUpdate = false;
        }
    }

    private void ApplyBuoyancyForce(CharacterController cc, float power)
    {
        cc.Move(power * Vector3.up);
    }
}
