using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.Video;

public class PlayVideoOnce : MonoBehaviour
{
    public VideoPlayer videoPlayer;      
    public RawImage videoDisplay;        
    public RenderTexture videoTexture;
    public GameObject gameCanvas; 
    private AudioSource[] allAudioSources; 
    private bool hasPlayed = false;      

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); 
        }
        
        videoPlayer.targetTexture = videoTexture;      
        videoPlayer.loopPointReached += EndReached;    
        videoPlayer.playOnAwake = false;               
        videoPlayer.Prepare();                         
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))  
        {
            videoDisplay.texture = videoTexture;       
            videoDisplay.enabled = true; 
            gameCanvas.SetActive(false); 
            videoPlayer.Play();                        
            hasPlayed = true;  
            PauseAllAudioExceptVideo();
        }
    }

    void EndReached(VideoPlayer vp)
    {
        vp.Stop();                                    
        videoDisplay.enabled = false;                 
        vp.gameObject.SetActive(false);               
        ResumeAllAudio();
        gameCanvas.SetActive(true); 
    }

    private void PauseAllAudioExceptVideo()
    {
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio != videoPlayer.GetTargetAudioSource(0)) 
            {
                audio.Pause();
            }
        }
    }

    private void ResumeAllAudio()
    {
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio != videoPlayer.GetTargetAudioSource(0)) 
            {
                audio.UnPause();
            }
        }
    }
}
