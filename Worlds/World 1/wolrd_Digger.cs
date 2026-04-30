using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace WorldDigger
{
    public class World1 : MonoBehaviour
    {
        // Tree scaling configuration
        [Header("Tree Scaling")]
        [SerializeField] private GameObject[] worldTrees;
        [SerializeField] private float maxTreeScale = 1.5f;
        [SerializeField] private float minTreeScale = 0.5f;
        [SerializeField] private float scaleResponseSpeed = 2f;

        // Progression system settings
        [Header("Progression")]
        [SerializeField] private float progressionSpeed = 0.1f;
        [SerializeField] private float maxProgression = 100f;
        [SerializeField] private float minProgression = 0f;

        // Player statistics
        [Header("Player Stats")]
        [SerializeField] private float playerHealth = 100f;
        [SerializeField] private int playerLives = 5;
        [SerializeField] private float enemyDamage = 10f;
        [SerializeField] private float playerDamage = 10f;

        // Audio feedback
        [Header("Audio")]
        [SerializeField] private AudioClip gemCollectSound;

        // UI text references
        [Header("UI")]
        [SerializeField] private Text healthText;
        [SerializeField] private Text gemsText;

        // Internal state variables
        private float enemyHealth = 50f;
        private int randomGems;
        private float currentProgression = 0f;
        private int gemsCollected = 0;
        private int gemsCollectedTotal = 0;

        // Initialize random gem count and UI on start
        private void Start()
        {
            randomGems = Random.Range(0, 100);
            UpdateGemsText();
        }

        // Main update loop - runs every frame
        private void Update()
        {
            UpdateTreeScaling();
            UpdateProgression();
            UpdatePlayerStatus();
        }

        // Scales trees based on current progression value
        private void UpdateTreeScaling()
        {
            if (worldTrees == null) return;

            // Calculate target scale using linear interpolation
            float targetScale = Mathf.Lerp(minTreeScale, maxTreeScale, currentProgression / maxProgression);
            
            // Apply smooth scaling to each tree
            foreach (GameObject tree in worldTrees)
            {
                if (tree != null)
                {
                    tree.transform.localScale = Vector3.Lerp(
                        tree.transform.localScale,
                        Vector3.one * targetScale,
                        scaleResponseSpeed * Time.deltaTime
                    );
                }
            }
        }

        // Continuously increases progression over time
        private void UpdateProgression()
        {
            currentProgression = Mathf.Clamp(
                currentProgression + progressionSpeed * Time.deltaTime, 
                minProgression, 
                maxProgression
            );
        }

        // Get current progression value
        public float GetProgression()
        {
            return currentProgression;
        }

        // Check player status each frame
        private void UpdatePlayerStatus()
        {
            if (playerHealth <= 0)
            {
                HandlePlayerDeath();
            }
        }

        // Handle player death - decrement lives and respawn
        private void HandlePlayerDeath()
        {
            playerLives--;
            
            if (playerLives <= 0)
            {
                Debug.Log("Player has died - Game Over");
                playerLives = 0;
            }
            else
            {
                playerHealth = 100f;
            }
        }

        // Apply damage to player
        public void TakeDamage(float damage)
        {
            playerHealth -= damage;
            playerHealth = Mathf.Max(0, playerHealth);
            UpdateHealthUI();
        }

        // Modify player health by specified amount
        public void ChangePlayerHealth(float amount)
        {
            playerHealth += amount;
            playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        }

        // Handle damage from enemy encounters
        public void HandleEnemyDamage()
        {
            playerHealth -= enemyDamage;
            playerHealth = Mathf.Clamp(playerHealth, 0, 100);
            UpdateHealthUI();
        }

        // Handle enemy death when player attacks
        public void HandleEnemyDeath()
        {
            enemyHealth -= playerDamage;
            enemyHealth = Mathf.Clamp(enemyHealth, 0, 100);
        }

        // Increment gem counters when collected
        public void CollectGem()
        {
            gemsCollected++;
            gemsCollectedTotal++;

            // Play collection sound if available
            if (gemCollectSound != null)
            {
                AudioSource.PlayClipAtPoint(gemCollectSound, transform.position);
            }
            
            UpdateGemsText();
        }

        // Get gems collected in current session
        public int GetGemsCollected()
        {
            return gemsCollected;
        }

        // Update health display text
        private void UpdateHealthUI()
        {
            if (healthText != null)
            {
                healthText.text = "Health: " + playerHealth.ToString("F0");
            }
        }

        // Update gems display text
        private void UpdateGemsText()
        {
            if (gemsText != null)
            {
                gemsText.text = "Gems: " + gemsCollected;
            }
        }

        // Save game when application quits
        private void OnApplicationQuit()
        {
            Debug.Log("Game Save Successful");
        }

        // Cleanup when object is destroyed
        private void OnDestroy()
        {
            Debug.Log("World1 cleanup complete");
        }
    }

    // Music management system
    public class GameMusicManager : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip defaultTrack;

        private AudioClip currentTrack;

        // Add AudioSource component if not already present
        private void Start()
        {
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Play specified audio track
        public void PlayTrack(AudioClip track)
        {
            if (musicSource != null && track != null)
            {
                currentTrack = track;
                musicSource.clip = track;
                musicSource.loop = true;
                musicSource.Play();
            }
        }

        // Enable track looping
        public void LoopTrack()
        {
            if (musicSource != null)
            {
                musicSource.loop = true;
            }
        }

        // Pause currently playing music
        public void PauseMusic()
        {
            if (musicSource != null)
            {
                musicSource.Pause();
            }
        }

        // Resume paused music
        public void ResumeMusic()
        {
            if (musicSource != null)
            {
                musicSource.UnPause();
            }
        }

        // Stop music playback
        public void StopMusic()
        {
            if (musicSource != null)
            {
                musicSource.Stop();
            }
        }

        // Set and play new track
        public void SetTrack(AudioClip track)
        {
            PlayTrack(track);
        }
    }
}
