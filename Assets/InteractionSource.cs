using UnityEngine;
using System.Collections;

public class InteractionSource : MonoBehaviour
{
    [Header("Status Interaction Properties")]
    [Tooltip("Positive and negative values are taken in account")]
    public float armorModifyAmount;
    public bool destroyOnInteraction;

    void OnTriggerEnter(Collider hit)
    {
        StatusController hasStatus = hit.GetComponent<StatusController>();
        if(hasStatus)
        {
            Interact(hasStatus);
        }
    }

    void Interact(StatusController target)
    {
        target.ModifyCurrentStat(StatusController.Stat.Armor, armorModifyAmount);
        if(destroyOnInteraction)
        {
            Destroy(gameObject);
        }
    }
}
