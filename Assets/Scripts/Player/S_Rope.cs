using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Rope : MonoBehaviour
{
    private Transform playerTrsf;

    public List<Transform> ropeSection = new List<Transform>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private float sectionMaxLength = 1f;

    private Transform ropeStart, ropeEnd;
    private GameObject ropeExtremityPrefab;

    private void Start()
    {
        ropeStart = GetComponent<S_HarponBehaviour>().ropeStartTrsf;
        ropeSection.Add(ropeStart);
        rigidbodies.Add(ropeStart.GetComponent<Rigidbody>());

        playerTrsf = GameObject.FindGameObjectWithTag("Player").transform;
        ropeEnd = playerTrsf.GetComponent<StateMachineParameters>().ropeEndOnPlayer;
        ropeExtremityPrefab = playerTrsf.GetComponent<StateMachineParameters>().ropeExtremityPrefab;
    }

    private void Update()
    {
        Debug.Log("section rope : " + Vector3.Distance(ropeEnd.position, ropeSection[ropeSection.Count - 1].position));
        if (Vector3.Distance(ropeEnd.position, ropeSection[ropeSection.Count - 1].position) > sectionMaxLength)
        {
            var section = Instantiate(ropeExtremityPrefab);
            section.transform.position = 0.5f * ropeEnd.position + 0.5f * ropeSection[ropeSection.Count - 1].position;
            ropeSection.Add(section.transform);
            rigidbodies.Add(section.transform.GetComponent<Rigidbody>());
        }
        /*
        for (int i = 1; i < ropeSection.Count - 1; i++)
        {
            rgbd.MovePosition(ropeSection[i].position,)
            ropeSection[i].position = ;//ropeSection[i - 1].position + sectionMaxLength * (ropeSection[i].position - ropeSection[i - 1].position).normalized;
        }
        ropeSection[ropeSection.Count - 1].position =
        */
    }
}