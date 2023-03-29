using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
        SceneManager.LoadScene("Scene_William2", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scene_Pierre_Deco", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scene_Shader2", LoadSceneMode.Additive);
        SceneManager.LoadScene("ThibaultDecoverticalSlice", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scene_Ocean", LoadSceneMode.Additive);
        
    }
}
