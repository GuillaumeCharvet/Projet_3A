using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject leaderPrefab;
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private int numberOfAnimals;
    private List<FlockAnimal> animals = new List<FlockAnimal>();
    [SerializeField] public List<Vector3> positions = new List<Vector3>();

    private int currentIndex = 1;
    private float currentPosition = 0f;
    private float speed = 10f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        /*
        for (int i = 0; i < numberOfAnimals; i++)
        {

        }
        */
    }

    void Update()
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
            animal.Move();
        }
    }

    public class FlockAnimal
    {
        public void Move()
        {

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
}
