using UnityEngine;
using CollisionUtilities;

public class CollisionSender : MonoBehaviour
{
    [SerializeField]
    public CollisionInfo collisionInfo;
    public bool destroyOnCollision;
    public string[] ignoredTags;

    void OnTriggerEnter(Collider hit)
    {
        CollisionReceiver hasReceiver = hit.GetComponent<CollisionReceiver>();
        if(hasReceiver && !Check.IgnoredTags(hit.tag, ignoredTags))
        {
            Debug.Log(hit.name);
            hasReceiver.OnCollision(collisionInfo);
        }
    }
}
