using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class DeathTrigger : MonoBehaviour
    {
        private void Awake()
        {
            TriggerNotifier triggerNotifier = gameObject.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>() { UnitType.Player});
            triggerNotifier.UnitEntered += OnUnitEntered;
        }

        private void OnUnitEntered(UnitType unitType, IUnit unit)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}