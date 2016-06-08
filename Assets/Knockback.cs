using UnityEngine;
using CollisionUtilities;

public class Knockback : MonoBehaviour
{
    public float knockbackForce;
    public string[] ignoredTags;

    void OnTriggerEnter(Collider hit)
    {
        if(!Check.IgnoredTags(hit.tag, ignoredTags))
        {
            Rigidbody rigid = hit.GetComponent<Rigidbody>();
            if (rigid)
            {
                Debug.Log("Knockback");
                rigid.velocity = Vector3.zero;
                rigid.AddForce(transform.forward * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}
