using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlockAnimal2 : MonoBehaviour
{
    public FlockBehaviour2 flock;
    private Vector3 lastPosition;
    private Vector3 lastMovement = Vector3.zero;

    public bool debug = false;

    public AnimationCurve velocityCurve = new AnimationCurve();

    public List<FlockAnimal2> animals = new List<FlockAnimal2>();

    public Animator anim;

    // FLOCK

    private float inertiaFactor = 1f;
    public float repelDistance = 3f;
    private float repelFactor = 200f;
    private float attractFactor = 20f;

    private float randomPhaseShift, randomSpeedShift, randomGaussianDelay;

    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        lastPosition = transform.position;

        anim.Play("Base Layer.MovePlease", 0, Random.Range(0f, 1f));
        randomPhaseShift = Random.Range(0f, 2f * Mathf.PI);
        randomSpeedShift = Random.Range(-2f, 2f);
        randomGaussianDelay = Mathf.Abs(RandomGaussian(-3f, 3f));
    }

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

        var targetPosition = flock.dadsRoom.transform.position + randomSpeedShift * Mathf.Cos(0.3f * Time.time + randomPhaseShift) * Vector3.up - 2f * randomGaussianDelay * flock.lastMovement.normalized;
        var attractForce = targetPosition - transform.position;

        newSpeed = inertiaFactor * lastMovement + (repelFactor * repelForce + attractFactor * attractForce) * Time.deltaTime;
        newPosition += newSpeed * Time.deltaTime;

        if (newSpeed.magnitude > Mathf.Epsilon) transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(newSpeed, Vector3.up), 1f);

        transform.position = newPosition;
    }

    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
}