using System;
using Common.UnitSystem;
using Common.UnitSystem.ExamplePlayer.Stats;
using UnityEngine;

namespace Gameplay.Player
{
    [Serializable]
    public class PlatformPlayerMovementStats : MovementStats
    {
        [SerializeField]
        private Stat _turningDecreaseInProcent;
        
        [SerializeField]
        private Stat _losingSpeedPerFrameInProcent;

        [SerializeField]
        private Stat _jumpForce;

        public float TurningDecreaseInProcent => _turningDecreaseInProcent.Value;

        public float LosingSpeedPerFrameInProcent => _losingSpeedPerFrameInProcent.Value;

        public float JumpForce => _jumpForce.Value;
    }
}