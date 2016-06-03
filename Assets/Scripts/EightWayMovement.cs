using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EightWayMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public Rigidbody rigid;
    public float movementSpeed;
    public float movementAcceleration;

    private bool dodging;
    private float dodgeDirection;

    [Header("Dodge Properties")]
    public float dodgeSpeed;

    public float dodgeLength;
    private float currentDodgeLength;

    public float dodgeCooldown;
    private float currentCooldown;

    private Tween dodgeTween;

    [Header("Head Bobbing")]
    public bool enableHeadBob;
    public Transform bobTarget;
    public float bobThreshold;
    public float bobSpeed;
    public Ease bobEase;
    public Tween bobTween;

    void Start()
    {
        bobTween = bobTarget.DOLocalMoveY(bobThreshold + bobTarget.localPosition.y, bobSpeed);
        bobTween.SetLoops(-1, LoopType.Yoyo);
        bobTween.SetEase(bobEase);
        bobTween.Pause();

        if(GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(UpdateMovement);
        }
    }
    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(UpdateMovement);
        }
    }
    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    void UpdateMovement(InputState input)
    {
        if (enabled)
        {
            Vector3 newVelocity = Vector3.zero;

            //General Movement
            Vector3 forward = transform.forward * input.Move.y;
            Vector3 right = transform.right * input.Move.x;
            Vector3 move = (forward + right) * movementSpeed;

            move.y = rigid.velocity.y;
            newVelocity = Vector3.MoveTowards(rigid.velocity, move, movementAcceleration);

            //Dodging
            if (!dodging && input.Jump && input.LockOn && currentCooldown <= 0 && input.Move.x != 0)
            {
                currentDodgeLength = dodgeLength;
                dodgeDirection = input.Move.x;
                dodging = true;
            }

            if (dodging)
            {
                //Vector3 dodgeAngle = Quaternion.AngleAxis(-dodgeDirection * 20, Vector3.up) * (transform.right * dodgeDirection);
                Vector3 dodgeAngle = transform.right * dodgeDirection;
                if (currentDodgeLength >= 0)
                {
                    Vector3 force = dodgeAngle * dodgeSpeed;
                    newVelocity = force;
                    currentDodgeLength -= Time.deltaTime;
                }
                else
                {
                    newVelocity = dodgeAngle * movementSpeed;
                    currentCooldown = dodgeCooldown;
                    dodging = false;
                }
            }
            else
            {
                if (enableHeadBob)
                {
                    if (input.Move.x != 0 || input.Move.y != 0)
                    {
                        bobTween.Play();
                    }

                    if (input.Move.x == 0 && input.Move.y == 0)
                    {
                        bobTween.Pause();
                    }
                }
            }
            rigid.velocity = newVelocity;
        }
    }

    void CompleteDodge()
    {
        dodgeTween = null;
        dodging = false;
        currentCooldown = dodgeCooldown;
    }
}
