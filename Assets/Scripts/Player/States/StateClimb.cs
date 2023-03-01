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
        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Climb(maxclimbSpeed, maxClimbAcceleration);
        reader.smp.UpdateCanClimbTopRay();
        reader.smp.UpdateCanClimbUp();

        reader.smp.currentModeMovement = ModeMovement.Climb;

        base.Update();
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
    }
}
