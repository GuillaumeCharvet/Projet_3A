using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject leaderPrefab;
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private Transform kidsRoom;
    [SerializeField] private int numberOfAnimals;
    public float flockSpread = 0f;
    public float meanVelocity;
    private List<FlockAnimal> animals = new List<FlockAnimal>();
    [SerializeField] public List<Vector3> positions = new List<Vector3>();
    [SerializeField] public List<Vector3> controlPoints = new List<Vector3>();

    private int currentIndex = 1;
    private float currentPosition = 0f;
    private float speed = 10f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        for (int i = 0; i < numberOfAnimals; i++)
        {
            var animal = Instantiate(animalPrefab, transform.position, Quaternion.identity, kidsRoom).GetComponent<FlockAnimal>();
            animal.flock = this;
            animal.targetSmoothSpeed = meanVelocity;
            animals.Add(animal);
            for (int j = 0; j < positions.Count; j++)
            {
                animal.positionsDelta.Add(positions[j] + new Vector3(flockSpread * Random.Range(-1f, 1f), 0.1f * flockSpread * Random.Range(-1f, 1f), flockSpread * Random.Range(-1f, 1f)));
            }
        }
    }

    private void Update()
    {
        var previousPos = transform.position;
        var distance = speed * Time.deltaTime;
        currentPosition += distance;
        transform.position = Vector3.MoveTowards(transform.position, positions[(currentIndex + 1) % positions.Count], distance);
        if (currentPosition >= Vector3.Distance(positions[currentIndex], positions[(currentIndex + 1) % positions.Count]))
        {
            currentPosition = 0f;
            currentIndex = (currentIndex + 1) % positions.Count;
        }
        velocity = transform.position - previousPos;
        if (velocity.sqrMagnitude > Mathf.Epsilon) transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);

        foreach (var animal in animals)
        {
            animal.MoveSmooth();
        }
    }

    public void ResetControlPoints()
    {
        controlPoints = new List<Vector3>();
        for (int i = 1; i < positions.Count + 1; i++)
        {
            controlPoints.Add(0.5f * (positions[i - 1] + positions[i % positions.Count]));
        }
    }

    public void AddPoint()
    {
        var newPoint = 0.5f * (positions[0] + positions[positions.Count - 1]);
        positions.Add(newPoint);
        var newControlPoint = 0.5f * (positions[positions.Count - 1] + positions[0]);
        controlPoints.Add(newControlPoint);
    }
}