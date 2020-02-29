using System;
using Gamelogic.Extensions;
using Gameplay.Enemy;
using Gameplay.Player;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlatformPlayer PlatformPlayer { get; private set; }

    public void Start()
    {
        SpawnManager.Instance.Spawn<PlatformPlayer>(SpawnType.Player, OnPlayerSpawned);
        SpawnManager.Instance.Spawn<Enemy>(SpawnType.Enemy);
    }

    private void OnPlayerSpawned(PlatformPlayer player)
    {
        PlatformPlayer = player;
    }
}