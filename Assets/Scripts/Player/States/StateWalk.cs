using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="states/walk", fileName ="new Wallk")]
public class StateWalk : StateBehaviourParent
{

    [Header("RUN/JUMP")]
    [SerializeField] public float maxIdleSpeed = 10f;
    [SerializeField] public float maxIdleAcceleration = 10f;
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
        reader.smp.Move(maxIdleAcceleration);

        reader.smp.UpdateIdleTransitionsParameters(maxIdleSpeed);

        base.Update();
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
    }
}
