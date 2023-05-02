using UnityEngine.Audio;
using UnityEngine;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
