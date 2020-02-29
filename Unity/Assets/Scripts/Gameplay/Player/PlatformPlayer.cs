using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class PlatformPlayer : MovingUnit<PlayerConfig>, PlayerInputActions.IPlayerActions
    {
        private PlatformPlayerMovement _platformPlayerMovement;
        private PlatformPlayerPhantom _platformPlayerPhantom;
        
        [SerializeField]
        private PlayerGround.Data _playerGroundData;

        [SerializeField] 
        private PlayerConfig _playerConfig;
        
        [SerializeField]
        private MovementSetup _movementSetup;

        [SerializeField]
        private PlayerStatsManager _statsManager;

        public override UnitType UnitType => UnitType.Player;

        public PlatformPlayerPhantom PlatformPlayerPhantom => _platformPlayerPhantom;

        public MovementSetup MovementSetup => _movementSetup;
        public Vector2 Position => _movementSetup.MovementTransform.position;

        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override UnitSetup UnitSetup => _movementSetup;

        protected override IArmor Armor { get; set; }

        protected override PlayerConfig Config { get; set; }

        protected override UnitSlowManager SlowManager { get; set; }

        protected  void Start()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            StatsManager.Init();
            SetupInput();
            Config = _playerConfig;
            SlowManager = new UnitSlowManager(GetStatsManager<PlayerStatsManager>().MovementStats);
            Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _movementSetup);
            PlayerGround playerGround = new PlayerGround(_movementSetup, _playerGroundData);
            _platformPlayerMovement = new PlatformPlayerMovement(_movementSetup, _statsManager.MovementStats, playerGround);
            _platformPlayerPhantom = new PlatformPlayerPhantom(_statsManager.PlayerSpecificStats.PlayerPhantomData);
            AddLifeCycleObjects( Armor, _platformPlayerMovement, playerGround, _platformPlayerPhantom);
        }

        private void SetupInput()
        {
            PlayerInputActions playerInputActions = new PlayerInputActions();
            playerInputActions.Player.SetCallbacks(this);
            playerInputActions.Player.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _platformPlayerMovement.OnMove(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _platformPlayerMovement.OnJump();
            }
        }

        public void OnPhantomMode(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _platformPlayerPhantom.OnPhantomModeDown();
            }
        }
    }
}
