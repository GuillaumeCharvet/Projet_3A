using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/roll", fileName = "new_Roll")]
public class StateRoll : StateBehaviourParent
{
    [Header("RUN/JUMP")]

    [SerializeField] public float maxAcceleration = 50f;

    [SerializeField] public float speedDown = 0f;

    /*
    [SerializeField] public float jumpVerticalBoost = 0.4f;
    [SerializeField] public float jumpHorizontalBoost = 1f;
    public float speed;
    */

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Roll(reader.smp.MaxSpeed, maxAcceleration, true);

        reader.smp.UpdateIdleTransitionsParameters("speedThresholdReached_2", speedDown);
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