using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="states/walk", fileName ="new Wallk")]
public class StateWalk : StateBehaviourParent
{

    [Header("RUN/JUMP")]

    [SerializeField] private static float maxSpeed = 10f;
    [SerializeField] public float maxAcceleration = 10f;

    [SerializeField] public bool hasTransitionDown = false;
    [SerializeField] public string parameterNameDown = "";
    [SerializeField] public float speedDown = 0f;
    [SerializeField] public bool hasTransitionUp = false;
    [SerializeField] public string parameterNameUp = "";
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
        reader.smp.Move(maxSpeed, maxAcceleration);

        if (hasTransitionDown)
        {
            reader.smp.UpdateIdleTransitionsParameters(parameterNameDown, speedDown);
        }
        if (hasTransitionUp)
        {
            reader.smp.UpdateIdleTransitionsParameters(parameterNameUp, speedUp);
        }

        base.Update();
    }

    protected override void OnEnterState()
    {
        base.OnEnterState();
    }
}
