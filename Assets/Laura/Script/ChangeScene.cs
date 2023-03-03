using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    //Changement de scene possible par rapport aux int donner (possible de mettre plusieurs scene)
    //int = numéro // string = text
    public void ChangeToScene(string sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }
}
