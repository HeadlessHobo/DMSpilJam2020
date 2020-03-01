using System;
using UnityEngine;
using Yurowm.DebugTools;

namespace DefaultNamespace
{
    public class DebugShortcuts : MonoBehaviour
    {
        public static bool UnlimitedPhantomModeActivated;
        
        private void Awake()
        {
            UnlimitedPower();
        }
        
        private void UnlimitedPower() {
            DebugPanel.AddDelegate("Unlimited phantom mode", () => UnlimitedPhantomModeActivated = true);
        }
    }
}