using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using Generated;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Gameplay.Player
{
    public class PlatformPlayer : MovingUnit<PlayerConfig>, PlayerInputActions.IPlayerActions
    {
        private PlatformPlayerMovement _platformPlayerMovement;
        private PlatformPlayerPhantom _platformPlayerPhantom;
        private PlayerInputActions _playerInputActions;
        
        [SerializeField]
        private PlayerGround.Data _playerGroundData;

        [SerializeField] 
        private Color _phantomColor;

        [SerializeField] 
        private PlayerConfig _playerConfig;
        
        [SerializeField]
        private MovementSetup _movementSetup;

        [SerializeField]
        private PlayerStatsManager _statsManager;

        [SerializeField] 
        private PlayerAnim _playerAnim;

        public override UnitType UnitType => UnitType.Player;

        public PlatformPlayerPhantom PlatformPlayerPhantom => _platformPlayerPhantom;

        public MovementSetup MovementSetup => _movementSetup;
        public Vector2 Position => _movementSetup.MovementTransform.position;

        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override UnitSetup UnitSetup => _movementSetup;

        protected override IArmor Armor { get; set; }

        protected override PlayerConfig Config { get; set; }

        protected override UnitSlowManager SlowManager { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            StatsManager.Init();
            SetupInput();
            Config = _playerConfig;
            SlowManager = new UnitSlowManager(GetStatsManager<PlayerStatsManager>().MovementStats);
            Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _movementSetup);
            PlayerGround playerGround = new PlayerGround(_movementSetup, _playerGroundData);
            _platformPlayerMovement =
                new PlatformPlayerMovement(_movementSetup, _statsManager.MovementStats, playerGround, _playerAnim);
            PlatformPlayerGraphics platformPlayerGraphics = new PlatformPlayerGraphics(_movementSetup);
            _platformPlayerPhantom = new PlatformPlayerPhantom(platformPlayerGraphics, _phantomColor,
                _statsManager.PlayerSpecificStats.PlayerPhantomData);
            AddLifeCycleObjects(Armor, _platformPlayerMovement, playerGround, _platformPlayerPhantom);
            Armor.Died += OnDied;
        }

        private void OnDied(IUnit killedBy)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void SetupInput()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.SetCallbacks(this);
            _playerInputActions.Player.Enable();
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

        public void OnExit(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SceneManager.LoadScene(Scenes.MENU);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _playerInputActions.Player.Disable();
        }
    }
}
