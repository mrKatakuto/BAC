using UnityEngine;
using TMPro;
using System.Collections;

public class WindmillActivation : MonoBehaviour
{
    public WindmillRotation[] windmills;
    public AudioSource windSound;
    public GameObject interactionCanvas;
    public CentralStationController centralStationController;
    public bool isActivated = false;
    private bool isPlayerInTrigger = false;  
    private bool canActivate = false;        
    private float volumeMax = 0.1f;          

    void Start() 
    {
        interactionCanvas.SetActive(false);  
        windSound.volume = 0f;               
        StartCoroutine(ManageWindSound());   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !AreAnyWindmillsRotating() && !isActivated)
        {
            isPlayerInTrigger = true;  
            interactionCanvas.SetActive(true);  
            PauseGame();  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;  
            interactionCanvas.SetActive(false);  
            ResumeGame();  
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interactionCanvas.activeSelf && canActivate && isPlayerInTrigger)
        {
            ActivateAllWindmills();  
        }
    }

    public void ActivateAllWindmills()
    {
        foreach (var windmill in windmills)
        {
            windmill.ActivateRotation();  
        }
        isActivated = true;  
        centralStationController.ActivateStation("Windmill");  
        SoundManager.Instance.PlaySound(SoundManager.Sound.Activate, 0.3f);

        interactionCanvas.SetActive(false);  
        ResumeGame();  
    }

    bool AreAnyWindmillsRotating()
    {
        foreach (var windmill in windmills)
        {
            if (windmill.isRotating)
                return true;  
        }
        return false;  
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;  
    }

    public void ResumeGame() 
    {
        Time.timeScale = 1f;  
    }

    IEnumerator ManageWindSound()
    {
        while (!isActivated)
        {
            yield return new WaitForSeconds(Random.Range(10f, 20f));  
            StartCoroutine(PlayWindSound());  
        }
    }

    IEnumerator PlayWindSound()
    {
        float initialVolume = windSound.volume;
        
        while (windSound.volume < volumeMax)
        {
            windSound.volume += Time.deltaTime / 3f;  
            yield return null;
        }

        windSound.Play();  
        canActivate = true;  

        yield return new WaitForSeconds(windSound.clip.length);  

        while (windSound.volume > initialVolume)
        {
            windSound.volume -= Time.deltaTime / 3f;  
            yield return null;
        }

        canActivate = false;  
    }
}
