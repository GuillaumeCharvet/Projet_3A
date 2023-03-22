using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/hang", fileName = "new Hang")]
public class StateHang : StateBehaviourParent
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
        reader.smp.ClimbHanging(maxclimbSpeed, maxClimbAcceleration);
        reader.smp.UpdateCanClimbTopToBot();
        reader.smp.UpdateStopHanging();

        reader.smp.currentModeMovement = ModeMovement.Hang;

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.ResetPlayerCollider();

        base.OnEnterState();
    }
}
