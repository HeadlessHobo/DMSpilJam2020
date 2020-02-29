using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    [Serializable]
    public class EnemySpecificStats
    {
        [SerializeField]
        private EnemyVision.Data _enemyVisionData;

        [SerializeField] 
        private Missile.Missile.Data _missileSpawnData;

        public EnemyVision.Data EnemyVisionData => _enemyVisionData;

        public Missile.Missile.Data MissileSpawnData => _missileSpawnData;
    }
}