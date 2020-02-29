using System;
using System.Collections.Generic;
using System.IO;
using Common.UnitSystem;
using Common.Util;
using NaughtyAttributes;
using Plugins.LeanTween.Framework;
using Plugins.Timer.Source;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class WinTrigger : MonoBehaviour
{
    private TriggerNotifier _triggerNotifier;
    
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
    
    private DropdownList<string> _allScenes;

    [SerializeField, Dropdown("_allScenes")] 
    private string _sceneToLoad;

    private DropdownList<string> GetAllScenes()
    {
        DropdownList<string> allScenes = new DropdownList<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            allScenes.Add(Path.GetFileNameWithoutExtension(scene.path), Path.GetFileNameWithoutExtension(scene.path));
        }

        return allScenes;
    }
    
    private void Awake()
    {
        if (Application.isPlaying)
        {
            _triggerNotifier = gameObject.AddComponent<TriggerNotifier>();
            _triggerNotifier.Init(new List<UnitType>(){ UnitType.Player});
            _triggerNotifier.UnitEntered += OnUnitEntered;
            _backgroundCanvasGroup.alpha = 1;
            _backgroundCanvasGroup.LeanAlpha(0, _fadeToBlackTime);
        }
    }
    
    private void Update()
    {
        if (Application.isEditor)
        {
            _allScenes = GetAllScenes();
        }
    }

    private void OnUnitEntered(UnitType unitType, IUnit unit)
    {
        _backgroundCanvasGroup.LeanAlpha(1, _fadeToBlackTime);
        Timer.Register(_fadeToBlackTime, BackgroundShown);
        Timer.Register(_fadeToBlackTime + _stayInBlackTime, () => SceneManager.LoadScene(_sceneToLoad));
    }

    private void BackgroundShown()
    {
        _textCanvasGroup.LeanAlpha(1, _textFadeInTime);
    }
}