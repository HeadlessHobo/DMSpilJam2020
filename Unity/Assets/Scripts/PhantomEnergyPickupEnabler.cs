using System.Collections.Generic;
using Common.UnitSystem;
using Common.Util;
using Gameplay.Player;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace
{
    public class PhantomEnergyPickupEnabler : MonoBehaviour
    {
        [SerializeField]
        private bool _phantomModeEnabledAfterPickup;
        
        [SerializeField, ShowIf("ShowSetupSettings")] 
        private GameObject _triggerGo;
    
        [SerializeField, ShowIf("ShowSetupSettings")] 
        private GameObject _rootGo;

        private bool ShowSetupSettings()
        {
            return true;
        }
    
        private void Awake()
        {
            TriggerNotifier triggerNotifier = _triggerGo.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>() { UnitType.Player});
            triggerNotifier.UnitEntered += OnUnitEntered;
        }

        private void OnUnitEntered(UnitType unitType, IUnit unit)
        {
            PlatformPlayer platformPlayer = unit as PlatformPlayer;
            if (platformPlayer != null)
            {
                platformPlayer.PlatformPlayerPhantom.IsPhantomModeEnabled = _phantomModeEnabledAfterPickup;
            }
            Destroy(_rootGo);
        }
    }
}