using System;
using System.Timers;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using Yurowm.DebugTools;
using Timer = Plugins.Timer.Source.Timer;

namespace Gameplay.Player
{
    public class PlatformPlayerPhantom : IUpdate
    {
        private Data _data;
        private Timer _currentPhantomEnergyCycleTimer;
        private PlatformPlayerGraphics _platformPlayerGraphics;
        private Color _phantomColor;
        
        public bool IsPhantomModeActive { get; private set; }
        public bool IsPhantomModeEnabled { get; set; }
        public bool IsLockedOut { get; private set; }
        public float CurrentFillProcent => _data.PhantomEnergy.CurrentProcent;

        public PlatformPlayerPhantom(PlatformPlayerGraphics platformPlayerGraphics, Color phantomColor, Data data)
        {
            _platformPlayerGraphics = platformPlayerGraphics;
            _phantomColor = phantomColor;
            _data = data;
            IsLockedOut = true;
            Timer.Register(_data.PhantomLockout.Value, () => IsLockedOut = false);
        }
        
        public void OnPhantomModeDown()
        {
            if (IsPhantomModeEnabled && !IsLockedOut)
            {
                if (IsPhantomModeActive)
                {
                    DeactivatePhantomMode();
                }
                else if(_data.PhantomEnergy.Value > 0)
                {
                    ActivatePhantomMode();
                }
            }
        }

        private void ActivatePhantomMode()
        {
            UsedPhantomEnergy();
            PhantomEnergyCycle();
            IsPhantomModeActive = true;
            _platformPlayerGraphics.ChangeColor(_phantomColor);
            SoundManagerDefault.Instance.PlayEntPhantSound();
        }

        private void DeactivatePhantomMode()
        {
            IsPhantomModeActive = false;
            _currentPhantomEnergyCycleTimer.Cancel();
            _currentPhantomEnergyCycleTimer.Cancel();
            _platformPlayerGraphics.ChangeColor(Color.white);
            SoundManagerDefault.Instance.PlayExitPhantSound();
        }

        private void PhantomEnergyCycle()
        {
            if (_data.PhantomEnergy.Value > 0)
            {
                UsedPhantomEnergy();
                _currentPhantomEnergyCycleTimer = Timer.Register(_data.PhantomCycleInterval.Value, PhantomEnergyCycle);
            }
        }

        private void UsedPhantomEnergy()
        {
            if (_data.PhantomEnergy.Value > 0 && !DebugShortcuts.UnlimitedPhantomModeActivated)
            {
                _data.PhantomEnergy.DecreaseTempStat(_data.PhantomEnergyUsagePerCycle.Value);
            }
        }

        public void AddPhantomEnergy(float phantomEnergyToAdd)
        {
            _data.PhantomEnergy.IncreaseTempStat(phantomEnergyToAdd);
        }

        public void Update()
        {
            if (_data.PhantomEnergy.Value <= 0)
            {
                DeactivatePhantomMode();
            }

            DebugPanel.Log("PhantomEnergy", "Player", _data.PhantomEnergy.Value);
            DebugPanel.Log("PhantomEnergyProcent", "Player", _data.PhantomEnergy.CurrentProcent);
        }
        
        [Serializable]
        public class Data
        {
            public Stat PhantomEnergy;
            public Stat PhantomEnergyUsagePerCycle;
            public Stat PhantomCycleInterval;
            public Stat PhantomLockout;
        }
    }
}