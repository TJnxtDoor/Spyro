using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GemProgressionSystem : MonoBehaviour
{
    public static GemProgressionSystem Instance { get; private set; }

    [Header("World Settings")]
    public int currentWorld = 1;
    private int maxWorlds = 100;
    public int gemsToNextWorld = 1000;

    private GameObject gemPrefab;
    private int minGemsPerWorld = 10;
    private int maxGemsPerWorld = 25;

    public int collectedThisWorld;

    private void Awake()
    {
        Instance = this;
    }

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
        float valueMultiplier = GameManager.Instance != null && GameManager.Instance.difficultySettings != null
            ? GameManager.Instance.difficultySettings.gemValueMultiplier
            : 1f;
        int baseValue = Mathf.RoundToInt((100 + (currentWorld - 1) * 1000) * valueMultiplier);
        int maxValue = Mathf.Min(baseValue + 1000, 100000);
        return Random.Range(baseValue, maxValue + 1);
    }

    private void CheckWorldProgression()
    {
        float requirementMultiplier = GameManager.Instance != null && GameManager.Instance.difficultySettings != null
            ? GameManager.Instance.difficultySettings.gemRequirementMultiplier
            : 1f;
        int required = Mathf.RoundToInt(gemsToNextWorld * requirementMultiplier);

        if (collectedThisWorld >= required)
        {
            currentWorld = Mathf.Min(currentWorld + 1, maxWorlds);
            collectedThisWorld = 0;
            GenerateWorldGems();
            Debug.Log($"Entering World {currentWorld}!");
        }
    }
}

