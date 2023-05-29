using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlockAnimal : MonoBehaviour
{
    public FlockBehaviour flock;
    private int currentIndex = 1;
    private float currentPosition = 0f;
    private float speed = 10f;
    private float smoothSpeed = 0.4f;
    private float targetSmoothSpeed = 0.4f;
    private float maxAcceleration = 0.1f;
    private Vector3 velocity = Vector3.zero;
    public List<Vector3> positionsDelta = new List<Vector3>();

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
        velocity = transform.position - previousPos;
        if (velocity.sqrMagnitude > Mathf.Epsilon) transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
    }

    public void MoveSmooth()
    {
        var previousPos = transform.position;
        var distance = smoothSpeed * Time.deltaTime;
        currentPosition += distance;
        float t = currentPosition;
        transform.position = (1f - t) * (1f - t) * positionsDelta[currentIndex] + 2f * (1f - t) * t * flock.controlPoints[currentIndex] + t * t * positionsDelta[(currentIndex + 1) % positionsDelta.Count];

        if (currentPosition >= 1f)
        {
            currentPosition = 0f;
            currentIndex = (currentIndex + 1) % positionsDelta.Count;
        }
        velocity = transform.position - previousPos;
        var velocityMag = velocity.magnitude / Time.deltaTime;
        if (velocityMag > Mathf.Epsilon) transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
        if (velocityMag > targetSmoothSpeed) smoothSpeed -= maxAcceleration * Time.deltaTime;
        else if (velocityMag < targetSmoothSpeed) smoothSpeed += maxAcceleration * Time.deltaTime;
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
