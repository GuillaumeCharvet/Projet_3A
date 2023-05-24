using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "states/glider/deploy", fileName = "new_GliderDeploy")]
public class State_GliderDeploy : StateBehaviourParent
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
            item.SetActive(true);
        }        

        base.OnEnterState();
    }
}
