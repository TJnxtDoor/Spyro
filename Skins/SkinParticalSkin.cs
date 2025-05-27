using UnityEngine;

[System.Serializable]
public class SkinParticleSettings
{
    public ParticleSystem trailEffect;
    public ParticleSystem auraEffect;
    public float trailLifetimeMultiplier = 1f;
    public Color particleColor = Color.white;
}

[System.Serializable]
public class Skin
{
    public Material material;
    public bool isUnlocked;
    public SkinParticleSettings particleSettings;
    public ParticleSystem collectEffect;
}

public class SkinManager : MonoBehaviour
{
    // Previous variables...
    
    private ParticleSystem currentTrail;
    private ParticleSystem currentAura;

    public void ApplySkin(int skinIndex)
    {
        if (IsValidIndex(skinIndex) && skins[skinIndex].isUnlocked)
        {
            // Apply material
            spyroRenderer.material = skins[skinIndex].material;
            PlayerPrefs.SetInt(CURRENT_SKIN_KEY, skinIndex);

            // Remove previous particles
            ClearExistingParticles();

            // Apply new particle effects
            SkinParticleSettings particles = skins[skinIndex].particleSettings;
            if (particles.trailEffect != null)
            {
                currentTrail = Instantiate(particles.trailEffect, spyroRenderer.transform);
                ApplyParticleSettings(currentTrail, particles);
            }
            
            if (particles.auraEffect != null)
            {
                currentAura = Instantiate(particles.auraEffect, spyroRenderer.transform.position, 
                                       Quaternion.identity, spyroRenderer.transform);
                ApplyParticleSettings(currentAura, particles);
            }
        }
    }

    private void ApplyParticleSettings(ParticleSystem ps, SkinParticleSettings settings)
    {
        var main = ps.main;
        main.startColor = settings.particleColor;
        
        if (ps.main.loop && ps is ParticleSystem.TrailModule)
        {
            var trails = ps.trails;
            trails.lifetimeMultiplier = settings.trailLifetimeMultiplier;
        }
        
        ps.Play();
    }

    private void ClearExistingParticles()
    {
        if (currentTrail != null) Destroy(currentTrail.gameObject);
        if (currentAura != null) Destroy(currentAura.gameObject);
    }

    // Rest of previous implementation...
}

public class CollectableSkin : MonoBehaviour
{
    // Previous variables...

    [SerializeField] private Light collectLight;
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private float floatAmplitude = 0.5f;

    private Vector3 startPos;
    private bool isCollected = false;

    private void Start()
    {
        startPos = transform.position;
        if (IsAlreadyCollected()) gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isCollected)
        {
            // Floating animation
            transform.position = startPos + Vector3.up * Mathf.Sin(Time.time) * floatAmplitude;
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void Collect()
    {
        isCollected = true;
        SkinManager.Instance.UnlockSkin(skinIndex);

        // Play attached effects
        if (collectEffect != null)
        {
            ParticleSystem instance = Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration);
        }

        if (collectLight != null)
        {
            Light lightInstance = Instantiate(collectLight, transform.position, Quaternion.identity);
            Destroy(lightInstance.gameObject, 1f);
        }

        DisableVisuals();
        MarkAsCollected();
    }

    private void DisableVisuals()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 3f); // Allow time for effects to finish
    }
}
