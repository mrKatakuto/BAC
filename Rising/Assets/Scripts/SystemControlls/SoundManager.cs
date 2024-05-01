using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton: Nur einmal verwenden
public class SoundManager : Singleton<SoundManager>
{
    GameObject soundGameObject = null;
    AudioSource audioSource = null;

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    // Liste von Soundclips
    private List<AudioClip> soundList = new List<AudioClip>();

    public enum Sound
    {
        Intro,
        WinterAmbience,
        Wind,
        Activate,
        Lab,
        Robot,
        Sword,
        Dead
    }

    public void Init()
    {
        if (soundGameObject == null)
        {
            soundGameObject = new GameObject("Sound");
            audioSource = soundGameObject.AddComponent<AudioSource>();

            // Sounds laden und zur Liste hinzufügen
            soundList.Add(Resources.Load<AudioClip>("Audio/Intro"));
            soundList.Add(Resources.Load<AudioClip>("Audio/WinterAmbience"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Wind"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Activate"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Lab"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Robot"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Sword"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Dead"));
        }
    }

    // PlaySound-Methode mit Lautstärkeregelung
    public void PlaySound(Sound sound, float volume = 1.0f, bool check = false)
    {
        if (check && audioSource.isPlaying)
            return;

        audioSource.PlayOneShot(soundList[(int)sound], volume);
    }
}
