using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void UnlockAchievement(string achievementId)
    {
        Debug.Log($"Achievement unlocked: {achievementId}");
    }
}
