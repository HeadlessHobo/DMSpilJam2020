using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Movement
{
    public class PlayerMovement : Movement
    {
        private PlayerInputActions _playerInputActions;
        private Vector2 _moveDirection;
        
        public PlayerMovement(MovementSetup movementSetup, MovementStats movementStats, PlayerInputActions playerInputActions) : base(movementSetup, movementStats)
        {
            _playerInputActions = playerInputActions;
        }


        protected override Vector2 GetMoveDirection()
        {
            //_playerInput.actions[_playerInputActions.Player.Move.name].ReadValue<Vector2>()
            return _moveDirection;
        }
    }
}