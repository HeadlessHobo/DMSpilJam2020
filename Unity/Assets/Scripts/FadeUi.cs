using System;
using Plugins.LeanTween.Framework;
using Plugins.Timer.Source;
using UnityEngine;

namespace DefaultNamespace
{
    public class FadeUi : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _backgroundCanvasGroup;
    
        [SerializeField]
        private CanvasGroup _textCanvasGroup;

        [SerializeField] 
        private float _fadeToBlackTime;
    
        [SerializeField] 
        private float _textFadeInTime;
    
        [SerializeField] 
        private float _stayInBlackTime;

        public event Action FadedOut;

        private void Awake()
        {
            FadeIn();
        }

        public void FadeIn()
        {
            _backgroundCanvasGroup.alpha = 1;
            _backgroundCanvasGroup.LeanAlpha(0, _fadeToBlackTime);
        }

        public void FadeOut()
        {
            _backgroundCanvasGroup.LeanAlpha(1, _fadeToBlackTime);
            Timer.Register(_fadeToBlackTime, BackgroundShown);
            Timer.Register(_fadeToBlackTime + _stayInBlackTime, () => FadedOut?.Invoke());
        }
        
        private void BackgroundShown()
        {
            _textCanvasGroup.LeanAlpha(1, _textFadeInTime);
        }
    }
}