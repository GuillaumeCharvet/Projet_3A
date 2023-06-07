using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Folliage_Movement : MonoBehaviour
{

    public MeshRenderer meshrendererTree;
    public Material materialBase;
    public Material materialMovement;






    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshrendererTree.material = materialMovement;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshrendererTree.material = materialBase;
        }
    }