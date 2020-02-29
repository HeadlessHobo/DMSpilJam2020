using System;
using Common.UnitSystem.Stats;
using UnityEngine;

[Serializable][CreateAssetMenu(fileName = "BlockStatsManager", menuName = "Block stats manager", order = 53)]
public class BlockStatsManager : UnitStatsManager<UnitHealthStats>
{
    [SerializeField]
    private UnitHealthStats _unitHealthStats;

    public override UnitHealthStats HealthStats => _unitHealthStats;
}