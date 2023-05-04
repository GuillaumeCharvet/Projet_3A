using UnityEngine.Audio;
using UnityEngine;


public class S_Play_Sound : MonoBehaviour
{
    public AudioClip sound1;
    public AudioSource source1;
    public AudioClip sound2;
    public AudioSource source2;

    // Update is called once per frame
    void Update()
    {
        

    }
    public void PlaySound1()
    {
        source1.pitch = Random.Range(0.8f, 1.2f);
        source1.PlayOneShot(sound1);
        Debug.Log("le son de pied se joue");
    }

    public void PlaySound2()
    {
        source2.pitch = Random.Range(0.8f, 1.2f);
        source2.PlayOneShot(sound2);
        Debug.Log("le son de pied se joue");

    }




}
