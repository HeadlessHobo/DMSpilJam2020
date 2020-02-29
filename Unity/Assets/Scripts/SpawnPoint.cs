using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private SpawnType spawnType;

    public SpawnType SpawnType => spawnType;

    public Vector2 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
}