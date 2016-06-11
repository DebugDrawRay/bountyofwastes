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

    [Header("Weapon Visuals")]
    public Transform weapon;
    public float weaponOrientationSpeed;
    public float orientationDistanceMin;
    public LayerMask orientationTargetLayers;
    private Quaternion originWeaponRot;

    void Start()
    {
        originWeaponRot = weapon.localRotation;
    }
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

        OrientToTarget();
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

    void OrientToTarget()
    {
        Ray dirRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(dirRay, out hit, Mathf.Infinity, orientationTargetLayers))
        {
            if (Vector3.Distance(transform.position, hit.point) > orientationDistanceMin)
            {
                Quaternion look = Quaternion.LookRotation(hit.point - weapon.position);
                weapon.rotation = Quaternion.Slerp(weapon.rotation, look, weaponOrientationSpeed);
            }
        }
        else
        {
            weapon.localRotation = Quaternion.Slerp(weapon.localRotation, originWeaponRot, weaponOrientationSpeed);
        }
    }
}
