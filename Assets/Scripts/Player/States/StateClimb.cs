using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/climb", fileName = "new Climb")]
public class StateClimb : StateBehaviourParent
{

    [Header("RUN/JUMP")]

    [SerializeField] public float maxclimbSpeed = 10f;
    [SerializeField] public float maxClimbAcceleration = 10f;

    public override void OnExitState()
    {

        reader.cc.height = reader.smp.characterControlerHeightResetValue;
        reader.cc.center = 0.85f * Vector3.up;

        /*
        reader.GetComponent<CharacterController>().enabled = true;
        reader.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(reader.gameObject.GetComponent<Rigidbody>());
        */
        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Climb(maxclimbSpeed, maxClimbAcceleration);
        reader.smp.UpdateCanClimbTopRay();
        reader.smp.UpdateCanClimbUp();

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.currentModeMovement = ModeMovement.Climb;

        /*
        reader.GetComponent<CharacterController>().enabled = false;
        reader.GetComponent<CapsuleCollider>().enabled = true;
        var rgbd = reader.gameObject.AddComponent<Rigidbody>();
        rgbd.isKinematic = true;
        */

        reader.smp.StartCoroutine("ChangeBoolValueFor2Seconds");

        reader.cc.height = 0;
        reader.cc.center = 0.5f * Vector3.up;

        base.OnEnterState();
    }
}
