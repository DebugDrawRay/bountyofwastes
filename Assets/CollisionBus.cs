using UnityEngine;
using System.Collections;

public class CollisionBus : MonoBehaviour
{
    public delegate void CollisionEvent(CollisionInfo info);
    public CollisionEvent OnCollision;

    void Subscribe(CollisionEvent collisionEvent)
    {
        OnCollision += collisionEvent;
    }

    void Unsubscribe(CollisionEvent collisionEvent)
    {
        OnCollision -= collisionEvent;
    }

    public void InvokeCollision(CollisionInfo info)
    {
        if(OnCollision != null)
        {
            OnCollision(info);
        }
    }
}
