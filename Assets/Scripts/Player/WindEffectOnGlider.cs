using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Collider))]
public class WindEffectOnGlider : MonoBehaviour
{
    [SerializeField] private float windStrength;
    [SerializeField] private float windDirection;
    private float rescaledRadiusMax;
    private CapsuleCollider capsCollider;
    private Vector3 pointOfCapsule1, pointOfCapsule2;
    private StateMachineParameters stateMachineParameters;
    [SerializeField] private AnimationCurve distanceEffectOnWindStrength;

    private void Start()
    {
        capsCollider = GetComponent<CapsuleCollider>();
        pointOfCapsule1 = transform.position - transform.localScale.y * (capsCollider.height / 2f - capsCollider.radius) * transform.up;
        pointOfCapsule2 = transform.position + transform.localScale.y * (capsCollider.height / 2f - capsCollider.radius) * transform.up;
        rescaledRadiusMax = capsCollider.radius * Mathf.Max(transform.localScale.x, transform.localScale.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachineParameters = other.GetComponent<StateMachineParameters>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            var dist = DistanceFromPointToCapsule(other.transform.position);
            var windAttenuation = Mathf.Max(0f, (rescaledRadiusMax - dist) / rescaledRadiusMax);

            var windAttenuationCurved = distanceEffectOnWindStrength.Evaluate(windAttenuation);

            /*
            var windAttenuation = Vector3.Distance(other.transform.position, capsCollider.ClosestPoint(other.transform.position)) / rescaledRadiusMax;
            */
            Debug.Log("dist : " + dist);
            stateMachineParameters.windEffect = windAttenuationCurved * windStrength * (transform.rotation * Vector3.up);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stateMachineParameters.windEffect = Vector3.zero;
        }
    }

    public float DistanceFromPointToCapsule(Vector3 pos)
    {
        if (Vector3.Dot(pos - pointOfCapsule1, pointOfCapsule2 - pointOfCapsule1) < 0f)
        {
            return Vector3.Distance(pos, pointOfCapsule1);
        }
        else if (Vector3.Dot(pos - pointOfCapsule2, pointOfCapsule1 - pointOfCapsule2) < 0f)
        {
            return Vector3.Distance(pos, pointOfCapsule2);
        }
        else
        {
            var projection = Vector3.Project(pos - pointOfCapsule1, pointOfCapsule2 - pointOfCapsule1);
            var distanceFromCapsule = Vector3.Distance(pos, projection + pointOfCapsule1);
            return distanceFromCapsule;
        }
    }
}