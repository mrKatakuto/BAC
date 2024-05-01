using UnityEngine;

public class AnimationAudioSync : MonoBehaviour
{
    public Animator animator;           
    public AudioSource runningSound;    
    public float speedThreshold = 0.1f; 

    void Start()
    {
        runningSound.loop = true;       
    }

    void Update()
    {

        float speed = animator.GetFloat("FreeLookSpeed"); 

        if (speed > speedThreshold)
        {
            if (!runningSound.isPlaying)
                runningSound.Play();
        }
        else
        {
            if (runningSound.isPlaying)
                runningSound.Stop();
        }
    }
}
