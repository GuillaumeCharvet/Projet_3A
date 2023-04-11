using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_map : MonoBehaviour
    
{
    [SerializeField]
    public S_rewardmanager s_Rewardmanager;
    public GameObject canvaMap;

    


    // Start is called before the first frame update
    void Start()
    {
        s_Rewardmanager.p1hbs
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            canvaMap.SetActive(true);


            if (s_Rewardmanager.p1hbs)
            {

            }
            
           /* if (Input.GetKeyDown(KeyCode.M))
            {
                canvaMap.SetActive(false);
            }*/
        }
    }
    
}
