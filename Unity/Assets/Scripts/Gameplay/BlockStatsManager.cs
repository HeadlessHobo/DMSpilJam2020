using System;
using Common.UnitSystem.Stats;
using Gameplay;
using UnityEngine;

[Serializable][CreateAssetMenu(fileName = "BlockStatsManager", menuName = "Stats/Block stats manager", order = 53)]
public class BlockStatsManager : UnitStatsManager<UnitHealthStats>
{
    [SerializeField]
    private UnitHealthStats _unitHealthStats;

    [SerializeField] 
    private BlockBreakable.Data _blockBreakableData;

    public override UnitHealthStats HealthStats => _unitHealthStats;

    public BlockBreakable.Data BlockBreakableData => _blockBreakableData;
}