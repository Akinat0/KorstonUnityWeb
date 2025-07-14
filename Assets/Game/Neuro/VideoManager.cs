using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    void Start()
    {
        Debug.LogWarning("TRY PLAY VIDEO");
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Professional_Mode_A_2D_cartoon_cat_girl_character_.mp4");
        videoPlayer.Play();
    }
}
