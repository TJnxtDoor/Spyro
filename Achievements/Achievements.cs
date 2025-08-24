using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance;

    public class Achievements
    {
        public string id;
        public string title;
        public string description;
        public AchievementType type;
        public int targetValue;
        public int currentValue;
        public bool isUnlocked;
        public int gemReward;
    }

    public enum AchievementType
    {
        TotalGemCollected,
        EnemiesDefeated,
        WorldCompleted,
        SecretsFound,
        TimeChallenge,
    }

    public List<Achievements> achievements = new List<Achievements>();
    private
       void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAchievements();
            LoadProgress();
        }
    }

    private void InitializeAchievements()
    {
        // Basic achievements
        CreateAchievement("gem_1000", "Gem Collector I", "Collect 1000 gems",
             AchievementType.TotalGemsCollected, 1000, 100);

        CreateAchievement("gem_10000", "Gem Hoarder", "Collect 10,000 gems",
            AchievementType.TotalGemsCollected, 10000, 1000);


        //Combat achievements
        CreateAchievement("enemy_50", "50 down", "Defeat 50 enemies",
            AchievementType.EnemiesDefeated, 50, 1000);

        CreateAchievement("enemy_100", "100 down", "Defeat 100 enemies",
            AchievementType.EnemiesDefeated, 100, 2500);


        //Progression achievements
        CreateAchievement("world_1", "1 Big Step", "Reach World 2",
            AchievementType.WorldCompleted, 1, 10);
        CreateAchievement("world_5", "Halfway There", "Reach World 5",
            AchievementType.WorldCompleted, 5, 500);
        CreateAchievement("world_10", "Journey's End", "Reach World 10",
            AchievementType.WorldCompleted, 10, 2000);
        //Skill achievements
        CreateAchievement("glide_100", "Sky Master", "Glide 100 times without landing",
            AchievementType.PerfectGlides, 100, 1500);



        // Secret achievements
        CreateAchievement("secret_1", "Hidden Treasure", "Find the secret cave",
            AchievementType.SecretFound, 1, 5000);
    }
}