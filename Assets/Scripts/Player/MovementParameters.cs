using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MovementParameters : MonoBehaviour
{
    public CharacterController characterController;

    [NonSerialized] public Vector3 moveDirection = Vector3.zero;

    public Transform trsfCamera;

    [Header("SENSIBILITY")]
    [SerializeField, Range(0f, 10f)]
    public float sensitivityH = 5f;
    [SerializeField, Range(0f, 10f)]
    public float sensitivityV = 5f;

    [SerializeField] public float moveSpeed = 10f;

    [Header("SWIM")]
    public bool isInWaterNextFixedUpdate = false;
    public BuoyancyEffect lastWaterVisited;
    public float forceOfWater;

    [Header("SLIDE")]
    public float speedOnRope = 0f;
    public float accelOnRope = 0.05f;
    public float maxSpeedOnRope = 1.5f;

    [Header("GLIDE")]
    public float gliderRotationSpeed = 0f;
    public float gliderRotationAcceleration = 0.5f;
    public float maxGliderRotationSpeed = 40f;

    public float gliderTurnSpeed = 0f;
    public float gliderCameraTurnSpeed = 0f;
    public float gliderTurnAcceleration = 0.1f;
    public float maxGliderTurnSpeed = 1f;

    public float GliderRotationSpeed { get => gliderRotationSpeed; set => gliderRotationSpeed = value; }

    public float currentHeightDiff = 0f;
    public float currentHeightRef = 0f;

    public float currentEnergy = 0f;
    public float speed = 0f;
    public float angleDiff = 0f;

    public Vector3 direction;
    public Vector3 weightForce;
    [SerializeField] public float mass = 10f;
    public Vector3 frictionForce;
    [SerializeField] public float frictionParameter = 0.01f;
    public Vector3 liftForce;
    [SerializeField, Range(0f, 108f)] public float liftParameter = 1f;


}
