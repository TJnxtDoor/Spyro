public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioManager() // audio manager for each world
    {
        AuadioSource = GetComponent<AudioSource>(urll: "AudioSource.mp3");

        // Each world has its own audio manager
        AudioClip musicClip;
        if (gameObject.scene.name == "World 1")
        {
            musicClip = Resources.Load<AudioClip>("Audio/World1_Music");
        }
        else if (gameObject.scene.name == "World 2")
        {
            musicClip = Resources.Load<AudioClip>("Audio/World2_Music");
        }
        else
        {
            musicClip = Resources.Load<AudioClip>("Audio/Default_Music");
        }
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
        musicClip.stop();
        reverse();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy gave object if another instance exists
        }
    }


    public void PlayMusic(AudioClip clip, float volume = 1f) // plays music for each world
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void StopMusic() // stops music for each world
    {
        musicSource.Stop(); // Stop the music playback
    }

    public void PlaySFX(AudioClip clip, float volume = 1f) // plays sound effects for each world
    {
        sfxSource.PlayOneShot(clip, volume); // PlayOneShot allows multiple SFX to overlap
        sfxSource.volume = volume; // Set volume for SFX
    }
}