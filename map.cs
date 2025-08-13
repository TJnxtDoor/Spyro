using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    [system.Serializable]
    public class SpawnPoint
    {
        public Transform point;
        public bool isOccupied;

        public vector3 GetPosition()
        {
            return point.position;
        }

        [Header("Spawn Points")]
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
            return availablePoints[randomIndex].point;
        }

        public player SpawnPlayer(GameObject playerPrefab)
        {
            Transform spawnPoint = GetRandomSpawnPoint();
            if (spawnPoint != null)
            {
                GameObject playerObj = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
                return playerObj.GetComponent<player>();
            }
            return null;
        }

        public Enemy SpawnEnemy(GameObject enemyPrefab)
        {
            Transform spawnPoint = GetRandomSpawnPoint();
            if (spawnPoint != null)
            {
                GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                return enemyObj.GetComponent<Enemy>();
            }
            return null;
            return null;
        }

    }

    map_colors mapColors;
    public MapColors MapColors => mapColors;
        updateColors();

    void updateColors()
{
        if (mapColors == null)
        {
            mapColors = new MapColors();
        }

        //checks if map is undiscovered
        if (mapColors.isUndiscovered)
        {
            //set colors to undiscovered colors
            RenderSettings.fogColor = mapColors.undiscoveredFogColor;
            Camera.main.backgroundColor = mapColors.undiscoveredBackgroundColor;
        }
        else
        {
            //set colors to discovered colors
            RenderSettings.fogColor = mapColors.discoveredFogColor;
            Camera.main.backgroundColor = mapColors.discoveredBackgroundColor;
        }
    }
}