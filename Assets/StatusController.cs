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

    [Header("Stat Resources")]
    public float baseArmor;
    public int armorModules;
    public float baseShield;
    public int shieldModules;

    private Dictionary<Stat, float> currentStats;

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

    void Awake()
    {
        InitializeCurrentStatus();
    }

    void InitializeCurrentStatus()
    {
        currentStats = new Dictionary<Stat, float>()
        {
            {Stat.Armor, baseArmor + (baseArmor * armorModules) },
            {Stat.Shield, baseShield + (baseShield * shieldModules) }
        };

        currentPowerDistribution = new Dictionary<PowerInterface, int>()
        {
            {PowerInterface.Thrusters, 0 },
            {PowerInterface.Reactor, 0 },
            {PowerInterface.Head, 0 },
            {PowerInterface.Legs, 0 },
            {PowerInterface.Arms, 0 }
        };
    }
}
