using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;

public class FlockBehaviour2 : MonoBehaviour
{
    [SerializeField] private GameObject leaderPrefab;
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private Transform kidsRoom;
    [SerializeField] public Transform dadsRoom;
    [SerializeField] private int numberOfAnimals;
    public float repelDistance = 3f;
    private List<FlockAnimal2> animals = new List<FlockAnimal2>();
    [SerializeField] public List<Vector3> positions = new List<Vector3>();
    [SerializeField] public List<Vector3> controlPoints1 = new List<Vector3>();
    [SerializeField] public List<Vector3> controlPoints2 = new List<Vector3>();

    private int currentIndex = 1;
    private float currentPosition = 0f;
    [SerializeField] private float smoothSpeed = 0.050f;
    private float referenceDistance = 100f;
    private Vector3 lastMovement = Vector3.zero;
    public AnimationCurve velocityCurve = new AnimationCurve();

    private bool moveAnimals = false;

    private void Start()
    {
        for (int i = 0; i < numberOfAnimals; i++)
        {
            var animal = Instantiate(animalPrefab, transform.position, Quaternion.identity, kidsRoom).GetComponent<FlockAnimal2>();
            animal.flock = this;
            animal.transform.localScale *= Random.Range(0.7f, 1.3f);
            animal.repelDistance = repelDistance;
            animals.Add(animal);
        }
        for (int i = 0; i < numberOfAnimals; i++)
        {
            animals[i].animals = animals;
        }

        kidsRoom.gameObject.SetActive(false);
        dadsRoom = GetComponentsInChildren<Transform>().Skip(1).First();
    }

    private void Update()
    {
        if (moveAnimals) Move();
    }

    public void Move()
    {
        var previousPos = dadsRoom.transform.position;
        var distance = smoothSpeed * Time.deltaTime;
        var normalizedDistance = smoothSpeed * Time.deltaTime / referenceDistance;
        currentPosition += normalizedDistance;
        float t = currentPosition;
        var potentialPos = (1f - t) * (1f - t) * (1f - t) * positions[currentIndex] + 3f * (1f - t) * (1f - t) * t * controlPoints1[currentIndex] + 3f * (1f - t) * t * t * controlPoints2[currentIndex] + t * t * t * positions[(currentIndex + 1) % positions.Count];

        var potentialDist = (potentialPos - previousPos).magnitude;
        var quotient = (smoothSpeed * Time.deltaTime) / potentialDist;
        //Debug.Log("QUOTIENT = " + quotient);

        currentPosition -= normalizedDistance;
        normalizedDistance *= quotient;
        currentPosition += distance;
        t = currentPosition;

        dadsRoom.transform.position = (1f - t) * (1f - t) * (1f - t) * positions[currentIndex] + 3f * (1f - t) * (1f - t) * t * controlPoints1[currentIndex] + 3f * (1f - t) * t * t * controlPoints2[currentIndex] + t * t * t * positions[(currentIndex + 1) % positions.Count];

        if (currentPosition >= 1f)
        {
            currentPosition -= 1f;
            currentIndex = (currentIndex + 1) % positions.Count;
        }
        lastMovement = dadsRoom.transform.position - previousPos;
        var velocityMag = lastMovement.magnitude / Time.deltaTime;
        velocityCurve.AddKey(Time.time, velocityMag);
        //if (debug) Debug.Log(velocityMag / Time.deltaTime);
        if (velocityMag > Mathf.Epsilon) dadsRoom.transform.rotation = Quaternion.LookRotation(lastMovement, Vector3.up);

        foreach (var animal in animals)
        {
            animal.MoveWithFlock();
        }
    }

    public void ResetControlPoints()
    {
        controlPoints1 = new List<Vector3>();
        controlPoints2 = new List<Vector3>();
        for (int i = 1; i < positions.Count + 1; i++)
        {
            controlPoints1.Add((2f / 3f) * positions[i - 1] + (1f / 3f) * positions[i % positions.Count]);
            controlPoints2.Add((1f / 3f) * positions[i - 1] + (2f / 3f) * positions[i % positions.Count]);
        }
    }

    public void AddPoint()
    {
        var newPoint = 0.5f * (positions[0] + positions[positions.Count - 1]);
        positions.Add(newPoint);
        var newControlPoint1 = (2f / 3f) * positions[positions.Count - 1] + (1f / 3f) * positions[0];
        controlPoints1.Add(newControlPoint1);
        var newControlPoint2 = (1f / 3f) * positions[positions.Count - 1] + (2f / 3f) * positions[0];
        controlPoints2.Add(newControlPoint2);
    }

    public void RemovePoint()
    {
        positions.RemoveAt(positions.Count - 1);
        controlPoints1.RemoveAt(controlPoints1.Count - 1);
        controlPoints2.RemoveAt(controlPoints2.Count - 1);
    }

    public void RecenterPathPoints()
    {
        var meanPosition = positions.Aggregate(Vector3.zero, (sum, position) => sum + position);
        meanPosition /= (float)positions.Count;
        meanPosition -= transform.position;
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] -= meanPosition;
            controlPoints1[i] -= meanPosition;
            controlPoints2[i] -= meanPosition;
        }
    }

    public void RotateStartingPpoint()
    {
        var firstItem = positions[0];
        positions.RemoveAt(0);
        positions.Add(firstItem);

        firstItem = controlPoints1[0];
        controlPoints1.RemoveAt(0);
        controlPoints1.Add(firstItem);

        firstItem = controlPoints2[0];
        controlPoints2.RemoveAt(0);
        controlPoints2.Add(firstItem);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            kidsRoom.gameObject.SetActive(true);
            moveAnimals = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            kidsRoom.gameObject.SetActive(false);
            moveAnimals = false;
        }
    }
}