using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class FootstepEffect
{
    public ParticleSystem footstepParticle;
    public GameObject footprintDecal;
    public float footprintSize = 1f;
    public float footprintLifetime = 30f;
    public AudioClip[] footstepSounds;
}

public class FootstepEmitter : MonoBehaviour
{
    [Header("References")]
    public Transform[] footTransforms;
    public LayerMask groundLayers;
    public AudioSource audioSource;

    [Header("Settings")]
    public float stepInterval = 0.3f;
    public float velocityThreshold = 0.1f;
    public int maxFootprints = 50;

    private float lastStepTime;
    private int currentFootIndex;
    private Queue<GameObject> footprintPool = new Queue<GameObject>();
    private List<GameObject> activeFootprints = new List<GameObject>();

    private void Start()
    {
        InitializeFootprintPool();
    }

    private void InitializeFootprintPool()
    {
        FootstepEffect currentEffects = SkinManager.Instance.CurrentSkinEffects();
        
        for (int i = 0; i < maxFootprints; i++)
        {
            GameObject footprint = Instantiate(currentEffects.footprintDecal);
            footprint.SetActive(false);
            footprintPool.Enqueue(footprint);
        }
    }

    private void Update()
    {
        if (ShouldTriggerStep())
        {
            TriggerStepEffect();
            lastStepTime = Time.time;
        }
    }

    private bool ShouldTriggerStep()
    {
        float velocity = GetComponent<Rigidbody>().velocity.magnitude;
        return velocity > velocityThreshold && 
               Time.time > lastStepTime + stepInterval;
    }

    private void TriggerStepEffect()
    {
        Transform currentFoot = footTransforms[currentFootIndex];
        currentFootIndex = (currentFootIndex + 1) % footTransforms.Length;

        if (Physics.Raycast(currentFoot.position, Vector3.down, 
            out RaycastHit hit, 1f, groundLayers))
        {
            SpawnFootstepEffects(hit.point + Vector3.up * 0.1f, hit.normal);
            PlaceFootprint(hit.point, hit.normal);
        }
    }

    private void SpawnFootstepEffects(Vector3 position, Vector3 normal)
    {
        FootstepEffect effects = SkinManager.Instance.CurrentSkinEffects();
        
        if (effects.footstepParticle != null)
        {
            ParticleSystem instance = Instantiate(
                effects.footstepParticle,
                position,
                Quaternion.LookRotation(normal)
            );
            Destroy(instance.gameObject, instance.main.duration);
        }

        PlayFootstepSound(effects);
    }

    private void PlayFootstepSound(FootstepEffect effects)
    {
        if (effects.footstepSounds.Length > 0 && audioSource != null)
        {
            AudioClip clip = effects.footstepSounds[
                Random.Range(0, effects.footstepSounds.Length)
            ];
            audioSource.PlayOneShot(clip);
        }
    }

    private void PlaceFootprint(Vector3 position, Vector3 normal)
    {
        FootstepEffect effects = SkinManager.Instance.CurrentSkinEffects();
        
        if (footprintPool.Count == 0)
            RecycleOldestFootprint();

        GameObject footprint = footprintPool.Dequeue();
        footprint.transform.position = position;
        footprint.transform.rotation = Quaternion.LookRotation(normal);
        footprint.transform.localScale = Vector3.one * effects.footprintSize;
        footprint.SetActive(true);
        
        activeFootprints.Add(footprint);
        StartCoroutine(DeactivateFootprint(footprint, effects.footprintLifetime));
    }

    private System.Collections.IEnumerator DeactivateFootprint(GameObject footprint, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        footprint.SetActive(false);
        activeFootprints.Remove(footprint);
        footprintPool.Enqueue(footprint);
    }

    private void RecycleOldestFootprint()
    {
        if (activeFootprints.Count > 0)
        {
            GameObject oldest = activeFootprints[0];
            oldest.SetActive(false);
            activeFootprints.RemoveAt(0);
            footprintPool.Enqueue(oldest);
        }
    }
}

// Add to your existing Skin class
[System.Serializable]
public class Skin
{
    // Existing properties...
    public FootstepEffect footstepEffects;
}

// Add to your existing SkinManager class
public class SkinManager : MonoBehaviour
{
    // Existing properties and methods...
    
    public FootstepEffect CurrentSkinEffects()
    {
        int currentIndex = PlayerPrefs.GetInt(CURRENT_SKIN_KEY, 0);
        return skins[currentIndex].footstepEffects;
    }

    public void ApplySkin(int skinIndex)
    {
        // Existing code...
        UpdateFootprintPool();
    }

    private void UpdateFootprintPool()
    {
        // Destroy old footprints
        foreach (GameObject fp in footprintPool)
            Destroy(fp);
        
        foreach (GameObject fp in activeFootprints)
            Destroy(fp);

        footprintPool.Clear();
        activeFootprints.Clear();
        InitializeFootprintPool();
    }
}