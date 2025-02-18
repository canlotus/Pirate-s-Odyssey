using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public Button playButton; 
    public float playDelay = 0.1f; 

    void Start()
    {
        Invoke("PlayVideo", playDelay);

        playButton.onClick.AddListener(() => StartCoroutine(PlayVideoWithDelay()));
    }

    private void PlayVideo()
    {
        videoPlayer.Play();
    }

    private IEnumerator PlayVideoWithDelay()
    {
        yield return new WaitForSeconds(playDelay);
        videoPlayer.Play();
    }
}
