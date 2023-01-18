using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBehaviourReader : MonoBehaviour
{

    public MovementParameters mp;
    private StateBehaviourParent currentState;

    public InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Update();   
    }

    public void ChangeState(StateBehaviourParent newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.EnterState(this);
    }

}
