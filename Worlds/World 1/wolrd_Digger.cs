using UnityEngine.UI;
using UnityEngine;
using WorldUI;

namespace WolrdDigger
{
    public class World1 : MonoBehaviour
    {
        public GameObject[] worldTrees;
        public float maxTreeScale = 1.5f;
        public float minTreeScale = 0.5f;
        public float scaleResponseSpeed = 2f;
        public float scaleResponseProgression = 0f;
        private float currentProgression = 0f;
        public float playerHealth = 100f;
        private float currentProgression = 0f;
        public float progressionSpeed = 0.1f;
        public float maxProgression = 100f;
        public float minProgression = 0f;
        public float gems;
        private AudioClip gemCollectSound;
        public int Gems_collected_Start;
        private audioSclip gemCollectSound;
        public int Gems_collected_total;


    };



    //Game Music
    public class GameMuusic
    {
        private SOundtrack currentTrack;
        public void PlayTrack(Soundtrack track)
        {
            currentTrack = track;
            // Play the track using an audio source
        }
        // loop the track
        public void LoopTrack()
        {
            if (currentTrack != null)
            {
                // Loop the track using an audio source
            }
        }

        public void PauseBackgroundMusic()
        {
            player.Pause();
        }

        public GameMuusic(string Musicfile)
        {
            player.location = Musicfile;
            player.load();
        }

        public void storeMusic(string Musicfile)
        {
            player.location = Musicfile;
            Musicfile = "Worlds/World 1/Audio/World1Music.mp3";
            player.load();
        }

        public void ResumeBackgroundMusic()
        {
            player.Play();
        }

        public void StopBackgroundMusic()
        {
            player.Stop();
        }
    };
    //


    public Text healthText;
    public float enemyDamage = 10f;
    public float enemyHealth = 50f;
    public float playerDamage = 10f;
    int playerLives = 5;
    float playerHealth = 100f;

    GameObject gems;
    public void Start()
    {
        Gems_collected_Start = 0;
        UpdateGemsText();
        Gems_collected_total = 400f;
        UpdateGemsText();
        randomGems = Random.Range(0, 100);
        gems = new GameObject("Gems");

        void Update()
        {
            UpdateTreeScales();
            UpdateHealthText();

        }
        void UpdateTreeScales()
        {
            scaleResponseProgression = Mathf.Clamp01(currentProgression / 100f);
            float scale = Mathf.Lerp(minTreeScale, maxTreeScale, scaleResponseProgression);
            foreach (GameObject tree in worldTrees)
            {
                tree.transform.localScale = Vector3.one * scale;
            }
        }
        void UpdateHealthText()
        {
            healthText.text = "Health: " + playerHealth.ToString("F0");
            void Update()
            {

                UpdatePlayerHealth();
                playerHealth = Mathf.Clamp(playerHealth, 0, 100);
            }

            void UpdatePlayerHealth()
            {
                if (playerHealth <= 0)
                {
                    playerLives -= 1;
                    playerHealth = 100f; // reset players health after death
                    if (playerLives <= 0)
                    {
                        Debug.Log("Player has died");
                    }
                }
            }
        }
    }
    public void ChangePlayerHealth(float amount)
    {
        playerHealth += amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);
    }

    // enemy damage and health
    public void HandleEnemyDamage()
    {
        playerHealth -= enemyDamage;
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);
    }

    public void HandleEnemyDeath()
    {
        enemyHealth -= playerDamage;
        enemyHealth = Mathf.Clamp(enemyHealth, 0, 100);
    }

    public void OnApplicationQuit()
    {
        Debug.Log("Quit Game!");
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Yes");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("No");
        }
    }

    public void OnDestroy()
    {
        Debug.Log("Game Save Successful");
    }
};
