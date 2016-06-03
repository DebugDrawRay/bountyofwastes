using UnityEngine;
using DG.Tweening;

public class Jets : MonoBehaviour
{
    public Rigidbody rigid;

    [Header("Properties")]
    public float jetStrength;

    [Header("Grounding Checks")]
    public float checkGroundedDistance;
    public LayerMask ground;

    void Start()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Subscribe(ActivateJets);
        }
    }
    void OnDestroy()
    {
        if (GetComponent<InputBus>())
        {
            GetComponent<InputBus>().Unsubscribe(ActivateJets);
        }
    }

    void ActivateJets(InputState input)
    {
        if (enabled)
        {
            Vector3 direction = Quaternion.AngleAxis(-input.Move.x * 45, Vector3.up) * (input.Move.x * transform.right);

            if (input.Jump && CheckGrounded())
            {
                if (input.LockOn)
                {
                    if (input.Move.x == 0)
                    {
                        Vector3 force = Vector3.up * jetStrength;
                        rigid.AddForce(force, ForceMode.Impulse);
                    }
                }
                else
                {
                    Vector3 force = Vector3.up * jetStrength;
                    rigid.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }

    bool CheckGrounded()
    {
        Ray groundedRay = new Ray(transform.position, -Vector3.up);
        return Physics.Raycast(groundedRay, checkGroundedDistance, ground);
    }
}
