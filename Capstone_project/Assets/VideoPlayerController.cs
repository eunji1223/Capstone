using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public List<VideoClip> videoClips = new List<VideoClip>();

    int VideoWidth = 1920;
int VideoHeight = 1080;


    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // 비디오 파일 로드 및 재생
        int randomNumber = Random.Range(0, 3);
        videoPlayer.clip = videoClips[randomNumber];
        videoPlayer.Play();

        // RawImage에 VideoPlayer의 출력을 연결
        videoPlayer.targetTexture = new RenderTexture(VideoWidth, VideoHeight, 0);
        rawImage.texture = videoPlayer.targetTexture;
    }
}

