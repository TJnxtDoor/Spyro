using UnityEngine;

[System.Serializable]
public class SmokeSparkSettings
{
    [Header("Smoke Settings")]
    public ParticleSystem smokeParticles;
    public float smokeEmissionRate = 20f;
    public Gradient smokeColorOverLifetime;
    public Vector3 smokeParticleOffset = new Vector3(0, 0.5f, 0);

    [Header("Spark Settings")]
    public ParticleSystem sparkParticles;
    public float sparkBurstInterval = 0.5f;
    public int sparkBurstAmount = 5;
    public Color sparkColor = Color.yellow;
    public Vector3 sparkParticleOffset = new Vector3(0, 0.2f, 0);
}

public class SkinParticleController : MonoBehaviour
{
    private ParticleSystem activeSmoke;
    private ParticleSystem activeSparks;
    private ParticleSystem.EmissionModule smokeEmission;
    private ParticleSystem.MainModule sparkMain;

    public void InitializeParticles(SmokeSparkSettings settings)
    {
        ClearExistingParticles();
        
        if (settings.smokeParticles != null)
        {
            activeSmoke = Instantiate(settings.smokeParticles, 
                transform.position + settings.smokeParticleOffset, 
                Quaternion.identity, 
                transform);
            
            ConfigureSmoke(settings);
        }

        if (settings.sparkParticles != null)
        {
            activeSparks = Instantiate(settings.sparkParticles, 
                transform.position + settings.sparkParticleOffset, 
                Quaternion.identity, 
                transform);
            
            ConfigureSparks(settings);
        }
    }

    private void ConfigureSmoke(SmokeSparkSettings settings)
    {
        ParticleSystem.MainModule main = activeSmoke.main;
        ParticleSystem.ColorOverLifetimeModule color = activeSmoke.colorOverLifetime;
        
        main.startSpeed = 0.5f;
        main.startSize = 0.3f;
        color.color = settings.smokeColorOverLifetime;
        
        smokeEmission = activeSmoke.emission;
        smokeEmission.rateOverTime = settings.smokeEmissionRate;
        
        activeSmoke.Play();
    }

    private void ConfigureSparks(SmokeSparkSettings settings)
    {
        ParticleSystem.MainModule main = activeSparks.main;
        ParticleSystem.EmissionModule emission = activeSparks.emission;
        
        main.startColor = settings.sparkColor;
        main.startSpeed = 2f;
        main.startSize = 0.1f;
        
        emission.SetBursts(new ParticleSystem.Burst[] {
            new ParticleSystem.Burst(settings.sparkBurstInterval, settings.sparkBurstAmount)
        });
        
        activeSparks.Play();
    }

    public void UpdateSmokeIntensity(float intensity)
    {
        if (activeSmoke != null)
        {
            smokeEmission.rateOverTime = intensity * 50f;
        }
    }

    private void ClearExistingParticles()
    {
        if (activeSmoke != null) Destroy(activeSmoke.gameObject);
        if (activeSparks != null) Destroy(activeSparks.gameObject);
    }
}

// Add to your existing Skin class
[System.Serializable]
public class Skin
{
    // Existing properties...
    public SmokeSparkSettings smokeSparkEffects;
}

// Add to your existing SkinManager class
public class SkinManager : MonoBehaviour
{
    // Existing properties...
    private SkinParticleController particleController;

    private void Start()
    {
        particleController = GetComponent<SkinParticleController>();
        if (particleController == null)
            particleController = gameObject.AddComponent<SkinParticleController>();
    }

    public void ApplySkin(int skinIndex)
    {
        if (IsValidIndex(skinIndex) && skins[skinIndex].isUnlocked)
        {
            // Existing material application...
            particleController.InitializeParticles(skins[skinIndex].smokeSparkEffects);
        }
    }
}