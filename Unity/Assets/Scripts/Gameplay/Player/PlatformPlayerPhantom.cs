using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class PlatformPlayerPhantom
    {
        public bool IsPhantomModeActive { get; private set; }

        public void OnPhantomModeDown(InputAction.CallbackContext context)
        {
            IsPhantomModeActive = true;
        }
        
        public void OnPhantomModeUp(InputAction.CallbackContext context)
        {
            IsPhantomModeActive = false;
        }
    }
}