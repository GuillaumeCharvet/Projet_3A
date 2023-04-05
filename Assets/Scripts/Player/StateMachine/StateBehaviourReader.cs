using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBehaviourReader : MonoBehaviour
{

    public MovementParameters mp;
    public StateMachineParameters smp;
    public CharacterController cc;
    public S_GliderBehaviour gliderBehaviour;

    private StateBehaviourParent currentState;

    public InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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