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
        /*
        Destroy(reader.gameObject.GetComponent<Rigidbody>());
        reader.GetComponent<CharacterController>().enabled = true;
        */
        reader.cc.Move(Vector3.zero);
        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Climb(maxclimbSpeed, maxClimbAcceleration);
        reader.smp.UpdateCanClimbTopToBot(false);
        reader.smp.UpdateCanClimbTopRay();
        reader.smp.UpdateCanClimbUp();

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.currentModeMovement = ModeMovement.Climb;

        /*
        reader.GetComponent<CharacterController>().enabled = false;
        */

        /*
        var rgbd = reader.gameObject.AddComponent<Rigidbody>();
        rgbd.isKinematic = true;
        rgbd.useGravity = false;
        */

        reader.smp.SetPlayerColliderToClimb();

        reader.smp.StartCoroutine("ChangeBoolValueFor2Seconds");

        base.OnEnterState();
    }
}
