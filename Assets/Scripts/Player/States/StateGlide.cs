using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/glide", fileName = "new_Glide")]
public class StateGlide : StateBehaviourParent
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
        var anim = reader.GetComponent<Animator>();
        Debug.Log("EXIT GLIDE STATE :" + " PlayerJumped = " + anim.GetBool("PlayerJumped")
                                        + " IsInWater = " + anim.GetBool("IsInWater")
                                        + " IsGrounded = " + anim.GetBool("IsGrounded")
                                        + " HasGroundBelow = " + anim.GetBool("HasGroundBelow")
                                        + " CanClimbTopToBot = " + anim.GetBool("CanClimbTopToBot"));

        reader.transform.rotation = Quaternion.Euler(0f, reader.transform.rotation.eulerAngles.y, 0f);

        base.OnExitState();
    }

    public override void Update()
    {
        reader.smp.Glide(reader.smp.MaxSpeed, maxAcceleration);

        reader.smp.UpdateCanClimbTopToBot();

        reader.smp.currentModeMovement = ModeMovement.Glide;

        base.Update();
    }

    protected override void OnEnterState()
    {
        reader.smp.gliderSpeed = 1f;
        base.OnEnterState();
    }
}
