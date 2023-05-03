using UnityEngine.Audio;
using UnityEngine;
using System;

public class S_Sound_Manager : MonoBehaviour
{
    public S_Sounds[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (S_Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
   public void Play (string name)
    {
        S_Sounds s = Array.Find(sounds, sounds => sounds.name == name);
        if(s == null)
            {
                return;
            }        
        s.source.Play();
    }
}
