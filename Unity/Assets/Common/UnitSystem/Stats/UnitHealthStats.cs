using System;
using UnityEngine;

namespace Common.UnitSystem.Stats
{
    [Serializable]
    public class UnitHealthStats : IResetStats
    {
        [SerializeField] 
        private Stat _healthStat;

        [SerializeField] 
        private Stat _invulnerabilityDuration;

        public Stat InvulnerabilityDuration => _invulnerabilityDuration;

        public Stat HealthStat => _healthStat;

        public void IncreaseHealth(int health)
        {
            _healthStat.IncreaseStat(health);
        }

        public virtual void Reset()
        {
            _healthStat.ResetTempStats();
        }
    }
}