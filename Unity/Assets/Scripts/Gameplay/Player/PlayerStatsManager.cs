using System;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Gameplay.Player
{
    [Serializable][CreateAssetMenu(fileName = "PlayerStatsManager", menuName = "Stats/Player stats manager", order = 52)]
    public class PlayerStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _healthStats;
        
        [SerializeField]
        private PlatformPlayerMovementStats _movementStats;
        
        [SerializeField] 
        private PlayerSpecificStats _playerSpecificStats;

        public override UnitHealthStats HealthStats => _healthStats;

        public PlatformPlayerMovementStats MovementStats => _movementStats;

        public PlayerSpecificStats PlayerSpecificStats => _playerSpecificStats;

        public override void Init()
        {
            base.Init();
            AddStats(_healthStats);
            AddStats(_movementStats);
        }
    }
}