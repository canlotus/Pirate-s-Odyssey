using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video Player referansı
    public Button playButton; // Tekrar oynatma butonu
    public float playDelay = 0.1f; // Gecikme süresi

    void Start()
    {
        // Play On Awake'i gecikmeli çalıştır
        Invoke("PlayVideo", playDelay);

        // Butona tıklama olayı ekle
        playButton.onClick.AddListener(() => StartCoroutine(PlayVideoWithDelay()));
    }

    private void PlayVideo()
    {
        videoPlayer.Play();
    }

    private IEnumerator PlayVideoWithDelay()
    {
        // Videoyu oynatmadan önce belirtilen süre kadar bekle
        yield return new WaitForSeconds(playDelay);
        videoPlayer.Play();
    }
}