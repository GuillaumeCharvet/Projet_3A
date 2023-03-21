using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/swim", fileName = "new Swim")]
public class StateSwim : StateBehaviourParent
{

    [Header("RUN/JUMP")]

    [SerializeField] public float maxAcceleration = 50f;

    [SerializeField] public float speedDown = 0f;

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Move(reader.smp.MaxSpeed, maxAcceleration, true);

        reader.smp.UpdateIdleTransitionsParameters("speedThresholdReached_2", speedDown);
        reader.smp.UpdateCanClimbTopToBot();

        reader.smp.currentModeMovement = ModeMovement.Run;

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.ResetPlayerCollider();

        reader.smp.ResetStamina();
        base.OnEnterState();
    }
}