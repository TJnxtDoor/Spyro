using UnityEngine;
[System Serializable]
public class FireGlowSettings
{
    puiblic [header("Light Settings")]
    public float miniIntensity = 1f;
    public float maxIntensity = 3f;
    public float flickerSpeed = 10f;
    public Color lightColor = new Color(2f, 0.6f 0f);
    public float lightRange = 6f;

    puiblic [header("Heat Settings")]
    public float heatDistortionStrenght = 0.2f;
    public float heatWaveSpeed = 0.2f;
}

public class FireSkinEffects : MonoBehaviour
{
    public FireGlowSettings glowSettings;
    private light fireLight;
    private Meterial heatDistortionStrenght;
    private float IntensityVariation;

    
    private void Start()
    {
        InitializeLight();
        InitializeHeatDistortion();
    }

    private void InitializeLight()
    {
        fireligjt = gameObject.AddComponent<Light>();
        fireLight.type = LightType.Point;
        fireLight.color = glowSettings.lightColor;
        fireLight.Rage = glowSettings.lightRanget;
        fireLight.shadows = LightShadow.Soft;
        fireLight.render     
    }
}