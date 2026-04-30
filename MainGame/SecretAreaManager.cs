using System.Collections.Generic;
using UnityEngine;

public class SecretAreaManager : MonoBehaviour
{
    public static SecretAreaManager Instance;
    public List<SecretArea> secretAreas = new List<SecretArea>();
    public int TotalSecrets => secretAreas.Count;
    public bool AllSecretsFound => FoundSecrets >= TotalSecrets;
    public int FoundSecrets {get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterSecretsFound()
    {
        FoundSecrets++;
        if( FoundSecrets >= TotalSecrets)
        {
            AchievementSystem.Instance.UnlockAchievement("You_Found_it!!!");
        }
    }
}

[System.Serializable]
public class SecretArea
{
    public bool discovered;
}
