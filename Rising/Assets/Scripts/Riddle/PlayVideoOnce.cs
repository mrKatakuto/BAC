using UnityEngine;
using UnityEngine.UI;  // Wichtig für den Umgang mit UI-Elementen
using UnityEngine.Video;

public class PlayVideoOnce : MonoBehaviour
{
    public VideoPlayer videoPlayer;      // Referenz zum VideoPlayer-Component
    public RawImage videoDisplay;        // RawImage, das das Video anzeigen wird
    public RenderTexture videoTexture;   // RenderTexture, auf die das Video gerendert wird
    private bool hasPlayed = false;      // Flag, ob das Video schon abgespielt wurde

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // Versuche den VideoPlayer zu erhalten, falls nicht zugewiesen
        }
        
        videoPlayer.targetTexture = videoTexture;      // Setze die Render Texture als Ziel für den Video Player
        videoPlayer.loopPointReached += EndReached;    // Abonnieren des Events, das am Ende des Videos ausgelöst wird
        videoPlayer.playOnAwake = false;               // Stelle sicher, dass das Video nicht automatisch startet
        videoPlayer.Prepare();                         // Vorbereiten des Videos zur Wiedergabe
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))  // Überprüfe, ob das Video noch nicht gespielt wurde und der Player den Trigger betritt
        {
            videoDisplay.texture = videoTexture;       // Weise die Render Texture dem Raw Image zu
            videoDisplay.enabled = true;               // Aktiviere das Raw Image, damit es sichtbar wird
            videoPlayer.Play();                        // Starte die Videowiedergabe
            hasPlayed = true;                          // Setze das Flag, dass das Video abgespielt wurde
        }
    }

    void EndReached(VideoPlayer vp)
    {
        vp.Stop();                                    // Stoppe die Videowiedergabe, wenn das Ende erreicht ist
        videoDisplay.enabled = false;                 // Deaktiviere das Raw Image, damit es nicht mehr sichtbar ist
        vp.gameObject.SetActive(false);               // Optional: Deaktiviere das GameObject des VideoPlayers, wenn das Video beendet ist
    }
}
