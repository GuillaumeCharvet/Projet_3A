using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TreeGlobal : MonoBehaviour
{
    public S_Tree_Dithering smallBox, bigBox;

    public void ChangeMaterial()
    {
        if (smallBox.closeEnough && bigBox.closeEnough)
        {
            smallBox.meshrendererTree.material = smallBox.materialDitheredAndMovement;
            return;
        }

        if (bigBox.closeEnough)
        {
            smallBox.meshrendererTree.material = smallBox.materialDithered;
            return;
        }

        smallBox.meshrendererTree.material = smallBox.materialBase;
        return;
    }
}