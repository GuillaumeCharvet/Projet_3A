using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModeMovement
{
    public string name;
    protected ModeMovementManager stateMachine;

    public BaseModeMovement(string name, ModeMovementManager stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
