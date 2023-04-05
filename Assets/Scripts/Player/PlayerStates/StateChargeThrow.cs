using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/chargeThrow", fileName = "new_ChargeThrow")]
public class StateChargeThrow : StateBehaviourParent
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
        reader.smp.ChargeThrow();

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.currentModeMovement = ModeMovement.Idle;

        reader.smp.ResetPlayerCollider();

        reader.transform.rotation = Quaternion.Euler(0f, reader.transform.rotation.eulerAngles.y, 0f);
        reader.smp.ResetStamina();
        base.OnEnterState();
    }
}
