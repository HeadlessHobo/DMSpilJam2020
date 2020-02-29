﻿using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class Enemy : MovingUnit<EnemyConfig>
    {
        [SerializeField]
        private EnemyStatsManager _statsManager;

        [SerializeField] 
        private MovementSetup _movementSetup;

        [SerializeField] 
        private EnemyConfig _enemyConfig;
        
        [SerializeField] 
        private EnemyMissileLauncher.MissileLaunchData _missileLaunchData;

        [SerializeField]
        private EnemyAnim _enemyAnim;

        public override UnitType UnitType => UnitType.Enemy;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _movementSetup;
        protected override EnemyConfig Config { get; set; }
        protected override UnitSlowManager SlowManager { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Config = _enemyConfig;
            SlowManager = new UnitSlowManager(_statsManager.MovementStats);
            Armor = new UnitArmor(this, HealthFlag.Killable | HealthFlag.Destructable, _movementSetup);
            EnemyVision enemyVision = new EnemyVision(_movementSetup, _statsManager.EnemySpecificStats.EnemyVisionData, Vector2.left);
            EnemyMissileLauncher enemyMissileLauncher = new EnemyMissileLauncher(enemyVision, _movementSetup,  _statsManager.UnitAttackStats, 
                _statsManager.EnemySpecificStats.MissileSpawnData,
                _missileLaunchData,
                this, _enemyAnim);
            AddLifeCycleObjects(Armor, enemyVision, enemyMissileLauncher);
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DebugExtension.DebugCone(_movementSetup.MovementTransform.position, Vector2.left * 10, Color.green, 
                _statsManager.EnemySpecificStats.EnemyVisionData.ConeInDegrees.Value, 0, false);
        }
    }
}