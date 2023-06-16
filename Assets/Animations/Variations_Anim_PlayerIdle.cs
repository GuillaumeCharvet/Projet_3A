using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variations_Anim_PlayerIdle : MonoBehaviour
{
    private Animator anim;
 
    IEnumerator Start()
    {
        anim = GetComponent<Animator>();

        while (true)
        {
            yield return new WaitForSeconds(5);
            anim.SetInteger("VarIdleTrigIndex", Random.Range(0, 3));
            anim.SetTrigger("VarIdleTrig");
        }
    }
}
