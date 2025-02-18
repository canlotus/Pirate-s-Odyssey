using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip buttonClickSound;

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
       
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

       
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = 0.2f;
        musicSource.playOnAwake = false;

        sfxSource.playOnAwake = false;
        sfxSource.volume = 0.3f;

        PlayMusic();
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayButtonClickSound()
    {
        PlaySoundEffect(buttonClickSound);
    }
}
