// GemProgressionSystem.cs
using UnityEngine;




public class GemProgressionSystem : MonoBehaviour;
// GemProgressionSystem.cs updates
public class GemProgressionSystem : MonoBehaviour
{
    private int CalculateGemValue()
    {
        DifficultySettings diff = GameManager.Instance.difficultySettings;
        int baseValue = Mathf.RoundToInt((100 + (currentWorld - 1) * 1000) * diff.gemValueMultiplier);
        return Random.Range(baseValue, Mathf.Min(baseValue + 1000, 100000));
    }

    private void CheckWorldProgression()
    {
        int required = Mathf.RoundToInt(gemsToNextWorld *
            GameManager.Instance.difficultySettings.gemRequirementMultiplier);

        if (collectedThisWorld >= required)
        {
            // Progress to next world
        }
    }



    [Header("World Settings")]



    public int currentWorld = 1;
    public int maxWorlds = 100;
    public int gemsToNextWorld = 1000;

    public GameObject gemPrefab;
    public int minGemsPerWorld = 10;
    public int maxGemsPerWorld = 25;

    private int collectedThisWorld;

    void Start()
    {
        GenerateWorldGems();
    }

    public void AddGems(int value)
    {
        collectedThisWorld += value;
        CheckWorldProgression();
    }

    private void GenerateWorldGems()
    {
        // Clear existing gems
        foreach (Transform child in transform) Destroy(child.gameObject);

        // Generate new gems
        int gemCount = Random.Range(minGemsPerWorld, maxGemsPerWorld + 1);
        for (int i = 0; i < gemCount; i++)
        {
            GameObject gem = Instantiate(gemPrefab, GetRandomPosition(), Quaternion.identity, transform);
            ConfigureGem(gem);
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-20f, 20f),
            0.5f,
            Random.Range(-20f, 20f)
        );
    }

    private void ConfigureGem(GameObject gem)
    {
        GemCollectible gemScript = gem.GetComponent<GemCollectible>();
        gemScript.SetValue(CalculateGemValue());
    }

    private int CalculateGemValue()
    {
        int baseValue = 100 + (currentWorld - 1) * 1000;
        int maxValue = Mathf.Min(baseValue + 1000, 100000);
        return Random.Range(baseValue, maxValue + 1);
    }

    private void CheckWorldProgression()
    {
        if (collectedThisWorld >= gemsToNextWorld)
        {
            currentWorld = Mathf.Min(currentWorld + 1, maxWorlds);
            collectedThisWorld = 0;
            GenerateWorldGems();
            Debug.Log($"Entering World {currentWorld}!");
        }
    }
}

