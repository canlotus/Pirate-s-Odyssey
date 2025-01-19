using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // Singleton örneği
    public AudioSource musicSource; // Müzik kaynağı
    public AudioSource sfxSource; // Ses efekt kaynağı
    public AudioClip backgroundMusic; // Arka plan müziği
    public AudioClip buttonClickSound; // Buton tıklama sesi

    private void Awake()
    {
        // Singleton oluştur
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahneler arasında müziğin devam etmesini sağlar
        }
        else
        {
            Destroy(gameObject); // Zaten bir MusicManager varsa yenisini yok et
        }
    }

    private void Start()
    {
        // Müzik kaynağı kontrolü
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>(); // Eğer bir AudioSource ekli değilse oluştur
        }

        // Ses efekt kaynağı kontrolü
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>(); // Ses efekt kaynağı oluştur
        }

        // AudioSource ayarlarını yap
        musicSource.clip = backgroundMusic;
        musicSource.loop = true; // Müzik döngüde çalsın
        musicSource.volume = 0.2f; // Ses seviyesi
        musicSource.playOnAwake = false; // Otomatik başlamasın

        sfxSource.playOnAwake = false; // Efekt otomatik başlamasın
        sfxSource.volume = 0.3f; // Efekt sesi seviyesi

        PlayMusic(); // Müziği başlat
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play(); // Müziği çal
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop(); // Müziği durdur
        }
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp(volume, 0f, 1f); // Ses seviyesini ayarla (0 ile 1 arasında)
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip); // Tek seferlik ses çal
        }
    }

    public void PlayButtonClickSound()
    {
        PlaySoundEffect(buttonClickSound); // Önceden tanımlanmış tıklama sesini çal
    }
}