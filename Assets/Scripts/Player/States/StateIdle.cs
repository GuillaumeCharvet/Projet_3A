using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/idle", fileName = "new_Idle")]
public class StateIdle : StateBehaviourParent
{

    [Header("RUN/JUMP")]

    [SerializeField] public float maxAcceleration = 50f;

    [SerializeField] public float speedUp = 0f;

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
        reader.smp.Move(reader.smp.MaxSpeed, maxAcceleration);
        
        reader.smp.UpdateIdleTransitionsParameters("speedThresholdReached_1", speedUp);

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.ResetStamina();
        base.OnEnterState();
    }
}
