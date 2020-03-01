using System;
using DefaultNamespace;
using Gamelogic.Extensions;
using Gameplay.Enemy;
using Gameplay.Player;
using Generated;
using NaughtyAttributes;
using UnityEngine;

public class MyGameManager : Singleton<MyGameManager>
{
    private FadeUi _fadeUi;
    private GameObject _musicGo;

    public static int CurrentLevel = 1;
    
    [SerializeField]
    private bool _phantomModeEnabled;

    [SerializeField] 
    private GameObject _musicPrefab;

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

    private void Awake()
    {
        CreateMusicGoIfNotExists();
    }

    private void CreateMusicGoIfNotExists()
    {
        GameObject musicGo = GameObject.FindWithTag(Tags.MUSIC);
        if (musicGo == null)
        {
            musicGo = Instantiate(_musicPrefab);
            DontDestroyOnLoad(musicGo);
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