using System;
using Common.UnitSystem.LifeCycle;
using Common.UnitSystem.Stats;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Common.UnitSystem
{
    public delegate void KilledUnit(IUnit unitKilled);
    
    public class UnitArmor : IArmor, IUpdate
    {
        private IUnit _ownerUnit;
        private float _nextDamageableTime;
        private bool _wasDeadInLastFrame;
        private Life _life;
        private UnitHealthStats _unitHealthStats;
        private UnitSetup _unitSetup;
        
        public event Died Died;
        public event TookDamage TookDamage;
        public event KilledUnit KilledUnit;

        public HealthFlag HealthFlags { get; }
        public bool IsDead => _life.Health.Value <= 0;
        
        public UnitArmor(IUnit ownerUnit, HealthFlag healthFlags, UnitSetup unitSetup)
        {
            _unitSetup = unitSetup;
            _ownerUnit = ownerUnit;
            HealthFlags = healthFlags;
            _unitHealthStats = ownerUnit.GetStatsManager<IUnitStatsManager>().GetStats<UnitHealthStats>();
            _life = new Life(ownerUnit, _unitHealthStats);
            _life.Died += OnDied;
            _life.TookDamage += (damage, unitDealingDamage) => TookDamage?.Invoke(damage, unitDealingDamage);
        }

        public void TakeDamage(int damage, IUnit unitDealingDamage)
        {
            if (CanTakeDamage())
            {
                _life.TakeDamage(damage, unitDealingDamage);
                MakeInvulnerable();
            }
        }

        public void Die()
        {
            _life.Die();
        }

        private void OnDied(IUnit killedBy)
        {
            Died?.Invoke(killedBy);
            killedBy?.GetArmor<IArmor>().OnKilledUnit(_ownerUnit);
            if(HealthFlags.HasFlag(HealthFlag.Destructable)){
                Object.Destroy(_unitSetup.RootGo);
            }
        }

        public void OnKilledUnit(IUnit unitKilled)
        {
            KilledUnit?.Invoke(unitKilled);   
        }

        private void MakeInvulnerable()
        {
            _nextDamageableTime = Time.time + _unitHealthStats.InvulnerabilityDuration.Value; 
        }

        public void Update()
        {
            _life.Update();
        }

        private bool CanTakeDamage()
        {
            return HealthFlags.HasFlag(HealthFlag.Killable) && _nextDamageableTime < Time.time;
        }
    }
}