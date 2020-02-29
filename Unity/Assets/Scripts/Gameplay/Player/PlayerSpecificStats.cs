using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Gameplay.Player
{
    [Serializable]
    public class PlayerSpecificStats : IResetStats
    {
        [SerializeField] 
        private PlatformPlayerPhantom.Data _playerPhantomData;

        public PlatformPlayerPhantom.Data PlayerPhantomData => _playerPhantomData;
        
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}