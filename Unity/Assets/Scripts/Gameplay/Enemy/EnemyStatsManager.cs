using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Gameplay.Enemy
{
    [Serializable][CreateAssetMenu(fileName = "EnemyStatsManager", menuName = "Stats/Enemy stats manager", order = 52)]
    public class EnemyStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _unitHealthStats;
        
        [SerializeField]
        private UnitMovementStats _movementStats;

        [SerializeField] 
        private UnitAttackStats _unitAttackStats;

        [SerializeField] 
        private EnemySpecificStats _enemySpecificStats;

        public override UnitHealthStats HealthStats => _unitHealthStats;

        public UnitMovementStats MovementStats => _movementStats;

        public EnemySpecificStats EnemySpecificStats => _enemySpecificStats;

        public UnitAttackStats UnitAttackStats => _unitAttackStats;
    }
}