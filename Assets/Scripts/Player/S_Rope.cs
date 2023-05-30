using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Rope : MonoBehaviour
{
    private Transform playerTrsf;

    public List<Transform> ropeSection = new List<Transform>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private float sectionMaxLength = 1f;
    private float ropeElasticity = 150f;
    private float gravity = -20f;
    private float inertia = 1f;

    private Transform ropeStart, ropeEnd;
    private GameObject ropeExtremityPrefab;
    private Transform transformParentProjectiles;

    private void Start()
    {
        ropeStart = GetComponent<S_HarponBehaviour>().ropeStartTrsf;
        ropeSection.Add(ropeStart);
        rigidbodies.Add(ropeStart.GetComponent<Rigidbody>());

        playerTrsf = GameObject.FindGameObjectWithTag("Player").transform;
        ropeEnd = playerTrsf.GetComponent<StateMachineParameters>().ropeEndOnPlayer;
        ropeExtremityPrefab = playerTrsf.GetComponent<StateMachineParameters>().ropeExtremityPrefab;

        transformParentProjectiles = GameObject.FindGameObjectWithTag("Projectiles").transform;
    }

    private void FixedUpdate()
    {
        //Debug.Log("section rope : " + Vector3.Distance(ropeEnd.position, ropeSection[ropeSection.Count - 1].position));
        if (Vector3.Distance(ropeEnd.position, ropeSection[ropeSection.Count - 1].position) > sectionMaxLength)
        {
            var section = Instantiate(ropeExtremityPrefab, transformParentProjectiles);
            section.transform.position = 0.5f * ropeEnd.position + 0.5f * ropeSection[ropeSection.Count - 1].position;
            ropeSection.Add(section.transform);
            rigidbodies.Add(section.transform.GetComponent<Rigidbody>());
        }
        /*
        var ropeSectionCopy = new List<Transform>(ropeSection);
        for (int i = 1; i < ropeSection.Count - 1; i++)
        {
            var f1 = ropeSectionCopy[i].position - ropeSectionCopy[i - 1].position;
            var f2 = ropeSectionCopy[i].position - ropeSectionCopy[i + 1].position;
            Debug.Log("rope lenght " + i + " : " + f1.magnitude + " : " + f2.magnitude);
            var newPosi = ropeSectionCopy[i].position + inertia * rigidbodies[i].velocity * Time.deltaTime + (-ropeElasticity * (Mathf.Max(f1.magnitude - sectionMaxLength, 0f) * f1.normalized + Mathf.Max(f2.magnitude - sectionMaxLength, 0f) * f2.normalized) + gravity * Vector3.up) * Time.deltaTime * Time.deltaTime;//ropeSection[i - 1].position + sectionMaxLength * (ropeSection[i].position - ropeSection[i - 1].position).normalized;
            rigidbodies[i].MovePosition(newPosi);
        }

        // a(t) = -g / m + F/m
        // v(t+dt) = v(t) + (-g/m + F/m) * dt
        // x(t+dt)-x(t) = v(t) * dt + (-g/m + F/m) * dt*dt

        if (ropeSection.Count > 1)
        {
            var lastSectionIndex = ropeSection.Count - 1;

            var f1b = ropeSectionCopy[lastSectionIndex].position - ropeSectionCopy[lastSectionIndex - 1].position;
            var f2b = ropeSectionCopy[lastSectionIndex].position - ropeEnd.transform.position;

            var newPos = ropeSectionCopy[lastSectionIndex].position + inertia * rigidbodies[lastSectionIndex].velocity * Time.deltaTime + (-ropeElasticity * (Mathf.Max(f1b.magnitude - sectionMaxLength, 0f) * f1b.normalized) + gravity * Vector3.up) * Time.deltaTime * Time.deltaTime;
            rigidbodies[lastSectionIndex].MovePosition(newPos);
        }*/

        var ropeSectionCopy = new List<Transform>(ropeSection);
        var newSpeeds = new List<Vector3>();
        newSpeeds.Add(Vector3.zero);
        var lastSectionIndex = ropeSection.Count - 1;

        for (int i = 1; i < ropeSection.Count - 1; i++)
        {
            var f1 = ropeSectionCopy[i].position - ropeSectionCopy[i - 1].position;
            var f2 = ropeSectionCopy[i].position - ropeSectionCopy[i + 1].position;

            Debug.Log("rope lenght " + i + " : " + f1.magnitude + " : " + f2.magnitude);
            var speed = inertia * rigidbodies[i].velocity + (-ropeElasticity * (Mathf.Max(f1.magnitude - sectionMaxLength, 0f) * f1.normalized + Mathf.Max(f2.magnitude - sectionMaxLength, 0f) * f2.normalized) + gravity * Vector3.up) * Time.deltaTime;//ropeSection[i - 1].position + sectionMaxLength * (ropeSection[i].position - ropeSection[i - 1].position).normalized;
            newSpeeds.Add(speed);
        }

        var speedLastSection = Vector3.zero;
        if (ropeSection.Count > 1)
        {
            var f1b = ropeSectionCopy[lastSectionIndex].position - ropeSectionCopy[lastSectionIndex - 1].position;
            var f2b = ropeSectionCopy[lastSectionIndex].position - ropeEnd.transform.position;

            speedLastSection = inertia * rigidbodies[lastSectionIndex].velocity + (-ropeElasticity * (Mathf.Max(f1b.magnitude - sectionMaxLength, 0f) * f1b.normalized) + gravity * Vector3.up) * Time.deltaTime;
        }

        for (int i = 1; i < ropeSection.Count - 1; i++)
        {
            rigidbodies[i].MovePosition(ropeSectionCopy[i].position + newSpeeds[i] * Time.deltaTime);
        }

        if (ropeSection.Count > 1)
        {
            rigidbodies[lastSectionIndex].MovePosition(ropeSectionCopy[lastSectionIndex].position + speedLastSection * Time.deltaTime);
        }

        // a(t) = -g / m + F/m
        // v(t+dt) = v(t) + (-g/m + F/m) * dt
        // x(t+dt)-x(t) = v(t) * dt + (-g/m + F/m) * dt*dt
    }
}