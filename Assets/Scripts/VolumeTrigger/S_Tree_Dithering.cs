using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tree_Dithering : MonoBehaviour
{
    public MeshRenderer meshrendererTree;
    public Material materialBase;
    public Material materialDithered;
    public Material materialDitheredAndMovement;
    public S_TreeGlobal streeGlobal;

    public bool closeEnough;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //meshrendererTree.material = materialDithered;
            closeEnough = true;
            streeGlobal.ChangeMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //meshrendererTree.material = materialBase;
            closeEnough = false;
            streeGlobal.ChangeMaterial();
        }
    }
}