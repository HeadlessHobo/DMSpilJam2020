using System;
using System.Collections.Generic;
using System.IO;
using Common.UnitSystem;
using Common.Util;
using DefaultNamespace;
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
    private DropdownList<string> _allScenes;
    private FadeUi _fadeUi;

    [SerializeField, Dropdown("_allScenes")] 
    private string _sceneToLoad;

    private DropdownList<string> GetAllScenes()
    {
        DropdownList<string> allScenes = new DropdownList<string>();
        #if UNITY_EDITOR
        foreach (var scene in EditorBuildSettings.scenes)
        {
            allScenes.Add(Path.GetFileNameWithoutExtension(scene.path), Path.GetFileNameWithoutExtension(scene.path));
        }
        #endif

        return allScenes;
    }
    
    private void Awake()
    {
        if (Application.isPlaying)
        {
            _fadeUi = MyGameManager.Instance.FadeUi;
            _triggerNotifier = gameObject.AddComponent<TriggerNotifier>();
            _triggerNotifier.Init(new List<UnitType>(){ UnitType.Player});
            _triggerNotifier.UnitEntered += OnUnitEntered;
            _fadeUi.FadedOut += OnFadedOut;
        }
    }

    private void OnFadedOut()
    {
        SceneManager.LoadScene(_sceneToLoad);
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
        MyGameManager.CurrentLevel++;
        SoundManagerDefault.Instance.PlayPortalSound();
        _fadeUi.FadeOut("Level " + MyGameManager.CurrentLevel);
    }
}