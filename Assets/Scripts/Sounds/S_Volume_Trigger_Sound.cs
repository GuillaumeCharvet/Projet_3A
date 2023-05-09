using UnityEngine.Audio;
using UnityEngine;


public class S_Volume_Trigger_Sound : MonoBehaviour
{
    public AudioClip sound1;
    public AudioSource source1;
    private bool soundHasBeenSaid = false;


    // Update is called once per frame
    void Update()
    {

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&  !soundHasBeenSaid)
        {
            source1.PlayOneShot(sound1);
            soundHasBeenSaid = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        soundHasBeenSaid = false;
    }
}
