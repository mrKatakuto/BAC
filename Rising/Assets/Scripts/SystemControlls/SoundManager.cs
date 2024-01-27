using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton: Nur einmal verwenden
public class SoundManager : Singleton<SoundManager>
{
    GameObject soundGameObject = null;
    AudioSource audioSource = null;

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    //new Soundlist
    private List<AudioClip> soundList = new List<AudioClip>();

    public enum Sound
    {
        /*
       Back,
       Coat,
       Confirm,
       Fall,
       Jump,
       Quik, 
       Select,
       BackgroundMusic,
       Bubble,
       BackgroundMusicNeu,
       EndSceneSoundNeu,
       BubbleNeu,
       FlySound,
       FlySoundNeu
        */
        // Hier kommen die importieren Sound namen rein die Audios m�ssen in den Ressources ordner reinkopiert werden
        Intro,
        WinterAmbience
    }

    public void Init()
    {
        if (soundGameObject == null)
        {
            soundGameObject = new GameObject("Sound");
            audioSource = soundGameObject.AddComponent<AudioSource>();
           

            //add Sounds to List
            /* soundList.Add(Resources.Load<AudioClip>("Audio/Back"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Coat_02"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Confirm"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Fall_01"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Jump_01"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Quik_01"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Select_01"));
            soundList.Add(Resources.Load<AudioClip>("Audio/BackgroundMusic"));
            soundList.Add(Resources.Load<AudioClip>("Audio/Bubble"));
            soundList.Add(Resources.Load<AudioClip>("Audio/BackgroundMusicNeu"));
            soundList.Add(Resources.Load<AudioClip>("Audio/EndSceneSoundNeu"));
            soundList.Add(Resources.Load<AudioClip>("Audio/BubbleNeu"));
            soundList.Add(Resources.Load<AudioClip>("Audio/FlySound"));
            soundList.Add(Resources.Load<AudioClip>("Audio/FlySoundNeu")); */
            soundList.Add(Resources.Load<AudioClip>("Audio/Intro"));
            soundList.Add(Resources.Load<AudioClip>("Audio/WinterAmbience"));


        }
    }

    public void PlaySound(Sound sound, bool check = false)
    {
        if (check)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(soundList[(int)sound]);
            }
        }
        else
        {
            audioSource.PlayOneShot(soundList[(int)sound]);
        }
    }

}