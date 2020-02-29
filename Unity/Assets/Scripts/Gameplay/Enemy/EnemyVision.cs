using System;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Gameplay.Player;
using UnityEngine;
using Yurowm.DebugTools;

namespace Gameplay.Enemy
{
    public delegate void PlayerEnteredVision(PlatformPlayer player);
    public delegate void PlayerExitedVision(PlatformPlayer player);
    
    public class EnemyVision : IUpdate
    {
        private Transform _movementTransform;
        private Vector2 _startLookDirection;
        private Data _data;
        private bool _wasPlayerWithinVisionConeLastFrame;
        private bool _canSeePlayer;
        private LayerMask _visionLayermask;

        public event PlayerEnteredVision PlayerEnteredVision;
        public event PlayerExitedVision PlayerExitedVision;
        
        public EnemyVision(MovementSetup movementSetup, Data data, Vector2 startLookDirection, LayerMask visionLayermask)
        {
            _visionLayermask = visionLayermask;
            _movementTransform = movementSetup.MovementTransform;
            _data = data;
            _startLookDirection = startLookDirection;
        }
        
        public void Update()
        {
            PlatformPlayer platformPlayer = GameManager.Instance.PlatformPlayer;
            _canSeePlayer = CanSeePlayer(platformPlayer);
            
            if (!_wasPlayerWithinVisionConeLastFrame && _canSeePlayer)
            {
                PlayerEnteredVision?.Invoke(platformPlayer);
            }
            else if(_wasPlayerWithinVisionConeLastFrame && !_canSeePlayer)
            {
                PlayerExitedVision?.Invoke(platformPlayer);
            }

            _wasPlayerWithinVisionConeLastFrame = _canSeePlayer;
            DebugPanel.Log("CanSeePlayer", "Enemy", _canSeePlayer);
        }

        private bool CanSeePlayer(PlatformPlayer platformPlayer)
        {
            return IsPlayerWithinVisionCone(platformPlayer) &&
                   IsNoObstructionToLineOfSightToPlayer(platformPlayer) &&
                   !platformPlayer.PlatformPlayerPhantom.IsPhantomModeActive;
        }

        private bool IsNoObstructionToLineOfSightToPlayer(PlatformPlayer platformPlayer)
        {
            RaycastHit2D hit = Physics2D.Raycast(_movementTransform.position, DirectionToPlayer(platformPlayer), int.MaxValue, _visionLayermask);
            return hit.transform != null && hit.transform.GetComponentInParent<PlatformPlayer>() != null;
        }

        private bool IsPlayerWithinVisionCone(PlatformPlayer platformPlayer)
        {
            float degreeBetweenForwardDirectionAndPlayer = Vector2.Angle(ForwardDirection(), DirectionToPlayer(platformPlayer));

            return degreeBetweenForwardDirectionAndPlayer <= _data.ConeInDegrees.Value;
        }

        private Vector2 DirectionToPlayer(PlatformPlayer platformPlayer)
        {
            return (platformPlayer.Position - (Vector2)_movementTransform.position).normalized;
        }

        public Vector2 ForwardDirection()
        {
            return _movementTransform.rotation * _startLookDirection;
        }
        
        [Serializable]
        public class Data
        {
            public Stat ConeInDegrees;
        }
    }
}