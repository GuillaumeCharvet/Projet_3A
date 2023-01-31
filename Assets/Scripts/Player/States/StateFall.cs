using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/fall", fileName = "new_Fall")]
public class StateFall : StateBehaviourParent
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
        reader.smp.Move(reader.smp.MaxSpeed * fallControl, maxAcceleration);

        reader.smp.UpdateCanClimbTopToBot();
        reader.smp.UpdateStartGlide();

        base.Update();
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
    }
}
