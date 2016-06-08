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
    public float maneuveringPeriodDelay;
    private float currentDelay;
    private float currentPeriod;

    public float distanceLimit;

    [Header("Direction Control")]
    public Vector3[] maneuveringDirections;
    public bool randomizeDirections;
    public float targetFacingSpeed;

    private Vector3 currentDirection;
    private int selectedDirection = 0;

    [Header("Attacking Properties")]
    public float attackLength;
    private float currentAttackLength;

    public Transform target
    {
        get;
        private set;
    }

    void Awake()
    {
        Initialize();

        maneuveringLength += maneuveringPeriodDelay * maneuveringPeriods;
        currentManeuveringLength = maneuveringLength;
        currentPeriod = maneuveringLength / maneuveringPeriods;
        currentDelay = maneuveringPeriodDelay;

        currentAttackLength = attackLength;

        currentDirection = FindNewDirection();
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
                if (!Maneuver())
                {
                    currentState = State.Attacking;
                }
                break;
            case State.Attacking:
                if(!Attacking())
                {
                    currentState = State.Maneuvering;
                }
                break;
        }
    }

    bool Maneuver()
    {
        if (target)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            LookAtTarget();

            if (maneuveringDirections.Length > 0)
            {
                if (currentManeuveringLength > 0)
                {
                    currentManeuveringLength -= Time.deltaTime;
                    if (currentPeriod > 0)
                    {
                        currentPeriod -= Time.deltaTime;
                        input.Move = currentDirection;
                    }
                    else
                    {
                        if (currentDelay > 0)
                        {
                            currentDelay -= Time.deltaTime;
                            input.Move = input.Move * .9f;
                        }
                        else
                        {
                            if (distance >= distanceLimit)
                            {
                                currentDirection = new Vector3(0, .5f, 0);
                            }
                            else
                            {
                                currentDirection = FindNewDirection();
                            }
                            currentPeriod = maneuveringLength / maneuveringPeriods;
                            currentDelay = maneuveringPeriodDelay;
                        }
                    }
                    return true;
                }
                else
                {
                    currentManeuveringLength = maneuveringLength;
                    currentPeriod = maneuveringLength / maneuveringPeriods;
                    currentDirection = FindNewDirection();
                    input.Move = Vector3.zero;
                    return false;
                }
            }
            else
            {
                input.Move = Vector3.zero;
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool Attacking()
    {
        if(currentAttackLength > 0)
        {
            currentAttackLength -= Time.deltaTime;
            input.UseItem = true;
            return true;
        }
        else
        {
            input.UseItem = false;
            currentAttackLength = attackLength;
            return false;
        }
    }

    void LookAtTarget()
    {
        if (target)
        {
            Vector3 direction = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
            direction.x = 0;
            direction.z = 0;
            Quaternion lookAt = Quaternion.Euler(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, targetFacingSpeed);
        }
    }

    Vector3 FindNewDirection()
    {
        if (randomizeDirections)
        {
            selectedDirection = Random.Range(0, maneuveringDirections.Length);
        }
        else
        {
            selectedDirection++;
            if (selectedDirection >= maneuveringDirections.Length)
            {
                selectedDirection = 0;
            }
        }
        return maneuveringDirections[selectedDirection];
    }
}
