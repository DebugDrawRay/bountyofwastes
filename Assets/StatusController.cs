using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusController : MonoBehaviour
{
    //Stats - Controls resource stats that can be modified by outside sources

    public enum Stat
    {
        Armor,
        Shield
    }

    [Header("Armor Resource")]
    public float baseArmor;
    public int armorModules;
    public float currentArmor
    {
        get;
        private set;
    }

    [Header("Shield Resource")]
    public float baseShield;
    public int shieldModules;
    public float currentShield
    {
        get;
        private set;
    }

    //Power - Controls functionality of modules

    public enum PowerInterface
    {
        Thrusters,
        Reactor,
        Head,
        Legs,
        Arms
    }

    [Header("Power Resources")]
    public int powerModules;

    private Dictionary<PowerInterface, int> currentPowerDistribution;

    [Header("Animations")]
    public Animator anim;
    public string damagedAnimTrigger;
    public string deathAnimTrigger;
    public float deathAnimLength;

    [Header("Reactions")]
    public float invulnerableTime;
    private bool invul;
    void Awake()
    {
        InitializeCurrentStatus();
    }

    void InitializeCurrentStatus()
    {
        currentArmor = baseArmor + (baseArmor * armorModules);
        currentShield = baseShield + (baseShield * shieldModules);

        currentPowerDistribution = new Dictionary<PowerInterface, int>()
        {
            {PowerInterface.Thrusters, 0 },
            {PowerInterface.Reactor, 0 },
            {PowerInterface.Head, 0 },
            {PowerInterface.Legs, 0 },
            {PowerInterface.Arms, 0 }
        };
    }

    public void ModifyArmor(CollisionInfo info)
    {
        if (!invul)
        {
            currentArmor = currentArmor + info.statChangeAmount;
            if (info.statChangeAmount < 0)
            {
                DamagedEvent();
            }
            if(invulnerableTime > 0)
            {
                StartCoroutine(Invulnerable(invulnerableTime));
            }
            CheckStatus();
        }
    }

    public void ModifyShield(CollisionInfo info)
    {
        currentShield = currentShield + info.statChangeAmount;
        if (info.statChangeAmount < 0)
        {
            DamagedEvent();
        }
        if (invulnerableTime > 0)
        {
            StartCoroutine(Invulnerable(invulnerableTime));
        }
        CheckStatus();
    }

    void CheckStatus()
    {
        if(currentArmor <= 0)
        {
            DeathEvent();
        }
    }

    void DamagedEvent()
    {
        if (anim)
        {
            anim.SetTrigger(damagedAnimTrigger);
        }
    }

    void DeathEvent()
    {
        if (anim)
        {
            anim.SetTrigger(deathAnimTrigger);
        }
        invul = true;
        StartCoroutine(Death(deathAnimLength));
    }

    IEnumerator Death(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    IEnumerator Invulnerable(float time)
    {
        invul = true;
        yield return new WaitForSeconds(time);
        invul = false;
    }
}
