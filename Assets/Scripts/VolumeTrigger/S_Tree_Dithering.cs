using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tree_Dithering : MonoBehaviour
{

    public MeshRenderer meshrendererTree;
    public Material materialBase;
    public Material materialDithered;





    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
         {
            meshrendererTree.material = materialDithered;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshrendererTree.material = materialBase;
        }
    }
}
