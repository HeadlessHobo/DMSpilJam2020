using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Plugins.Timer.Source;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class Enemy : MovingUnit<EnemyConfig>
    {
        private bool _deathAnimationPlayed;
        
        [SerializeField]
        private EnemyStatsManager _statsManager;

        [SerializeField] 
        private MovementSetup _movementSetup;

        [SerializeField] 
        private EnemyConfig _enemyConfig;
        
        [SerializeField] 
        private EnemyMissileLauncher.MissileLaunchData _missileLaunchData;

        [SerializeField] 
        private LayerMask _visionLayermask;

        [SerializeField]
        private EnemyAnim _enemyAnim;

        [SerializeField] 
        private Transform _enemyVisionStartTransform;

        [SerializeField]
        private float _deathAnimationDuration;

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
            Armor = new UnitArmor(this, HealthFlag.Killable | HealthFlag.Destructable, _movementSetup, () => _deathAnimationPlayed);
            Armor.Died += OnDied;
            EnemyVision enemyVision = new EnemyVision(_movementSetup, _enemyVisionStartTransform, _statsManager.EnemySpecificStats.EnemyVisionData, Vector2.left, _visionLayermask);
            EnemyMissileLauncher enemyMissileLauncher = new EnemyMissileLauncher(enemyVision, _movementSetup,  _statsManager.UnitAttackStats, 
                _statsManager.EnemySpecificStats.MissileSpawnData,
                _missileLaunchData,
                this, _enemyAnim);
            AddLifeCycleObjects(Armor, enemyVision, enemyMissileLauncher);
        }

        private void OnDied(IUnit killedBy)
        {
            _enemyAnim.AnimEnDeath();
            SoundManagerDefault.Instance.PlayMonsterDeathSound();
            Timer.Register(_deathAnimationDuration, () => _deathAnimationPlayed = true);
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DebugExtension.DebugCone(_enemyVisionStartTransform.position, (_movementSetup.MovementTransform.rotation * Vector2.left) * 10, Color.green, 
                _statsManager.EnemySpecificStats.EnemyVisionData.ConeInDegrees.Value, 0, false);
        }
    }
}