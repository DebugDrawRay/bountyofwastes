using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour
{
    public float lifetime;

    void Update()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
