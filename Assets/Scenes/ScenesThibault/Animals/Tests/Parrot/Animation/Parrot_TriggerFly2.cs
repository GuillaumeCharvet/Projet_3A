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

    private SkinnedMeshRenderer _renderer;

    private void Start()
    {
        animator.Play("Base Layer.A_Parrot_Idle", 0, Random.Range(0f, 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("BoolParrotFly", true);
            _renderer = transform.parent.GetComponentInChildren<SkinnedMeshRenderer>();
            //StartCoroutine(DelayFadeOut());
        }
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
            Color objectColor = _renderer.materials[0].color;
            _renderer.materials[0].color = new Color(objectColor.r, objectColor.g, objectColor.b, alphaValue);

            yield return new WaitForEndOfFrame();
        }
    }
}