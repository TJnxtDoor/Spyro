public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioManager()
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
            Destroy(gameObject);
        }
    }


    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }
}