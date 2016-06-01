using UnityEngine;
using System.Collections;
using CollisionUtilities;
using DG.Tweening;

public class LockOn : MonoBehaviour
{
    public string[] ignoreLockOn;
    public LayerMask includedColliders;
    public Transform head;

    public float maxLockOnDistance;
    private bool lockHeld;

    public Transform target
    {
        get;
        private set;
    }

    private bool bodyFocused;
    private bool headFocused;

    private Tween bodyTween;
    private Tween headTween;

    void Start()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(LockTarget);
        }
    }

    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(LockTarget);
        }
    }

    Transform CastTargetRay()
    {
        Ray targeting = new Ray(head.position, head.forward);
        RaycastHit checkTarget;

        if (Physics.Raycast(targeting, out checkTarget, maxLockOnDistance, includedColliders))
        {
            if (!Check.IgnoredTags(checkTarget.collider.tag, ignoreLockOn))
            {
                return checkTarget.transform;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    void LockTarget(InputState input)
    {
        if (input.LockOn)
        {
            if(!lockHeld)
            {
                target = CastTargetRay();
                lockHeld = true;
            }
            if (target != null)
            {
                LockedOn();
            }
        }
        else
        {
            ResetLockOn();
        }
    }

    void LockedOn()
    {
        Vector3 dir = target.position - head.transform.position;
        Vector3 lookRot = Quaternion.LookRotation(dir, Vector3.up).eulerAngles;

        head.localRotation = Quaternion.Euler(lookRot.x, 0, 0);
        transform.rotation = Quaternion.Euler(0, lookRot.y, 0);

        if (GetComponent<RotateLook>() != null)
        {
            GetComponent<RotateLook>().enabled = false;
        }
    }

    void ResetLockOn()
    {
        lockHeld = false;

        bodyFocused = false;
        headFocused = false;

        bodyTween = null;
        headTween = null;

        if (GetComponent<RotateLook>() != null)
        {
            GetComponent<RotateLook>().enabled = true;
        }
    }

    void CompleteBodyFocus()
    {
        bodyFocused = true;
    }
    void CompleteHeadFocus()
    {
        headFocused = true;
    }
}
