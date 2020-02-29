using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Gameplay.Player
{
    [Serializable]
    public class PlayerSpecificStats : IResetStats
    {
        [SerializeField]
        private PlayerGround.Data _playerGroundData;

        public PlayerGround.Data PlayerGroundData => _playerGroundData;
        
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}