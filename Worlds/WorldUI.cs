// WorldUI.cs
using UnityEngine;
using UnityEngine.UI;

public class WorldUI : MonoBehaviour
{
    public Text worldText;
    public Slider progressionSlider;

    // Resonsive Greass based on where player moves 

    [Header("Tree Settings")]
    public GameObject[] worldTress
    public float maxTressScale = 1.5f;
    public float minTreeScale = 0.8f;
    public float scaleResponseSpeed = 2f;
    public float scaleResponseProgression = 0f;
    [Header("Grass Settings")]
    public GameObject grassParents = 0.3f
    public float grassBendAmmount =  0.2f
    public float grassRecoverySpeed = 4f
    public float grassProgressionMultiplier = 1.4f
    public Color healthyGrassColor = new Color(0.2f, 0.8f, 0.3f)
    public Color dryGrassColor = new Color(0.7f, 0.6f, 0.2f);
    
        private Vector3[] initialTreeScales;
        private float currentProgression = 0f;
        private Transform playerTransform;
        private Material[] grassMaterials;
        private Vector4[] grassOriginalPositions;
        private bool grassInitialized = false;





   void Start()
    {
        // Initialize trees
        if (worldTrees != null && worldTrees.Length > 0)
        {
            initialTreeScales = new Vector3[worldTrees.Length];
            for (int i = 0; i < worldTrees.Length; i++)
            {
                if (worldTrees[i] != null)
                {
                    initialTreeScales[i] = worldTrees[i].transform.localScale;
                }
            }
        }

        // Initialize grass
        InitializeGrass();
        
        // Find player (tagged as "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void InitializeGrass()
    {
        if (grassParent != null)
        {
            // Get all grass mesh renderers
            MeshRenderer[] grassRenderers = grassParent.GetComponentsInChildren<MeshRenderer>();
            grassMaterials = new Material[grassRenderers.Length];
            
            // Store original material references
            for (int i = 0; i < grassRenderers.Length; i++)
            {
                grassMaterials[i] = grassRenderers[i].material;
            }
            
            grassInitialized = true;
        }
    }

    void Update()
    {
        GemProgressionSystem gps = GemProgressionSystem.Instance;
        worldText.text = $"World {gps.currentWorld}";
        
        // Update progression
        currentProgression = Mathf.Lerp(currentProgression, 
                                      (float)gps.collectedThisWorld / gps.gemsToNextWorld, 
                                      Time.deltaTime * scaleResponseSpeed);
        progressionSlider.value = currentProgression;
        
        // Update environment
        UpdateTreeScales(currentProgression);
        UpdateGrass(currentProgression);
    }
    
    void UpdateTreeScales(float progression)
    {
        if (worldTrees == null || initialTreeScales == null) return;
        
        for (int i = 0; i < worldTrees.Length; i++)
        {
            if (worldTrees[i] != null)
            {
                float scaleFactor = Mathf.Lerp(minTreeScale, maxTreeScale, progression);
                float individualVariation = 0.9f + 0.2f * Mathf.PerlinNoise(i * 10f, Time.time * 0.5f);
                worldTrees[i].transform.localScale = initialTreeScales[i] * scaleFactor * individualVariation;
            }
        }
    }
    
    void UpdateGrass(float progression)
    {
        if (!grassInitialized || playerTransform == null) return;
        
        // Calculate grass health based on progression
        float grassHealth = Mathf.Clamp01(progression * grassProgressionMultiplier);
        Color currentGrassColor = Color.Lerp(dryGrassColor, healthyGrassColor, grassHealth);
        
        foreach (Material mat in grassMaterials)
        {
            if (mat != null)
            {
                // Update grass color
                mat.SetColor("_Color", currentGrassColor);
                
                // Calculate player distance effect
                Vector3 playerPos = playerTransform.position;
                playerPos.y = 0; // Ignore height difference
                
                // Get grass position (assuming each material is used by one grass patch)
                Vector3 grassPos = mat.transform.position;
                grassPos.y = 0;
                
                float distance = Vector3.Distance(playerPos, grassPos);
                float influence = Mathf.Clamp01(1 - (distance / 5f)); // 5m influence radius
                
                // Calculate bend direction (away from player)
                Vector3 bendDirection = (grassPos - playerPos).normalized;
                bendDirection.y = 0;
                
                // Apply bending effect
                Vector4 bend = new Vector4(
                    bendDirection.x * influence * grassBendAmount,
                    0,
                    bendDirection.z * influence * grassBendAmount,
                    influence * grassRecoverySpeed
                );
                
                mat.SetVector("_BendRotation", bend);
            }
        }
    }


        

    void Update()
    {
        GemProgressionSystem gps = GemProgressionSystem.Instance;
        worldText.text = $"World {gps.currentWorld}";
        
        // Update progression slider
        currentProgression = Mathf.Lerp(currentProgression, 
                                      (float)gps.collectedThisWorld / gps.gemsToNextWorld, 
                                      Time.deltaTime * scaleResponseSpeed);
        progressionSlider.value = currentProgression;
        
        // Update tree scales based on progression
        UpdateTreeScales(currentProgression);
    }
    
    void UpdateTreeScales(float progression)
    {
        if (worldTrees == null || initialTreeScales == null) return;
        
        for (int i = 0; i < worldTrees.Length; i++)
        {
            if (worldTrees[i] != null)
            {
                // Calculate scale based on progression with some variation
                float scaleFactor = Mathf.Lerp(minTreeScale, maxTreeScale, progression);
                float individualVariation = 0.9f + 0.2f * Mathf.PerlinNoise(i * 10f, Time.time * 0.5f);
                
                worldTrees[i].transform.localScale = initialTreeScales[i] * scaleFactor * individualVariation;
                
                // Optional: Add slight sway animation
                float swayAmount = progression * 2f;
                float sway = Mathf.Sin(Time.time * (1f + i * 0.2f)) * swayAmount;
                worldTrees[i].transform.rotation = Quaternion.Euler(0, sway, 0);
            }
        }
    }
}