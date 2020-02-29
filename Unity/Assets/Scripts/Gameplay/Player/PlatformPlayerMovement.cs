using System;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Yurowm.DebugTools;

namespace Gameplay.Player
{
    public class PlatformPlayerMovement : IFixedUpdate
    {
        private Vector2 _inputDirection;
        private Rigidbody2D _rigidbody2D;
        private Direction _lastDirection;
        private PlatformPlayerMovementStats _platformPlayerMovementStats;
        private PlayerGround _playerGround;
        
        public PlatformPlayerMovement(MovementSetup movementSetup, PlatformPlayerMovementStats platformPlayerMovementStats, PlayerGround playerGround)
        {
            _rigidbody2D = movementSetup.Rigidbody2D;
            _platformPlayerMovementStats = platformPlayerMovementStats;
            _playerGround = playerGround;

        }

        public void FixedUpdate()
        {
            UpdateMovingDirection();
            Move();
            DebugPanel.Log("_inputDirection", "Player", _inputDirection.ToString());
        }

        private void UpdateMovingDirection()
        {
            switch (GetMovingDirection())
            {
                case Direction.Left:
                    if (_lastDirection == Direction.Right)
                    {
                        SwitchDirection(_lastDirection);
                    }
                    break;
                case Direction.Right:
                    if (_lastDirection == Direction.Left)
                    {
                        SwitchDirection(_lastDirection);
                    }
                    break;
                case Direction.Still:
                    if (_lastDirection != Direction.Still)
                    {
                        SwitchDirection(_lastDirection);
                    }
                    break;
            }

            _lastDirection = GetMovingDirection();
        }

        private void SwitchDirection(Direction lastDirection)
        {
            var velocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = new Vector2(velocity.x * (1 - (_platformPlayerMovementStats.TurningDecreaseInProcent / 100)), velocity.y);
        }

        private void Move()
        {
            if (GetMovingDirection() != Direction.Still)
            {
                _rigidbody2D.AddForce(GetMovingDirectionAsVector2() * _platformPlayerMovementStats.Speed);   
            }
            else if (GetMovingDirection() == Direction.Still)
            {
                var velocity = _rigidbody2D.velocity;
                _rigidbody2D.velocity = new Vector2(velocity.x * (1 - (_platformPlayerMovementStats.LosingSpeedPerFrameInProcent / 100)), velocity.y);
            }
        }

        private Vector2 GetMovingDirectionAsVector2()
        {
            switch (GetMovingDirection())
            {
                case Direction.Left:
                    return Vector2.left;
                case Direction.Right:
                    return Vector2.right;
            }
            
            return Vector2.zero;
        }

        private Direction GetMovingDirection()
        {
            if (_inputDirection.x > 0)
            {
                return Direction.Right;
            }
            else if (_inputDirection.x < 0)
            {
                return Direction.Left;
            }

            return Direction.Still;
        }

        public void OnMove(Vector2 move)
        {
            _inputDirection = move;
        }

        public void OnJump()
        {
            if (_playerGround.IsGrounded())
            {
                Debug.Log("Jumped");
                _rigidbody2D.AddForce(Vector2.up * _platformPlayerMovementStats.JumpForce, ForceMode2D.Impulse);
            }
        }
    }
}