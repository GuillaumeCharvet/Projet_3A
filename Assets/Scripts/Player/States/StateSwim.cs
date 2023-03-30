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
        reader.smp.Swim(reader.smp.MaxSpeed, maxAcceleration, true);

        reader.smp.UpdateIdleTransitionsParameters("speedThresholdReached_2", speedDown);
        reader.smp.UpdateCanClimbTopToBot(true);

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.currentModeMovement = ModeMovement.Swim;

        reader.smp.ResetPlayerCollider();

        reader.smp.ResetStamina();
        base.OnEnterState();
    }
}