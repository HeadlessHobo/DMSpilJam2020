using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.Util;
using Plugins.LeanTween.Framework;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    private Vector3 _startScale;
    
    [SerializeField]
    private Transform _scaleTransformOnEnter;

    [SerializeField] 
    private Vector3 _scaleTarget;

    [SerializeField] 
    private float _scaleTime;

    private void Awake()
    {
        _startScale = _scaleTransformOnEnter.lossyScale;
        TriggerNotifier triggerNotifier = gameObject.AddComponent<TriggerNotifier>();
        triggerNotifier.Init(new List<UnitType>(){ UnitType.Player});
        triggerNotifier.UnitEntered += OnUnitEntered;
        triggerNotifier.UnitExited += OnUnitExited;
    }

    private void OnUnitEntered(UnitType unitType, IUnit unit)
    {
        _scaleTransformOnEnter.LeanScale(_scaleTarget, _scaleTime);
    }
    
    private void OnUnitExited(UnitType unitType, IUnit unit)
    {
        _scaleTransformOnEnter.LeanScale(_startScale, _scaleTime);
    }
}