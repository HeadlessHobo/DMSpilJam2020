using System;

namespace Common.UnitSystem
{
    public interface IArmor
    {
        HealthFlag HealthFlags { get; }
        
        event Died Died;
        event TookDamage TookDamage;
        event KilledUnit KilledUnit;
        
        bool IsDead { get; }
        void AddDestroyRequirement(Func<bool> destroyRequirement);
        void TakeDamage(int damage, IUnit unitDealingDamage);
        void Die();
        void OnKilledUnit(IUnit unitKilled);
    }
}