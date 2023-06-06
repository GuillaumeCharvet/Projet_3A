using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateManager : Manager
{
    private UnityAction updateActions;
    private UnityAction fixedUpdateActions;

    public bool updateActivated, fixedUpdateActivated = true;

    void Start()
    {
        StartCoroutine(CoroutineUpdate());
        StartCoroutine(CoroutineFixedUpdate());
    }

    public void AddActionToUpdate(UnityAction action)
    {
        updateActions += action;
    }
    public void RemoveActionToUpdate(UnityAction action)
    {
        updateActions -= action;
    }
    public void AddActionToFixedUpdate(UnityAction action)
    {
        fixedUpdateActions += action;
    }
    public void RemoveActionToFixedUpdate(UnityAction action)
    {
        fixedUpdateActions += action;
    }

    private IEnumerator CoroutineUpdate()
    {
        while (updateActivated)
        {
            yield return new WaitWhile(() => updateActions == null);
            updateActions.Invoke();
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CoroutineFixedUpdate()
    {
        while (fixedUpdateActivated)
        {
            yield return new WaitWhile(() => fixedUpdateActions == null);
            fixedUpdateActions.Invoke();
            yield return new WaitForFixedUpdate();
        }
    }
}
