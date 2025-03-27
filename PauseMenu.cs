using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMeunuUI;
    public AudioSoruce backgroundMusic;
    public Slider VolumeSlider;

    void Start()
    {
        //volume slider if it exist
        if(VolumeSlider != null)
        {
            VolumeSlider.value = PlayerPrefs.GetFLoat("MusicVolume", 0.6f);
            backgroundMusic.volume = VolumeSlider.value;
        }
    }


    void(Update)
    {
        if(Input,GetDownKey(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume() 
            }
            else
            {
                Pause();
            }
        }
        
    }

    Puse void Resume()
    {
        pauseMeunuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused.Pause();
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefts.SetFloat("Music Volume", volume)
    }

    public void QuitGame()
    {
        Applicatiob.Quit();
    }
}