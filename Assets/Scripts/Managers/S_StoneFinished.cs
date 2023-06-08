using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StoneFinished : MonoBehaviour
{
    [SerializeField]
    public S_TriggerDrop ile1;

    public S_TriggerDrop ile2;

    public GameObject wind1;

    // Update is called once per frame
    void Update()
    {
        if (ile1.stoneFinished && ile2.stoneFinished)
        {
            wind1.SetActive(true);
        }
    }
}
