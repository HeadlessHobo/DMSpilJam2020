using System.Collections.Generic;
using System.Linq;
using Common.UnitSystem;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlatformPlayerGraphics
    {
        private Transform _graphicsTransform;
        private List<SpriteRenderer> _spriteRenderers;
        
        public PlatformPlayerGraphics(MovementSetup movementSetup)
        {
            _graphicsTransform = movementSetup.GraphicsTransform;
            _spriteRenderers = _graphicsTransform.GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        public void ChangeColor(Color newColor)
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = newColor;
            }
        }
    }
}