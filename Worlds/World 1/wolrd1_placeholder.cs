<<<<<<< HEAD
using UnityEngine.UI;
using UnityEngine;
using WorldUI;
=======
using UnityEngine;
using UnityEngine.UI;
>>>>>>> 1dff947955af425608aa3a58755f2e892d2f66ad

namespace World1
{
    public class World1 : MonoBehaviour
    {
        public GameObject[] worldTrees;
        public float maxTreeScale = 1.5f;
        public float minTreeScale = 0.5f;
        public float scaleResponseSpeed = 2f;
        public float scaleResponseProgression = 0f;
<<<<<<< HEAD
        private float currentProgression = 0f;
        public float playerHealth = 100f;
        public float playerLives = 5f;
    }

}


=======
        private readonly float currentProgression = 0f;
        public float playerHealth = 100f;
        public float playerLives = 5f;
        public Text healthText;
        public float enemyDamage = 10f;
        public float enemyHealth = 50f;

        void Update()
        {
            updatePlayerHealth();
            Mathf.Clamp(playerHealth, 0, 100);
        }

        void updatePlayerHealth()
        {
            if (playerHealth <= 0)
            {
                playerLives -= 1;
                playerHealth = 100f; // reset players heath after death
                if (playerLives <= 0)
                {
                    Debug.Log("Player has died");

                }

            }
        }
        private void ChangePlayerHealth(float amount)
        {
            playerHealth += amount;
            playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        }


        // enemy damage and health
        private void HandleEnemyDamage()
        {
            playerHealth -= enemyDamage;
            playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        }

        private void HandleEnemyDeath()
        {
            enemyHealth -= playerDamage;
            enemyHealth = Mathf.Clamp(enemyHealth, 0, 100);
        }

        private void OnApplicationQuit(KEYBOARD_EVENT Q)
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
       

        private void OnDestroy()
        {
            Debug.Log("Game Save Successful");
        }
    }
}
>>>>>>> 1dff947955af425608aa3a58755f2e892d2f66ad
