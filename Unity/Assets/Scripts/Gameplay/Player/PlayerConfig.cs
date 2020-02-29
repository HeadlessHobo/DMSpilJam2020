using System;
using Common.UnitSystem;
using UnityEngine;

namespace Gameplay.Player
{
    [Serializable]
    public class PlayerConfig : UnitConfig
    {
        [SerializeField] 
        private string _name;
    
        public string Name => _name;
    }
}