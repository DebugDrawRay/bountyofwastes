using UnityEngine;
using System.Collections;

public class RamAction : MonoBehaviour
{
    [Header("Ram Properties")]
    public float attackTellTime;
    public float recoveryTime;
    public float ramSpeed;
    public Rigidbody rigid;

    [Header("Targeting")]
    public bool faceTarget;
    public float targetFacingSpeed;

    private Transform facingTarget;
    private bool inRam;
    private bool lookAt = true;

    void Start()
    {


        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(Ram);
        }
    }

    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(Ram);
        }
    }

    void Ram(InputState input)
    {
        if (input.UseItem)
        {
            if (!inRam)
            {
                StartCoroutine(PerformRam());
            }
            if (lookAt)
            {
                LookAtTarget();
            }
        }
    }

    IEnumerator PerformRam()
    {
        inRam = true;
        for(float i = 0; i <= attackTellTime; i += Time.deltaTime)
        {
            yield return null;
        }
        rigid.AddForce(transform.forward * ramSpeed, ForceMode.Impulse);
        lookAt = false;
        for(float i = 0; i <= recoveryTime; i += Time.deltaTime)
        {
            yield return null;
        }
        inRam = false;
        lookAt = true;
    }

    void LookAtTarget()
    {
        if (faceTarget && GetComponent<CircleStrafeBehaviour>())
        {
            facingTarget = GetComponent<CircleStrafeBehaviour>().target;
            if (facingTarget != null)
            {
                Vector3 direction = Quaternion.LookRotation(facingTarget.position - transform.position).eulerAngles;
                direction.x = 0;
                direction.z = 0;
                Quaternion lookAt = Quaternion.Euler(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, targetFacingSpeed);
            }
        }
    }
}
