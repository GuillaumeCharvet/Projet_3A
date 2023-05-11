using UnityEngine.Audio;
using UnityEngine;

public class S_Play_Sound : MonoBehaviour
{
    /// bruits de pas //////////
    public AudioClip sound1;

    public AudioSource source1;

    public AudioClip sound2;
    public AudioSource source2;

    ///bruit d'escalade////
    //// mains
    public AudioClip sound3;

    public AudioSource source3;

    public AudioClip sound4;
    public AudioSource source4;

    /// pieds
    public AudioClip sound5;

    public AudioClip sound6;

    // Update is called once per frame
    private void Update()
    {
    }

    ///sons de pas //////
    public void PlaySound1()
    {
        source1.pitch = Random.Range(0.8f, 1.2f);
        source1.PlayOneShot(sound1);
    }

    public void PlaySound2()
    {
        source2.pitch = Random.Range(0.8f, 1.2f);
        source2.PlayOneShot(sound2);
    }

    ///sons d'ecalade ////////
    //sons des mains
    public void PlaySound3()
    {
        source3.pitch = Random.Range(0.8f, 1.2f);
        source3.PlayOneShot(sound3);
    }

    public void PlaySound4()
    {
        source4.pitch = Random.Range(0.8f, 1.2f);
        source4.PlayOneShot(sound4);
    }

    //sons des pieds
    public void PlaySound5()
    {
        source1.pitch = Random.Range(0.8f, 1.2f);
        source1.PlayOneShot(sound5);
    }

    public void PlaySound6()
    {
        source2.pitch = Random.Range(0.8f, 1.2f);
        source2.PlayOneShot(sound6);
    }
}