using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAnimal : MonoBehaviour
{
    public FlockBehaviour flock;
    private int currentIndex = 1;
    private float currentPosition = 0f;
    private float speed = 10f;
    private float smoothSpeed;
    public float targetSmoothSpeed = 0.1f;
    private float referenceDistance = 100f;
    private float maxAcceleration = 0.01f;
    private Vector3 lastMovement = Vector3.zero;
    public List<Vector3> positionsDelta = new List<Vector3>();
    public bool debug = false;

    public AnimationCurve velocityCurve = new AnimationCurve();

    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        smoothSpeed = targetSmoothSpeed;

        //anim. ["MovePlease"].time = Random.Range(0f, 10f);
    }

    public void Move()
    {
        var previousPos = transform.position;
        var distance = speed * Time.deltaTime;
        currentPosition += distance;
        transform.position = Vector3.MoveTowards(transform.position, positionsDelta[(currentIndex + 1) % positionsDelta.Count], distance);
        if (currentPosition >= Vector3.Distance(positionsDelta[currentIndex], positionsDelta[(currentIndex + 1) % positionsDelta.Count]))
        {
            currentPosition = 0f;
            currentIndex = (currentIndex + 1) % positionsDelta.Count;
        }
        lastMovement = transform.position - previousPos;
        if (lastMovement.magnitude > Mathf.Epsilon) transform.rotation = Quaternion.LookRotation(lastMovement, Vector3.up);
    }

    public void MoveSmooth()
    {
        var previousPos = transform.position;
        var distance = smoothSpeed * Time.deltaTime;
        var normalizedDistance = smoothSpeed * Time.deltaTime / referenceDistance;
        currentPosition += normalizedDistance;
        float t = currentPosition;
        var potentialPos = (1f - t) * (1f - t) * positionsDelta[currentIndex] + 2f * (1f - t) * t * flock.controlPoints[currentIndex] + t * t * positionsDelta[(currentIndex + 1) % positionsDelta.Count];

        var potentialDist = (potentialPos - previousPos).magnitude;
        var quotient = (targetSmoothSpeed * Time.deltaTime) / potentialDist;
        //Debug.Log("QUOTIENT = " + quotient);

        currentPosition -= normalizedDistance;
        normalizedDistance *= quotient;
        currentPosition += distance;
        t = currentPosition;

        transform.position = (1f - t) * (1f - t) * positionsDelta[currentIndex] + 2f * (1f - t) * t * flock.controlPoints[currentIndex] + t * t * positionsDelta[(currentIndex + 1) % positionsDelta.Count];

        if (currentPosition >= 1f)
        {
            currentPosition -= 1f;
            currentIndex = (currentIndex + 1) % positionsDelta.Count;
        }
        lastMovement = transform.position - previousPos;
        var velocityMag = lastMovement.magnitude / Time.deltaTime;
        velocityCurve.AddKey(Time.time, velocityMag);
        if (debug) Debug.Log(velocityMag / Time.deltaTime);
        if (velocityMag > Mathf.Epsilon) transform.rotation = Quaternion.LookRotation(lastMovement, Vector3.up);
        /*
        if (velocityMag > targetSmoothSpeed) smoothSpeed -= maxAcceleration * Time.deltaTime;
        else if (velocityMag < targetSmoothSpeed) smoothSpeed += maxAcceleration * Time.deltaTime;*/
    }
}

/*
public void OnDrawGizmos()
{
    foreach (var position in positions)
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(position, 1);
    }
}*/