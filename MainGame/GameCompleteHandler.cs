// GameCompletionHandler.cs
public class GameCompletionHandler : MonoBehaviour
{
    void Update()
    {
        if (CompletionTracker.Instance.Check100PercentCompletion())
        {
            AchievementSystem.Instance.UnlockAchievement("ultimate_spyro");
            UnlockGoldenSkin();
            EnableNewGamePlus();
        }
    }

    private void UnlockGoldenSkin()
    {
        PlayerCustomization.Instance.UnlockSkin("Golden Dragon");
        
    }

    private void EnableNewGamePlus()
    {
        GameManager.Instance.EnableNewGamePlusMode();
    }
}