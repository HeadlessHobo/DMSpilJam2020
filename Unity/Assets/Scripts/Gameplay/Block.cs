using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Gameplay
{
    public class Block : Unit
    {
        [SerializeField]
        private BlockStatsManager _statsManager;

        [SerializeField] 
        private HealthFlag _healthFlags;

        [SerializeField] 
        private UnitSetup _unitSetup;
        
        public override UnitType UnitType => UnitType.Block;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _unitSetup;

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Armor = new UnitArmor(this, _healthFlags, UnitSetup);
        }
    }
}