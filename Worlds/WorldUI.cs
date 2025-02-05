// WorldUI.cs
using UnityEngine;
using UnityEngine.UI;

public class WorldUI : MonoBehaviour
{
    public Text worldText;
    public Slider progressionSlider;

    void Update()
    {
        GemProgressionSystem gps = GemProgressionSystem.Instance;
        worldText.text = $"World {gps.currentWorld}";
        progressionSlider.value = (float)gps.collectedThisWorld / gps.gemsToNextWorld;
    }
}