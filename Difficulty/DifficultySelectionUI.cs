// DifficultySelectorUI.cs
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectorUI : MonoBehaviour
{
    public DifficultySettings difficultySettings;
    public Button[] difficultyButtons;

    void Start()
    {
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            int index = i;
            difficultyButtons[i].onClick.AddListener(() => 
                SetDifficulty((DifficultySettings.DifficultyLevels)index));
        }
    }

    private void SetDifficulty(DifficultySettings.DifficultyLevels level)
    {
        difficultySettings.SetDifficulty(level);
        GameManager.Instance.difficultySettings = difficultySettings;
        Debug.Log($"Difficulty set to: {level}");
    }

    //save Selected difficulty to PlayerPerfs
    public void AdjustDynamicDifficulty(float playerSucceessRate)
    {
        if (playerSucceessRate > 0.8f)
            difficultySettings.SetDifficulty(difficultySettings.currentDifficulty + 1);
        else if (playerSucceessRate < 0.3f)
            difficultySettings.SetDifficulty(difficultySettings.currentDifficulty - 1);
    }

    //Unlock Achievements based on difficulty
    public void CheckDifficultyAchievements()
        {
            if (difficultySettings.currentDifficulty == DifficultySettings.DifficultyLevels.ExtremelyDifficult)
            {
                AchievementSystem.Instance?.UnlockAchievement("EXTREME_CHALLENGER");
            }
        }
}
