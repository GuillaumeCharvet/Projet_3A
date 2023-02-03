using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCollider2D;
    
    public void Update()
    {
        void OnMouseOver()
        {
            BoxCollider2D boxCollider2D = GetComponent <BoxCollider2D>();
            Debug.Log("Le mouse over fonctionne sale batard");
            animator.SetBool("IsOpenMenu", true);

        }

        void OnMouseExit()
        {
            animator.SetBool("IsOpenMenu", false);
        }

    }
}