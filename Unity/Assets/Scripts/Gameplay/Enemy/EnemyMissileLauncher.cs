using System;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Common.UnitSystem.Stats;
using Gameplay.Player;
using Plugins.Timer.Source;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyMissileLauncher : IOnDestroy
    {
        private UnitAttackStats _unitAttackStats;
        private bool _canAttack;
        private Timer _attackTimer;
        private Missile.Missile.Data _missileData;
        private IUnit _enemy;
        private EnemyVision _enemyVision;
        private MissileLaunchData _missileLaunchData;
        private MovementSetup _movementSetup;
        private EnemyAnim _enemyAnim;
        
        public EnemyMissileLauncher(EnemyVision enemyVision, MovementSetup movementSetup, UnitAttackStats unitAttackStats,
            Missile.Missile.Data missileData, MissileLaunchData missileLaunchData, IUnit enemy, EnemyAnim enemyAnim)
        {
            _enemyVision = enemyVision;
            _enemy = enemy;
            _missileLaunchData = missileLaunchData;
            _movementSetup = movementSetup;
            enemyVision.PlayerEnteredVision += OnPlayerEnteredVision;
            enemyVision.PlayerExitedVision += OnPlayerExitedVision;
            _unitAttackStats = unitAttackStats;
            _missileData = missileData;
            _enemyAnim = enemyAnim;
            _canAttack = true;
        }

        private void OnPlayerEnteredVision(PlatformPlayer player)
        {
            if (_canAttack)
            {
                LaunchMissile();
            }
            else
            {
                _attackTimer = Timer.Register(_unitAttackStats.AttackSpeed, LaunchMissile);
            }
        }

        private void LaunchMissile()
        {
            _missileData.Owner = _enemy;
            _missileData.MissileDirection = _enemyVision.ForwardDirection();
            _enemyAnim.AnimShoot();
            Spawner.Spawn(_missileLaunchData.MissilePrefab, _missileLaunchData.SpawnPoint.position,
                _movementSetup.MovementTransform.eulerAngles, _missileData);
            _attackTimer = Timer.Register(_unitAttackStats.AttackSpeed, LaunchMissile);
        }
        
        private void OnPlayerExitedVision(PlatformPlayer player)
        {
            _attackTimer.Cancel();
        }
        
        public void OnDestroy()
        {
            _attackTimer.Cancel();
        }

        [Serializable]
        public class  MissileLaunchData
        {
            public Missile.Missile MissilePrefab;
            public Transform SpawnPoint;
        }
    }
}