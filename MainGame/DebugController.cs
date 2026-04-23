using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Debug utility to test game systems during runtime.
/// Attach this to a persistent GameObject in your scene.
/// </summary>
public class DebugController : MonoBehaviour
{
    void Update()
    {
        // Press G to simulate collecting 100 gems
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (GameManager.Instance != null)
            {
                Debug.Log("[Debug] Adding 100 Gems.");
                GameManager.Instance.AddScore(100);
                if (GemProgressionSystem.Instance != null)
                    GemProgressionSystem.Instance.AddGems(100);
            }
        }

        // Press H to test the damage system (10 damage)
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (GameManager.Instance != null)
            {
                Debug.Log("[Debug] Player taking 10 damage.");
                GameManager.Instance.TakeDamage(10);
            }
        }

        // Press R to reload the current scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("[Debug] Reloading scene.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Press K to force 100% completion check
        if (Input.GetKeyDown(KeyCode.K))
        {
            var tracker = FindObjectOfType<CompletionTracker>();
            if (tracker != null)
            {
                bool isComplete = tracker.Check100PercentCompletion();
                Debug.Log($"[Debug] 100% Completion Status: {isComplete}");
            }
        }
    }

    private void OnGUI()
    {
        GUI.color = Color.yellow;
        GUILayout.BeginArea(new Rect(10, 10, 250, 150));
        GUILayout.Label("--- DEBUG CONTROLS ---");
        GUILayout.Label("[G] Add 100 Gems");
        GUILayout.Label("[H] Take 10 Damage");
        GUILayout.Label("[R] Reload Scene");
        GUILayout.Label("[K] Check 100% Status");
        GUILayout.EndArea();
    }
}