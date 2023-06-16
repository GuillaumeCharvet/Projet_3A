using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variations_Anim_PNJ_Deer : MonoBehaviour
{
    private Animator anim;
 
    IEnumerator Start()
    {
        anim = GetComponent<Animator>();

        while (true)
        {
            yield return new WaitForSeconds(8);
            anim.SetInteger("VarIndex2", Random.Range(0, 4));
            anim.SetTrigger("Var2");
        }
    }
}
