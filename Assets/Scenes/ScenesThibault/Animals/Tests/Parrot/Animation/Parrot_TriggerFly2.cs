using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot_TriggerFly2 : MonoBehaviour
{
    public bool BoolParrotFly;
    public GameObject Parot_TriggerZoneFly;
    public Animator animator;
    private float alphaValue = 1f;
    private float fadeSpeed = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolParrotFly", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.SetBool("BoolParrotFly", false);
    }

    private IEnumerator DelayFadeOut()
    {
        yield return new WaitForSeconds(2f);
        while (alphaValue > 0f)
        {
            alphaValue -= fadeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}