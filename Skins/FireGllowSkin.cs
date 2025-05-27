using UnityEngine;

namespace Spyro.Skins
{
    [System.Serializable]
    public class FireGlowSettings
    {
        [Header("Light Settings")]
        public float miniIntensity = 1f;
        public float maxIntensity = 3f;
        public float flickerSpeed = 10f;
        public Color lightColor = new Color(2f, 0.6f, 0f);
        public float lightRange = 6f;

        [Header("Heat Settings")]
        public float heatDistortionStrength = 0.2f;
        public float heatWaveSpeed = 0.2f;
    }

    public class FireSkinEffects : MonoBehaviour
    {
        public FireGlowSettings glowSettings;
        private Light fireLight;
        private Material heatDistortionMaterial;
        private readonly float IntensityVariation = 0f;

        private void Start()
        {
            InitializeLight();
            InitializeHeatDistortion();
        }

        private void InitializeLight()
        {
            fireLight = gameObject.AddComponent<Light>();
            fireLight.type = LightType.Point;
            fireLight.color = glowSettings.lightColor;
            fireLight.range = glowSettings.lightRange;
            fireLight.shadows = LightShadows.Soft;
            // fireLight.renderMode = LightRenderMode.Auto; // Uncomment and adjust as needed
        }

        private void InitializeHeatDistortion()
        {
            // Implementation for heat distortion initialization
        }
    }
}