using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public UnityEvent AttackAction;

    public float activationRate;
    private float currentActivationTime;

    public float cooldownTime;
    private float currentCooldown;

    public int activationLimit;
    private int currentActivations;

    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if(currentActivationTime > 0)
        {
            currentActivationTime -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if(currentCooldown <= 0)
        {
            if(currentActivationTime <= 0)
            {
                AttackAction.Invoke();
                currentActivationTime = activationRate;
                currentActivations++;

                if(activationLimit > 0 && currentActivations >= activationLimit)
                {
                    currentCooldown = cooldownTime;
                    currentActivations = 0;
                    currentActivationTime = 0;
                }
            }
        }
    }
}
