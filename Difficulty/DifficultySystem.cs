using UnityEngine;
[CreateAssetMenu(fileName = "DifficultySettings", menuName = "Game/Difficulty setttings")]
public class DifficultySettings : ScriptableObject
{
    [Header("Difficulty")]
    public DifficultyLevels currentDifficulty = DifficultyLevels.Normal;

    [Header("Player Modifiers")]
    public float playerHealthMultiplyer = 1f;
    public float playerDamagehMultiplyer = 1f;
    public float playerSpeechMultiplyer = 1f;
    public float playerDetectionMultiplyer = 1f;
    public float playerSpwanRateMultiplyer = 1f;


    [Header("Enemy Modifiers")]
    public float enemyHealthMultiplier = 1f;
    public float enemyDamageMultiplier = 1f;
    public float enemySpeedMultiplier = 1f;
    public float enemyDetectionMultiplier = 1f;
    public float enemySpawnRateMultiplier = 1f;

    [Header("Progression Modifiers")]
    public float gemValueMultiplier = 1f;
    public float gemRequirementMultiplier = 1f;


    [Header("Environmental Challanges")]
    public bool enableHazards = true;
    public float hazarDamangeMultiplier = 1f;
    public float platformStabilityMultiplier = 1f;


    public enum DifficultyLevels
    {
        Easy,
        Normal,
        Hard,
        ExtremelyDifficult
    }

    public void SetDifficulty(DifficultyLevels difficulty)
    {
        currentDifficulty = difficulty;
    }
}
