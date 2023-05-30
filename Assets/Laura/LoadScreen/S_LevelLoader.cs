using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class S_LevelLoader : MonoBehaviour

{
    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text progressText;

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
            
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        // Async = Changer la scène in the background
       
        loadingScreen.SetActive(true);
        
        while (!operation.isDone) 
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            //Debug.Log(progress);
            //Debug.Log(operation.progress);
            yield return null;
        }
    }
}
