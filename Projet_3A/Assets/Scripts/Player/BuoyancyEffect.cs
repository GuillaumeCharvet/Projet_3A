using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyEffect : MonoBehaviour
{
    private BoxCollider colliderBox;
    private float waterHeight;
    private float waterDensity = 1.35f / 3.5f;
    private Collider playerNextFixedUpdate;

    private void Start()
    {
        colliderBox = GetComponent<BoxCollider>();
        waterHeight = colliderBox.center.y + 0.5f * colliderBox.size.y;
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerNextFixedUpdate = other;
            waterHeight = colliderBox.center.y + 0.5f * colliderBox.size.y;
            var moveOther = other.GetComponent<PlayerMovementCC>();
            moveOther.isInWaterNextFixedUpdate = true;

            var capsule = playerNextFixedUpdate.GetComponent<CapsuleCollider>();
            var playerTop = capsule.transform.position.y + capsule.center.y + 0.5f * capsule.height;
            var playerBot = capsule.transform.position.y + capsule.center.y - 0.5f * capsule.height;
            if (playerTop <= waterHeight)
            {
                //Debug.Log("GROS PLOUF");
                //ApplyBuoyancyForce(cc, waterDensity * capsule.height);
                moveOther.forceOfWater = waterDensity * capsule.height;
            }
            else
            {
                //Debug.Log("PLOUF");
                //ApplyBuoyancyForce(cc, waterDensity * (waterHeight - playerBot));
                moveOther.forceOfWater = waterDensity * (waterHeight - playerBot);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovementCC>().isInWaterNextFixedUpdate = false;
        }
    }

    private void ApplyBuoyancyForce(CharacterController cc, float power)
    {
        cc.Move(power * Vector3.up);
    }*/
}
