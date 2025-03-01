using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;


    [HeaderAttribute("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;


    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle FullScreenToggle;

    private int qualityLevel;
    private bool _isFullscreen;
    private float brightnesslevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;
    [SerializeField] private GameObject warning;


    [Header("Levels To Load")]
    public string newGameLevel;
    [SerializeField] private GameObject noSavedGameDialog = null;


    [Header("Resolution Dropdown")]
    public TMP_Dropdown  resolutionDropdown;
    private Resolution[] resolutions;

    [Header("On Click Sound")]
    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioClip buttonClickSound;

    [Header("References")]
     //public ASyncLoader asyncLoader;
     public Loader loader;

    private void Start() 
    {
        InitializeResolutionSettings();
        SoundManager.Instance.Init(); // Soundmanager wird gestartet
        SoundManager.Instance.PlaySound(SoundManager.Sound.Intro);

    }

    private void InitializeResolutionSettings()
    {
        // Konfiguriert die Auflösungs-Optionen im UI

        warning.SetActive(false);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetStartResolution();
    }

    private void SetStartResolution()
    {
        Resolution currentRes = Screen.currentResolution;
        SetResolution(Array.IndexOf(resolutions, currentRes));
    }

    public void SetResolution(int resolutionIndex) 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //Debug.Log($"Resolution set to: {resolution.width} x {resolution.height}, Fullscreen: {Screen.fullScreen}");
    }

    public void NewGameDialogYes() 
    {
        Debug.Log("NewGameDialogYes called");

        if (IsGameSaved())
        {
            warning.SetActive(true);
        }
        else 
        {
            DeleteSavedGame();
            StartNewGame();;
        }
    }

    public void StartNewGame() 
    {    Debug.Log("StartNewGame called");
    
        //asyncLoader.LoadLevelBtn(newGameLevel);
        //loader.LoadLevel(newGameLevel);
        loader.LoadLevel("Level_1_The_Discovery", "TransitionCanvas");

    }

    private bool IsGameSaved() 
    {
        return PlayerPrefs.HasKey("CurrentLevel");
    }

    private void DeleteSavedGame()
    {
        Debug.Log("DeleteSaved called");

        PlayerPrefs.DeleteKey("PlayerPositionX");
        PlayerPrefs.DeleteKey("PlayerPositionY");
        PlayerPrefs.DeleteKey("PlayerPositionZ");
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.Save();
    }

    public void ConfirmNewGame()
    {
        Debug.Log("Confirm game called");
        warning.SetActive(false);
        DeleteSavedGame(); 
        StartNewGame();
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            GameManager1.Instance.LoadGame();
            // Fragezeichen ob das so funktionieren wird.
            //loader.LoadLevel(newGameLevel);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        buttonClickAudioSource.PlayOneShot(buttonClickSound);
    }

    public void ExitButton() 
    {
        Application.Quit();
    }

    public void SetVolume(float volume) 
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply() 
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    
        StartCoroutine(ConfirmationBox());    
    }

    public void SetConntrollerSen(float sensitivity) 
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply() 
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            //invert y
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            //not invert
        }

        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness) 
    {
        brightnesslevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");

        //Erste möglichkeit brightness zu ändern
        RenderSettings.ambientIntensity = brightnesslevel;
    }

    public void SetQuality(int qualityIndex) 
    {
        qualityLevel = qualityIndex;
    }

    public void SetFullScreen(bool isFullscreen) 
    {
        _isFullscreen = isFullscreen;
    }

    public void GraphicsApply() 
    {
        PlayerPrefs.SetFloat("masterBrightness", brightnesslevel);
        //Change the brightness later

        PlayerPrefs.SetInt("masterQuality", qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullscreen ? 1 : 0 ));
        Screen.fullScreen = _isFullscreen;

        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType) 
    {
        if (MenuType == "Graphics")
        {
            // Reset brightness value
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            FullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            // Korrigiere das Zurücksetzen der Auflösung
            int defaultResolutionIndex = Array.IndexOf(resolutions, Screen.currentResolution);
            defaultResolutionIndex = defaultResolutionIndex >= 0 ? defaultResolutionIndex : 0;
            resolutionDropdown.value = defaultResolutionIndex;
            SetResolution(defaultResolutionIndex);

            GraphicsApply();
        }
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }
    }

    public IEnumerator ConfirmationBox() 
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmationPrompt.SetActive(false);
    }
}