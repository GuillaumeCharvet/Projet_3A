using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_HarponBehaviour : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody rgbdSpear;
    public GameObject spearHookPrefab;

    private float scaleFactor = 1;
    //public RopeManager ropeManager;
    //public PlayerActions playerActions;

    private void Start()
    {
        rgbdSpear = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        velocity += -10f * Time.deltaTime * Vector3.up;
        velocity *= 0.99f;

        transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.Cross(velocity, Vector3.forward), Vector3.up));

        scaleFactor = Mathf.Min(scaleFactor + 0.1f * Time.deltaTime, 1.5f);
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