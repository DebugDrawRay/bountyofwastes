using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    public float knockbackForce;
    public Rigidbody rigid;

    public void Knockback(CollisionInfo info)
    {
        if (rigid)
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(info.hitTransform.forward * knockbackForce, ForceMode.Impulse);
        }
    } 
}
