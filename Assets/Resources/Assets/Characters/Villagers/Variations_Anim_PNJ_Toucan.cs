using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variations_Anim_PNJ_Toucan : MonoBehaviour
{
    private Animator anim;
 
    IEnumerator Start()
    {
        anim = GetComponent<Animator>();

        while (true)
        {
            yield return new WaitForSeconds(9);
            anim.SetInteger("VarIndex", Random.Range(0, 4));
            anim.SetTrigger("Var");
        }
    }
}
