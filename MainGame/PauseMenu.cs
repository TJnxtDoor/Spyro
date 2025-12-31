using UnityEngine;
using UnityEngine.UI;

namespace Spyro.MainGame
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;
        public AudioSource backgroundMusic;
        public Slider VolumeSlider;
        


        void Start()
        {
            // volume slider if it exists
            if (VolumeSlider != null)
            {
                VolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.6f);
                backgroundMusic.volume = VolumeSlider.value;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void SetVolume(float volume)
        {
            backgroundMusic.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        public void QuitGame()
        {
            Application.Quit();
        }


        public void MainMenuMusic()
        {
            backgroundMusic.repeat = true;
            backgroundMusic.Play();
            backgroundMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 0.6f);
            backgroundMusic.loop = true;
            
        }
    }
}