using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlockAnimal2 : MonoBehaviour
{
    public FlockBehaviour2 flock;
    private Vector3 lastPosition;
    private Vector3 lastMovement = Vector3.zero;
    public List<Vector3> positionsDelta = new List<Vector3>();
    public bool debug = false;

    public AnimationCurve velocityCurve = new AnimationCurve();

    public List<FlockAnimal2> animals = new List<FlockAnimal2>();

    public Animator anim;

    // FLOCK

    private float inertiaFactor = 1f;
    private float repelDistance = 3f;
    private float repelFactor = 200f;
    private float attractFactor = 20f;

    private float randomPhaseShift;

    public void Start()
    {
        anim = GetComponent<Animator>();
        lastPosition = transform.position;

        anim.Play("Base Layer.MovePlease", 0, Random.Range(0f, 1f));
        randomPhaseShift = Random.Range(0f, 2f * Mathf.PI);
    }

    /*
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
        var potentialPos = (1f - t) * (1f - t) * positionsDelta[currentIndex] + 2f * (1f - t) * t * flock.controlPoints1[currentIndex] + t * t * positionsDelta[(currentIndex + 1) % positionsDelta.Count];

        var potentialDist = (potentialPos - previousPos).magnitude;
        var quotient = (targetSmoothSpeed * Time.deltaTime) / potentialDist;
        //Debug.Log("QUOTIENT = " + quotient);

        currentPosition -= normalizedDistance;
        normalizedDistance *= quotient;
        currentPosition += distance;
        t = currentPosition;

        transform.position = (1f - t) * (1f - t) * positionsDelta[currentIndex] + 2f * (1f - t) * t * flock.controlPoints1[currentIndex] + t * t * positionsDelta[(currentIndex + 1) % positionsDelta.Count];

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
    }
    */

    public void MoveWithFlock()
    {
        lastMovement = lastPosition - transform.position;
        lastPosition = transform.position;

        var newPosition = transform.position;
        var newSpeed = Vector3.zero;

        var repelForce = Vector3.zero;
        var inc = 0;
        foreach (var anim in animals)
        {
            //Debug.Log("nombre d'ANIMALES : " + animals.Count);
            var dist = Vector3.Distance(anim.transform.position, transform.position);
            if (dist < repelDistance)
            {
                repelForce += (1f / (1f + dist)) * (transform.position - anim.transform.position);
                //Debug.DrawLine(transform.position, anim.transform.position);
                inc++;
            }
        }
        repelForce /= Mathf.Max(1f, (float)inc);

        var attractForce = flock.transform.position + 2f * Mathf.Cos(0.3f * Time.time + randomPhaseShift) * Vector3.up - transform.position;

        newSpeed = inertiaFactor * lastMovement + (repelFactor * repelForce + attractFactor * attractForce) * Time.deltaTime;
        newPosition += newSpeed * Time.deltaTime;

        if (newSpeed.magnitude > Mathf.Epsilon) transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(newSpeed, Vector3.up), 1f);

        transform.position = newPosition;
    }
}