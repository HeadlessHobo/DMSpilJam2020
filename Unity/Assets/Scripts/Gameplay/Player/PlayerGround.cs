using System;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using UnityEngine;
using Yurowm.DebugTools;

namespace Gameplay.Player
{
    public class PlayerGround : IUpdate
    {
        private MovementSetup _movementSetup;
        private Data _data;
        private bool _wasInTheAirLastFrame;

        public event Action HitGround;
        
        public PlayerGround(MovementSetup movementSetup, Data data)
        {
            _movementSetup = movementSetup;
            _data = data;
        }
        
        public void Update()
        {
            DebugPanel.Log("Grounded", "Player", IsGrounded());
            if (_wasInTheAirLastFrame && IsGrounded())
            {
                HitGround?.Invoke();
            }
            
            _wasInTheAirLastFrame = !IsGrounded();
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapArea(GetTopLeft(), GetBottomRight(), _data.Layers);
        }

        private Vector2 GetTopLeft()
        {
            return _movementSetup.MovementTransform.position + (Vector3)_data.Collider.offset - _data.Collider.bounds.extents;
        }
        
        private Vector2 GetBottomRight()
        {
            return _movementSetup.MovementTransform.position + (Vector3)_data.Collider.offset + _data.Collider.bounds.extents;
        }
        
        [Serializable]
        public class Data
        {
            public Collider2D Collider;
            public LayerMask Layers;
        }
    }
}