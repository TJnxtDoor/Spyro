using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("World Music")]
    [SerializeField] private AudioClip world1Music;
    [SerializeField] private AudioClip world2Music;
    [SerializeField] private AudioClip defaultMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeMusicForWorld();
    }

    private void InitializeMusicForWorld()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        AudioClip musicClip = defaultMusic;

        if (sceneName == "World 1")
        {
            musicClip = world1Music;
        }
        else if (sceneName == "World 2")
        {
            musicClip = world2Music;
        }

        if (musicClip != null)
        {
            PlayMusic(musicClip);
        }
    }

    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp01(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = Mathf.Clamp01(volume);
        }
    }
}