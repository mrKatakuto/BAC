using UnityEngine;
using System;
using TMPro;

public class SolarPanelActivation : MonoBehaviour
{
    public TimeController timeController;
    public GameObject interactionCanvas;  
    public CentralStationController centralStationController; 

    private TimeSpan activationStartTime = new TimeSpan(11, 50, 0);  
    private TimeSpan activationEndTime = new TimeSpan(12, 10, 0);    
    private bool isPlayerInTrigger = false; 

    public bool isActivated = false;  


    void Start()
    {
        SoundManager.Instance.Init(); 

        interactionCanvas.SetActive(false); 

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionCanvas.SetActive(true);
            SoundManager.Instance.PlaySound(SoundManager.Sound.Robot);
            //PauseGame();
            isPlayerInTrigger = true; 

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionCanvas.SetActive(false);
            //ResumeGame();
            isPlayerInTrigger = false; 
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (IsTimeToActivate(timeController.currentTime) && !isActivated)
            {
                ReportActivationToCentralController();
            }
            interactionCanvas.SetActive(false);
            //ResumeGame();
        }
    }

    private bool IsTimeToActivate(DateTime currentTime)
    {
        TimeSpan current = currentTime.TimeOfDay; 
        return current >= activationStartTime && current <= activationEndTime;
    }

    private void ReportActivationToCentralController()
    {
        centralStationController.ActivateStation("Solar");
        isActivated = true;  
        SoundManager.Instance.PlaySound(SoundManager.Sound.Activate);


    }

    private void PauseGame()
    {
        Time.timeScale = 0f;  
    }

    public void ResumeGame()  
    {
        Time.timeScale = 1f;  
    }
}
