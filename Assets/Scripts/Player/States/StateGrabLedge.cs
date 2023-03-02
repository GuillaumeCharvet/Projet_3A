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
        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Climb(maxclimbSpeed, maxClimbAcceleration);
        reader.smp.UpdateCanClimbTopRay();
        reader.smp.UpdateCanClimbTopToBot();
        reader.smp.UpdateCanClimbBotRay();

        reader.smp.currentModeMovement = ModeMovement.GrabLedge;

        base.Update();
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
    }
}
