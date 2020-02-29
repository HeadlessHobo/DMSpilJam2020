using System;
using System.Timers;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using UnityEngine.InputSystem;
using Yurowm.DebugTools;
using Timer = Plugins.Timer.Source.Timer;

namespace Gameplay.Player
{
    public class PlatformPlayerPhantom : IUpdate
    {
        private Data _data;
        private Timer _currentPhantomEnergyCycleTimer;
        
        public bool IsPhantomModeActive { get; private set; }
        public float CurrentFillProcent => _data.PhantomEnergy.CurrentProcent;

        public PlatformPlayerPhantom(Data data)
        {
            _data = data;
        }
        
        public void OnPhantomModeDown()
        {
            if (IsPhantomModeActive)
            {
                IsPhantomModeActive = false;
                _currentPhantomEnergyCycleTimer.Cancel();
            }
            else
            {
                UsedPhantomEnergy();
                PhantomEnergyCycle();
                IsPhantomModeActive = true;
            }
        }

        private void PhantomEnergyCycle()
        {
            UsedPhantomEnergy();
            _currentPhantomEnergyCycleTimer = Timer.Register(_data.PhantomCycleInterval.Value, PhantomEnergyCycle);
        }

        private void UsedPhantomEnergy()
        {
            _data.PhantomEnergy.DecreaseTempStat(_data.PhantomEnergyUsagePerCycle.Value);
        }

        public void Update()
        {
            DebugPanel.Log("PhantomEnergy", "Player", _data.PhantomEnergy.Value);
            DebugPanel.Log("PhantomEnergyProcent", "Player", _data.PhantomEnergy.CurrentProcent);
        }
        
        [Serializable]
        public class Data
        {
            public Stat PhantomEnergy;
            public Stat PhantomEnergyUsagePerCycle;
            public Stat PhantomCycleInterval;
        }
    }
}