using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/walk", fileName = "new_Walk")]
public class StateWalk : StateBehaviourParent
{
    [Header("RUN/JUMP")]
    [SerializeField] public float maxAcceleration = 50f;

    [SerializeField] public float speedUp = 0f;
    [SerializeField] public float speedDown = 0f;

    private UpdateManager updateManager;
    /*
    [SerializeField] public float jumpVerticalBoost = 0.4f;
    [SerializeField] public float jumpHorizontalBoost = 1f;
    public float speed;
    */

    public void Awake()
    {
        updateManager = ManagerManager.Instance.GetComponent<UpdateManager>();
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void Update()
    {
        if (updateManager.updateActivated) reader.smp.Move(reader.smp.MaxSpeed, maxAcceleration, true);

        reader.smp.UpdateIdleTransitionsParameters("speedThresholdReached_1", speedDown);
        reader.smp.UpdateIdleTransitionsParameters("speedThresholdReached_2", speedUp);
        reader.smp.UpdateCanClimbTopToBot(true);

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.currentModeMovement = ModeMovement.Walk;

        reader.smp.ResetPlayerCollider();

        reader.transform.rotation = Quaternion.Euler(0f, reader.transform.rotation.eulerAngles.y, 0f);
        reader.smp.ResetStamina();
        base.OnEnterState();
    }
}