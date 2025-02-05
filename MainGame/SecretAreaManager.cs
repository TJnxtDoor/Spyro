public class SecretAreaMawnager : MonoBehaviour
{
    public static SecretAreaMawnager Instance;
    public List<SecretArea> secrecAreas = new List<SecretArea>();
    public int TotalSecrets => secrecAreas.Count;
    public int FoundSecrets {get; private set; }

    public void RegisterSecretsFound()
    {
        FoundSecrets++;
        if( FoundSecrets >= TotalSecrets)
        {
            AchievementSystem.Instance.UnlockAchievement("You_Found_it!!!");
        }
    }
}