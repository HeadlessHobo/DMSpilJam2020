using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private SpawnTypeToPrefabMapping spawnTypeToPrefabMapping;

    private List<SpawnPoint> _spawnPoints;

    private void Awake()
    {
        _spawnPoints = FindObjectsOfType<SpawnPoint>().ToList();
    }

    public void Spawn<T>(SpawnType spawnType, Action<T> onSpawned = null) where T : MonoBehaviour
    {
        GameObject spawnPrefab = spawnTypeToPrefabMapping.GetSpawnPrefabForSpawnType(spawnType);

        T spawnedObject = Instantiate(spawnPrefab, GetSpawnPositionFromSpawnType(spawnType), Quaternion.identity).GetComponent<T>();
        
        onSpawned?.Invoke(spawnedObject);
    }

    private Vector2 GetSpawnPositionFromSpawnType(SpawnType spawnType)
    {
        return _spawnPoints.Find(item => item.SpawnType == spawnType).Position;
    }
}