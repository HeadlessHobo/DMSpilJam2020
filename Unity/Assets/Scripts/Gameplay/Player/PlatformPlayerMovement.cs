using System;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Gamelogic.Extensions;
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
        private PlayerAnim _playerAnim;
        private Transform _graphicsTransform;
        private float _originalGraphicsScaleX;
        
        public PlatformPlayerMovement(MovementSetup movementSetup, PlatformPlayerMovementStats platformPlayerMovementStats, PlayerGround playerGround,
             PlayerAnim playerAnim)
        {
            _rigidbody2D = movementSetup.Rigidbody2D;
            _platformPlayerMovementStats = platformPlayerMovementStats;
            _playerGround = playerGround;
            _playerAnim = playerAnim;
            _graphicsTransform = movementSetup.GraphicsTransform;
            _originalGraphicsScaleX = _graphicsTransform.localScale.x;
            _playerGround.HitGround += OnHitGround;
        }

        private void OnHitGround()
        {
            _playerAnim.AnimPlayerIdle();
        }

        public void FixedUpdate()
        {
            UpdateMovingDirection();
            Move();
            UpdateAnimator();
            DebugPanel.Log("_inputDirection", "Player", _inputDirection.ToString());
        }

        private void UpdateAnimator()
        {
            _playerAnim.SetSpeed(Mathf.Abs(_rigidbody2D.velocity.x));
            if (GetMovingDirection() == Direction.Left)
            {
                _graphicsTransform.SetScaleX(-_originalGraphicsScaleX);
            }
            else if(GetMovingDirection() == Direction.Right)
            {
                _graphicsTransform.SetScaleX(_originalGraphicsScaleX);
            }
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
                _rigidbody2D.velocity = new Vector2(GetMovingDirectionAsVector2().x * _platformPlayerMovementStats.Speed, _rigidbody2D.velocity.y);
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
                _playerAnim.AnimPlayerJump();
                _rigidbody2D.AddForce(Vector2.up * _platformPlayerMovementStats.JumpForce, ForceMode2D.Impulse);
            }
        }
    }
}