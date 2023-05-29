using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FarAwayCameraTrigger : MonoBehaviour
{
    private CinemachineFreeLook cam;
    [SerializeField] private float dezoomAmplify = 1f;

    private void Start()
    {
        cam = transform.parent.GetComponent<CinemachineFreeLook>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.Priority = 1000;
            cam.m_Orbits[0].m_Radius *= dezoomAmplify;
            cam.m_Orbits[1].m_Radius *= dezoomAmplify;
            cam.m_Orbits[2].m_Radius *= dezoomAmplify;
            cam.m_Orbits[0].m_Height *= dezoomAmplify;
            cam.m_Orbits[1].m_Height *= dezoomAmplify;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.Priority = 0;
        }
    }
}