// 100PercentAchievements.cs
using UnityEngine;

public class CompletionTracker : MonoBehaviour
{
    // Tracking Properties
    public int TotalWorlds => WorldManager.Instance.worlds.Count;
    public int WorldsCompleted { get; private set; }
    public int TotalGems => WorldManager.Instance.TotalGemsInGame;
    public int GemsCollected { get; private set; }
    public int TotalDragons => DragonStatueManager.Instance.TotalStatues;
    public int DragonsFreed { get; private set; }
    public int TotalEggs => EggManager.Instance.TotalEggs;
    public int EggsCollected { get; private set; }
    public int TotalSkills => SkillMastery.Instance.TotalSkills;
    public int SkillsMastered { get; private set; }

    // Completion Flags
    public bool AllWorlds100Percent => WorldsCompleted == TotalWorlds;
    public bool AllCollectibles => GemsCollected >= TotalGems && 
                                  DragonsFreed >= TotalDragons && 
                                  EggsCollected >= TotalEggs;
    public bool AllChallenges => SkillMastery.Instance.AllChallengesComplete;
    public bool AllSecrets => SecretAreaManager.Instance.AllSecretsFound;

    void Start()
    {
        WorldManager.OnWorldCompleted += UpdateWorldCompletion;
        GemCollectible.OnGemCollected += UpdateGemCount;
        DragonStatue.OnDragonFreed += UpdateDragonCount;
        EggCollectible.OnEggCollected += UpdateEggCount;
        SkillMastery.OnSkillMastered += UpdateSkillMastery;
    }

    void UpdateWorldCompletion(int worldID) => WorldsCompleted++;
    void UpdateGemCount(int amount) => GemsCollected += amount;
    void UpdateDragonCount() => DragonsFreed++;
    void UpdateEggCount() => EggsCollected++;
    void UpdateSkillMastery() => SkillsMastered++;

    public bool Check100PercentCompletion()
    {
        return AllWorlds100Percent && 
               AllCollectibles && 
               AllChallenges && 
               AllSecrets &&
               AchievementSystem.Instance.AllAchievementsUnlocked;
    }
}

// Implementation of Core 100% Achievements
public static class CompletionAchievements
{
    public static void Create100PercentAchievements()
    {
        AchievementSystem.Instance.CreateAchievement(
            "perfect_worlds", 
            "Dimensional Dominator", 
            "Complete all worlds with 100% completion",
            AchievementType.WorldCompletion, 
            WorldManager.Instance.TotalWorlds,
            5000
        );

        AchievementSystem.Instance.CreateAchievement(
            "gem_tycoon", 
            "Gem Tycoon", 
            "Collect every gem in the game",
            AchievementType.TotalGemsCollected, 
            WorldManager.Instance.TotalGemsInGame,
            10000
        );

        AchievementSystem.Instance.CreateAchievement(
            "dragon_liberator", 
            "Dragon Liberator", 
            "Free all trapped dragons",
            AchievementType.DragonsFreed, 
            DragonStatueManager.Instance.TotalStatues,
            7500
        );

        AchievementSystem.Instance.CreateAchievement(
            "egg_hunter", 
            "Egg Hunter Supreme", 
            "Collect all dragon eggs",
            AchievementType.EggsCollected, 
            EggManager.Instance.TotalEggs,
            5000
        );

        AchievementSystem.Instance.CreateAchievement(
            "skill_grandmaster", 
            "Grandmaster of the Arts", 
            "Master all abilities and skills",
            AchievementType.SkillsMastered, 
            SkillMastery.Instance.TotalSkills,
            2500
        );

        AchievementSystem.Instance.CreateAchievement(
            "secret_seeker", 
            "Keeper of Secrets", 
            "Discover all hidden areas",
            AchievementType.SecretsFound, 
            SecretAreaManager.Instance.TotalSecrets,
            15000
        );


         AchievementSystem.Instance.CreateAchievement(
            "Complete_Tutorial", 
            "Tutorial Conqueror", 
            "Complete the tutorial",
            AchievementType.TutorialCompletion, 
            1,
            100
        );

        AchievementSystem.Instance.CreateAchievement(
            "ultimate_spyro", 
            "Ultimate Spyro", 
            "Achieve 100% game completion",
            AchievementType.FullCompletion, 
            1,
            50000
        );
    }
}