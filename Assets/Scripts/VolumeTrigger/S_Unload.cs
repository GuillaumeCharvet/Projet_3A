using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Unload : MonoBehaviour
{
    public GameObject package1;
    public GameObject package2;
    public GameObject package3;
    public GameObject package4;
    public GameObject package5;
    private MeshRenderer[] renderer1, renderer2, renderer3, renderer4, renderer5;

    public void Start()
    {
        renderer1 = package1.GetComponentsInChildren<MeshRenderer>();
        renderer2 = package2.GetComponentsInChildren<MeshRenderer>();
        renderer3 = package3.GetComponentsInChildren<MeshRenderer>();
        renderer4 = package4.GetComponentsInChildren<MeshRenderer>();
        renderer5 = package5.GetComponentsInChildren<MeshRenderer>();
    }
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        foreach (var item in renderer1)
        {
            item.enabled = false;
        }
        foreach (var item in renderer2)
        {
            item.enabled = false;
        }
        foreach (var item in renderer3)
        {
            item.enabled = false;
        }
        foreach (var item in renderer4)
        {
            item.enabled = false;
        }
        foreach (var item in renderer5)
        {
            item.enabled = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        foreach (var item in renderer1)
        {
            item.enabled = true;
        }
        foreach (var item in renderer2)
        {
            item.enabled = true;
        }
        foreach (var item in renderer3)
        {
            item.enabled = true;
        }
        foreach (var item in renderer4)
        {
            item.enabled = true;
        }
        foreach (var item in renderer5)
        {
            item.enabled = true; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
