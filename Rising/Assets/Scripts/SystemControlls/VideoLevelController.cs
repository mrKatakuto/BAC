using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoLevelController : MonoBehaviour
{
    public VideoPlayer videoPlayer; 

    void Start()
    {
        if (videoPlayer == null)
        {
            return;
        }

        videoPlayer.loopPointReached += OnVideoFinished; 
    }


    private void OnVideoFinished(VideoPlayer vp)
    {
        LoadMainMenu(); 
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu"); 
    }
}
