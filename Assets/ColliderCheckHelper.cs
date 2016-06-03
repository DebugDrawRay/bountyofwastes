using UnityEngine;
using UnityEngine.Events;
using CollisionUtilities;
public class ColliderCheckHelper : MonoBehaviour
{
    [Header("Trigger Events")]
    #region Trigger Events
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerStay;
    public UnityEvent TriggerExit;
    #endregion

    [Header("Collision Events")]
    #region Collider Events
    public UnityEvent CollisionEnter;
    public UnityEvent CollisionStay;
    public UnityEvent CollisionExit;
    #endregion

    [Header("Properties")]
    public string[] ignoredTags;

    void OnTriggerEnter(Collider hit)
    {
        if(Check.IgnoredTags(hit.tag, ignoredTags))
        {
            if (TriggerEnter != null)
            {
                TriggerEnter.Invoke();
            }
        }
    }
    void OnTriggerStay(Collider hit)
    {
        if (Check.IgnoredTags(hit.tag, ignoredTags))
        {
            if (TriggerStay != null)
            {
                TriggerStay.Invoke();
            }
        }
    }
    void OnTriggerExit(Collider hit)
    {
        if (Check.IgnoredTags(hit.tag, ignoredTags))
        {
            if (TriggerExit != null)
            {
                TriggerExit.Invoke();
            }
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (Check.IgnoredTags(hit.transform.tag, ignoredTags))
        {
            if (CollisionEnter != null)
            {
                CollisionEnter.Invoke();
            }
        }
    }
    void OnCollisionStay(Collision hit)
    {
        if (Check.IgnoredTags(hit.transform.tag, ignoredTags))
        {
            if (CollisionStay != null)
            {
                CollisionStay.Invoke();
            }
        }
    }
    void OnCollisionExit(Collision hit)
    {
        if (Check.IgnoredTags(hit.transform.tag, ignoredTags))
        {
            if (CollisionExit != null)
            {
                CollisionExit.Invoke();
            }
        }
    }
}
