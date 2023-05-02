using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_HarponBehaviour : MonoBehaviour
{
    private Rigidbody rgbdSpear;
    public GameObject spearHookPrefab;

    //public RopeManager ropeManager;
    //public PlayerActions playerActions;

    private void Start()
    {
        rgbdSpear = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, transform.TransformDirection(Vector3.up))));
        if (Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, transform.TransformDirection(Vector3.up))) > 0.7f)
        {
            Debug.Log("PLANTé !");
            rgbdSpear.isKinematic = true;
            rgbdSpear.useGravity = false;
            /*
            rgbdSpear.velocity = Vector3.zero;
            rgbdSpear.angularVelocity = Vector3.zero;
            */
            var spearHook = Instantiate(spearHookPrefab, collision.contacts[0].point, Quaternion.identity);

            /*
            ropeManager.SetCurrentFirstHook(spearHook);
            playerActions.currentTool = Tools.SpearHook;
            */
        }
    }
}