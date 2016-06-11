using UnityEngine;
using UnityEngine.Events;

public class CollisionReceiver : MonoBehaviour
{
    [System.Serializable]
    public class OnCollisionEvent : UnityEvent<CollisionInfo> { }
    [SerializeField]
    public OnCollisionEvent onCollisionEvent;

    public void OnCollision(CollisionInfo info)
    {
        if(onCollisionEvent != null)
        {
            Debug.Log("Invoked");
            onCollisionEvent.Invoke(info);
        }
    }
}
