using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public Rigidbody rigid;
    public float travelSpeed;

    void Start()
    {
        rigid.velocity = transform.forward * travelSpeed;
    }
}
