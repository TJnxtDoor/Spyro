using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform SpawnPoint { get; private set; }

    public void SetSpawnPoint(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
    }
}

public class Enemy : MonoBehaviour
{
    public Transform SpawnPoint { get; private set; }

    public void SetSpawnPoint(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
    }
}
