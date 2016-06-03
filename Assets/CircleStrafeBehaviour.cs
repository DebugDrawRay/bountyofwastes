using UnityEngine;
using System.Collections;

public class CircleStrafeBehaviour : InputController
{
    public enum State
    {
        Inactive,
        Maneuvering,
        Attacking
    }
    public State currentState;

    [Header("Maneuvering Properties")]
    public float maneuveringLength;
    private float currentManeuveringLength;

    public int maneuveringPeriods;
    private float currentPeriod;

    [Header("Direction Control")]
    public Vector3[] maneuveringDirections;
    public bool randomizeDirections;
    public float targetFacingSpeed;
    private int currentDirection = 0;

    [Header("Attacking Properties")]
    public float attackLength;

    private Transform target;

    void Awake()
    {
        Initialize();
        currentManeuveringLength = maneuveringLength;
        currentPeriod = maneuveringLength / maneuveringPeriods;
        if (randomizeDirections)
        {
            currentDirection = Random.Range(0, maneuveringDirections.Length);
        }
    }

    void Start()
    {
        target = PlayerController.instance.targetObject.transform;
    }

    void Update()
    {
        RunStates();
        UpdateBus();
    }

    void RunStates()
    {
        switch (currentState)
        {
            case State.Inactive:
                break;
            case State.Maneuvering:
                Maneuver();
                break;
            case State.Attacking:
                break;
        }
    }

    void Maneuver()
    {
        if (maneuveringDirections.Length > 0)
        {
            if (currentManeuveringLength > 0)
            {
                currentManeuveringLength -= Time.deltaTime;
                if (currentPeriod > 0)
                {
                    currentPeriod -= Time.deltaTime;
                    input.Move = maneuveringDirections[currentDirection];
                }
                else
                {
                    if(randomizeDirections)
                    {
                        currentDirection = Random.Range(0, maneuveringDirections.Length);
                    }
                    else
                    {
                        currentDirection++;
                        if(currentDirection >= maneuveringDirections.Length)
                        {
                            currentDirection = 0;
                        }
                    }
                    currentPeriod = maneuveringLength / maneuveringPeriods;
                }
            }
        }
        else
        {
            currentManeuveringLength = maneuveringLength;
            currentPeriod = maneuveringLength / maneuveringPeriods;
            if (randomizeDirections)
            {
                currentDirection = Random.Range(0, maneuveringDirections.Length);
            }
            else
            {
                currentDirection = 0;
            }
        }

        Vector3 direction = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
        direction.x = 0;
        direction.z = 0;
        Quaternion lookAt = Quaternion.Euler(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, targetFacingSpeed);
    }
}
