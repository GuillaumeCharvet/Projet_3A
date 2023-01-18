using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StateBehaviourParent  : ScriptableObject 
{
    protected StateBehaviourReader reader;

    public void EnterState(StateBehaviourReader reader)
    {
        this.reader = reader;
        OnEnterState();
    }

    protected virtual void OnEnterState()
    {
        
    }
    public virtual void Update()
    {
        //Do stuff here

    }
    public virtual void OnExitState()
    {

    }

}
