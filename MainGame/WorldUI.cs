using UnityEngine;
using UnityEngine.UI; // Fixes: 'The type or namespace name "Text" could not be found'
using UnityEngine.SceneManagement;

public class WorldUI : MonoBehaviour
{
    [Header("UI References")]
    public Text worldNameText;
    public Text gemCountText;
    public Slider progressionSlider;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (WorldManager.Instance == null) return;

        // Update World Name based on current scene
        if (worldNameText != null)
        {
            worldNameText.text = SceneManager.GetActiveScene().name;
        }

        // Update Gem Progression
        if (gemCountText != null && GameManager.Instance != null)
        {
            int collected = GameManager.Instance.score;
            int total = WorldManager.Instance.TotalGemsInGame;

            gemCountText.text = $"Gems: {collected} / {total}";

            if (progressionSlider != null && total > 0)
            {
                progressionSlider.value = (float)collected / total;
            }
        }
    }
}