using System;
using Common.UnitSystem;
using Plugins.LeanTween.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay
{
    public class BlockBreakable
    {
        private Rigidbody2D[] _breakableParts;
        private bool _hasBlockBrokenApart;
        private Transform _centerTransform;
        private Data _data;
        
        public BlockBreakable(IArmor armor, Data data, Transform centerTransform, Rigidbody2D[] breakableParts)
        {
            _breakableParts = breakableParts;
            _data = data;
            _centerTransform = centerTransform;
            armor.Died += ArmorOnDied;
            armor.AddDestroyRequirement(() => _hasBlockBrokenApart);
        }

        private void ArmorOnDied(IUnit killedBy)
        {
            SoundManagerDefault.Instance.PlayBlocksExplodeSound();
            foreach (var breakablePart in _breakableParts)
            {
                breakablePart.simulated = true;
                breakablePart.AddExplosionForce(_data.ExplosionForce.Value, _centerTransform.position, _data.ExplosionRadius.Value, 1);
                breakablePart.transform.parent = null;
                breakablePart.gameObject.LeanAlpha(0, _data.ExplosionLiveTime.Value);
                Object.Destroy(breakablePart, _data.ExplosionLiveTime.Value);
            }

            _hasBlockBrokenApart = true;
        }
        
        [Serializable]
        public class Data
        {
            public Stat ExplosionForce;
            public Stat ExplosionRadius;
            public Stat ExplosionLiveTime;
        }
    }
}