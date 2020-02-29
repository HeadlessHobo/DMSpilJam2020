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

    public void SpawnAllWithType<T>(SpawnType spawnType) where T : MonoBehaviour
    {
        GameObject spawnPrefab = spawnTypeToPrefabMapping.GetSpawnPrefabForSpawnType(spawnType);
        foreach (var spawnPoint in _spawnPoints)
        {
            if (spawnPoint.SpawnType == spawnType)
            {
                Instantiate(spawnPrefab, spawnPoint.Position, Quaternion.identity).GetComponent<T>();
            }
        }
    }

    private Vector2 GetSpawnPositionFromSpawnType(SpawnType spawnType)
    {
        return _spawnPoints.Find(item => item.SpawnType == spawnType).Position;
    }
}