namespace Common.UnitSystem
{
    public interface IArmor
    {
        HealthFlag HealthFlags { get; }
        bool IsDead { get; }
        void TakeDamage(int damage, IUnit unitDealingDamage);
        void OnKilledUnit(IUnit unitKilled);
    }
}