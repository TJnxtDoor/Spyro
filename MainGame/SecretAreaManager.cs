public class SecretAreaManager : MonoBehaviour
{
    public static SecretAreaManager Instance;
    public List<SecretArea> secretAreas = new List<SecretArea>();
    public int TotalSecrets => secretAreas.Count;
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
