using UnityEngine.Audio;
using UnityEngine;


public class S_Volume_Trigger_Sound : MonoBehaviour
{
    public AudioClip sound1;
    public AudioSource source1;


    // Update is called once per frame
    void Update()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        source1.PlayOneShot(sound1);
    }
}
