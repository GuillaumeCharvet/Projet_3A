using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBehaviourReader : MonoBehaviour
{
    public MovementParameters mp;
    public StateMachineParameters smp;
    public CharacterController cc;
    public S_Main_Menu menuPause;

    private StateBehaviourParent currentState;

    public InputManager inputManager;

    public List<GameObject> gliderParts;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!menuPause.pause)
        {
            currentState?.Update();
        }
    }

    public void ChangeState(StateBehaviourParent newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.EnterState(this);
    }
}