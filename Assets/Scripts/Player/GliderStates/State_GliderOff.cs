using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/glider/off", fileName = "new_GliderOff")]
public class State_GliderOff : StateBehaviourParent
{

    public override void OnExitState()
    {
        reader.smp.gliderTransform.gameObject.SetActive(false);

        var anim = reader.GetComponent<Animator>();

        reader.transform.rotation = Quaternion.Euler(0f, reader.transform.rotation.eulerAngles.y, 0f);

        base.OnExitState();
    }

    public override void Update()
    {

        reader.smp.UpdateCanClimbTopToBot(true);

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.currentModeMovement = ModeMovement.Glide;

        reader.smp.gliderTransform.gameObject.SetActive(true);

        reader.smp.SetPlayerColliderToClimb();

        reader.smp.gliderSpeed = 1f;
        base.OnEnterState();
    }
}