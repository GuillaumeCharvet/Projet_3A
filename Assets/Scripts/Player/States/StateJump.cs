using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/jump", fileName = "new_Jump")]
public class StateJump : StateBehaviourParent
{

    [Header("RUN/JUMP")]

    [SerializeField] public float maxAcceleration = 50f;

    [SerializeField] public float fallControl = 0f;

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
        reader.smp.Move(reader.smp.MaxSpeed, maxAcceleration, false);

        reader.smp.UpdateCanClimbTopToBot();
        reader.smp.UpdateStartGlide();

        reader.smp.currentModeMovement = ModeMovement.Jump;

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.ResetPlayerCollider();

        base.OnEnterState();
    }
}
