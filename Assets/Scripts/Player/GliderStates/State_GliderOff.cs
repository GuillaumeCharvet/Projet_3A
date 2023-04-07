using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/glider/off", fileName = "new_GliderOff")]
public class State_GliderOff : StateBehaviourParent
{
    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnEnterState()
    {
        foreach (var item in reader.gliderParts)
        {
            item.SetActive(false);
        }

        base.OnEnterState();
    }
}