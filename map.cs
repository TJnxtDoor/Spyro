using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Linq;
public class Map : MonoBehaviour

{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform point;
        public bool isOccupied;

        public Vector3 GetPosition()
        {
            return point.position;
        }

        public void SetPosition(Vector3 newPosition)
        {
            point.position = newPosition;
        }

        // man im dead 💀

        public void PlayerDies()
        {
            isOccupied = false;
        }

        public void PlayerSpawns()
        {
            isOccupied = true;
        }
    }

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    public Transform GetRandomSpawnPoint()
    {
        List<SpawnPoint> availablePoints = spawnPoints.FindAll(sp => !sp.isOccupied);
        if (availablePoints.Count == 0)
        {
            Debug.LogWarning("No available spawn points!");
            return null;
        }
        int randomIndex = Random.Range(0, availablePoints.Count);
        availablePoints[randomIndex].isOccupied = true;
        return availablePoints[randomIndex].point;
    }

    public Player SpawnPlayer(GameObject playerPrefab)
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        if (spawnPoint != null)
        {
            GameObject playerObj = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            Player player = playerObj.GetComponent<Player>();
            if (player != null)
            {
                player.SetSpawnPoint(spawnPoint);
            }
            return player;
        }
        return null;
    }

    public Enemy SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = GetRandomSpawnPoint();
        if (spawnPoint != null)
        {
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.SetSpawnPoint(spawnPoint);
            }
            return enemy;
        }
        return null;
    }

    MapColors mapColors;
    public MapColors MapColors => mapColors;

    // A small fix to ensure mapColors is always initialized
    void Awake()
    {
        if (mapColors == null)
        {
            mapColors = new MapColors();
        }
    }

    void UpdateColors()
    {
        //checks if map is undiscovered
        if (mapColors.isUndiscovered)
        {
            //set colors to undiscovered colors
            RenderSettings.fogColor = mapColors.undiscoveredFogColor;
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = mapColors.undiscoveredBackgroundColor;
            }
        }
        else
        {
            //set colors to discovered colors
            RenderSettings.fogColor = mapColors.discoveredFogColor;
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = mapColors.discoveredBackgroundColor;
            }
        }
    }

     

    public void DiscoverMap()
    {
        if (mapColors != null)
        {
            mapColors.isUndiscovered = false;
            UpdateColors();
        }
        else
        {
            Debug.LogWarning("MapColors is not initialized.");
        }
    }
} 

public class MapColors
{
    public bool isUndiscovered = true;
    public Color undiscoveredFogColor = Color.black;
    public Color discoveredFogColor = Color.gray;
    public Color undiscoveredBackgroundColor = Color.black;
    public Color discoveredBackgroundColor = Color;
}
