using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_HarponBehaviour : MonoBehaviour
{
    public Vector3 velocity;
    private Rigidbody rgbdSpear;
    public GameObject spearHookPrefab;

    private bool firstCollision = false;

    private float scaleFactor = 1;
    //public RopeManager ropeManager;
    //public PlayerActions playerActions;

    private void Start()
    {
        rgbdSpear = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocity += -10f * Time.deltaTime * Vector3.up;
        velocity *= 0.995f;

        //transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.Cross(velocity, Vector3.forward), Vector3.up));

        if (!firstCollision) transform.rotation = Quaternion.LookRotation(rgbdSpear.velocity) * Quaternion.Euler(-90f, 0f, 0f);

        scaleFactor = Mathf.Min(scaleFactor + 1f * Time.deltaTime, 3.5f);
        transform.localScale = scaleFactor * Vector3.one;

        //rgbdSpear.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        firstCollision = true;
        Debug.Log(Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, transform.TransformDirection(Vector3.up))));
        if (Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, transform.TransformDirection(Vector3.up))) > 0.7f)
        {
            Debug.Log("PLANTé !");
            rgbdSpear.isKinematic = true;
            rgbdSpear.useGravity = false;

            rgbdSpear.velocity = Vector3.zero;
            rgbdSpear.angularVelocity = Vector3.zero;

            //var spearHook = Instantiate(spearHookPrefab, collision.contacts[0].point, Quaternion.identity);

            /*
            ropeManager.SetCurrentFirstHook(spearHook);
            playerActions.currentTool = Tools.SpearHook;
            */
        }
    }
}