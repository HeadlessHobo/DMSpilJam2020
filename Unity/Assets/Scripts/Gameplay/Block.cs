using Common.UnitSystem;
using Common.UnitSystem.Stats;
using NaughtyAttributes;
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

        [SerializeField, ShowIf("IsBreakable")] 
        private Rigidbody2D[] _breakableParts;
        
        [SerializeField, ShowIf("IsBreakable")] 
        private Transform _centerTransform;
        
        public override UnitType UnitType => UnitType.Block;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _unitSetup;

        [Button("FindBreakableParts")]
        private void FindBreakableParts()
        {
            _breakableParts = _unitSetup.RootGo.GetComponentsInChildren<Rigidbody2D>();
        }

        private bool IsBreakable()
        {
            return _healthFlags.HasFlag(HealthFlag.Destructable) && _healthFlags.HasFlag(HealthFlag.Killable);
        }

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Armor = new UnitArmor(this, _healthFlags, UnitSetup);
            AddLifeCycleObject(Armor);
            AddBreakableBlockFunctionalityIfNeeded();
        }

        private void AddBreakableBlockFunctionalityIfNeeded()
        {
            BlockBreakable blockBreakable = new BlockBreakable(Armor, _statsManager.BlockBreakableData, _centerTransform, _breakableParts);
        }
    }
}