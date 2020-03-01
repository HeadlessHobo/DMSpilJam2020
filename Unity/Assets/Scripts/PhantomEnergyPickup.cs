using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.Util;
using Gameplay.Player;
using NaughtyAttributes;
using UnityEngine;

public class PhantomEnergyPickup : MonoBehaviour
{
    [SerializeField] 
    private float _energyToAddOnPickup;
    
    [SerializeField, ShowIf("ShowSetupSettings")] 
    private GameObject _triggerGo;
    
    [SerializeField, ShowIf("ShowSetupSettings")] 
    private GameObject _rootGo;

    private bool ShowSetupSettings()
    {
        return false;
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
            platformPlayer.PlatformPlayerPhantom.AddPhantomEnergy(_energyToAddOnPickup);
        }
        Destroy(_rootGo);
    }
}