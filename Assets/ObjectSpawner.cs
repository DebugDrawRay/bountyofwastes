using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    public void Spawn()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
