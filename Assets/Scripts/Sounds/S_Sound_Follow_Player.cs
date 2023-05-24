using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Sound_Follow_Player : MonoBehaviour
{

    public GameObject sound_1;
    public GameObject sound_2;
    public GameObject playerPosition;
    
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        Sound1_FollowPlayer();

        Sound2_FollowPlayer();

    }
    public void Sound1_FollowPlayer()
    {
        sound_1.transform.position = new Vector3(playerPosition.transform.position.x, -55.00024f, playerPosition.transform.position.z);
    }
    public void Sound2_FollowPlayer()
    {
        sound_2.transform.position = new Vector3(playerPosition.transform.position.x, 177f, playerPosition.transform.position.z);
    }

}
