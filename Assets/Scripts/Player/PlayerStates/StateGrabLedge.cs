using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/grab ledge", fileName = "new Grab Ledge")]
public class StateGrabLedge : StateBehaviourParent
{

    [Header("RUN/JUMP")]

    [SerializeField] public float maxclimbSpeed = 10f;
    [SerializeField] public float maxClimbAcceleration = 10f;

    public override void OnExitState()
    {
        reader.cc.Move(Vector3.zero);

        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Climb(maxclimbSpeed, maxClimbAcceleration);
        reader.smp.UpdateCanClimbTopRay();
        reader.smp.UpdateCanClimbTopToBot(false);
        reader.smp.UpdateCanClimbBotRay();

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.currentModeMovement = ModeMovement.GrabLedge;

        reader.smp.SetPlayerColliderToClimb();

        base.OnEnterState();
    }
}