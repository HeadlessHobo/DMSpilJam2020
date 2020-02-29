using System;
using DefaultNamespace;
using Gamelogic.Extensions;
using Gameplay.Enemy;
using Gameplay.Player;
using NaughtyAttributes;
using UnityEngine;

public class MyGameManager : Singleton<MyGameManager>
{
    private FadeUi _fadeUi;
    
    [SerializeField]
    private bool _phantomModeEnabled;

    public PlatformPlayer PlatformPlayer { get; private set; }

    public FadeUi FadeUi
    {
        get
        {
            if (_fadeUi == null)
            {
                _fadeUi = FindObjectOfType<FadeUi>();
            }

            return _fadeUi;
        }
    }

    public void Start()
    {
        if (Application.isPlaying)
        {
            SpawnManager.Instance.Spawn<PlatformPlayer>(SpawnType.Player, OnPlayerSpawned);
            SpawnManager.Instance.SpawnAllWithType<Enemy>(SpawnType.Enemy);
        }
    }

    private void OnPlayerSpawned(PlatformPlayer player)
    {
        PlatformPlayer = player;
        PlatformPlayer.PlatformPlayerPhantom.IsPhantomModeEnabled = _phantomModeEnabled;
    }
}