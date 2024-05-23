using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public string mainSceneName; 

    private void Start()
    {
        PlayIntroVideo();
        
        if (PlayerPrefs.GetInt("hasPlayedVideo", 0) == 0)
        {
            PlayerPrefs.SetInt("hasPlayedVideo", 1);
            PlayIntroVideo();
        }
        else
        {

            LoadMainScene();
        }
        
        
    }

    private void PlayIntroVideo()
    {
        videoPlayer.loopPointReached += OnVideoFinished; 
        videoPlayer.Play(); 
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        LoadMainScene(); 
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(mainSceneName); 
    }
}
