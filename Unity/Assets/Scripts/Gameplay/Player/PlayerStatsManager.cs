using System;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Gameplay.Player
{
    [Serializable][CreateAssetMenu(fileName = "PlayerStatsManager", menuName = "Player stats manager", order = 52)]
    public class PlayerStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _healthStats;
        
        [SerializeField]
        private PlatformPlayerMovementStats _movementStats;

        public override UnitHealthStats HealthStats => _healthStats;

        public PlatformPlayerMovementStats MovementStats => _movementStats;

        public override void Init()
        {
            base.Init();
            AddStats(_healthStats);
            AddStats(_movementStats);
        }
    }
}