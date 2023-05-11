using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/roll", fileName = "new_Roll")]
public class StateRoll : StateBehaviourParent
{
    [Header("RUN/JUMP")]

    [SerializeField] public float maxAcceleration = 50f;

    [SerializeField] public float rollSpeedIdle = 6f;

    [SerializeField] public float rollSpeed = 12f;

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Roll(rollSpeed, rollSpeedIdle, maxAcceleration, true);

        reader.smp.UpdateCanClimbTopToBot(true);

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.currentModeMovement = ModeMovement.Run;

        reader.smp.ResetPlayerCollider();

        reader.smp.ResetStamina();
        base.OnEnterState();
    }
}