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
    //private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;


    [Header("Resolution Dropdown")]
    public TMP_Dropdown  resolutionDropdown;
    private Resolution[] resolutions;

    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioClip buttonClickSound;

    public ASyncLoader asyncLoader;

    private void Start() 
    {

        warning.SetActive(false);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void SetResolution(int resolutionIndex) 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log($"Resolution set to: {resolution.width} x {resolution.height}, Fullscreen: {Screen.fullScreen}");
    }

    public void NewGameDialogYes() 
    {
        if (IsGameSaved())
        {
            warning.SetActive(true);
            
        }
        else 
        {
            asyncLoader.LoadLevelBtn(newGameLevel);
        }
    }

    public void StartNewGame() 
    {
        DeleteSavedGame();
        asyncLoader.LoadLevelBtn(newGameLevel);
    }

    private bool IsGameSaved() 
    {
        return PlayerPrefs.HasKey("CurrentLevel");
    }

    private void DeleteSavedGame()
    {
        PlayerPrefs.DeleteKey("PlayerPositionX");
        PlayerPrefs.DeleteKey("PlayerPositionY");
        PlayerPrefs.DeleteKey("PlayerPositionZ");
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.Save();
    }

    public void ConfirmNewGame()
    {
        warning.SetActive(false); 
        StartNewGame();
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            string levelToLoad = PlayerPrefs.GetString("CurrentLevel");
            asyncLoader.LoadLevelBtn(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
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
            //Reset brightness value
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            FullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;

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

    public void OnButtonClick()
    {
        buttonClickAudioSource.PlayOneShot(buttonClickSound);
    }

    private IEnumerator RotateConfirmationPrompt()
    {
        float rotationDuration = 2.0f; 
        float elapsedTime = 0;

        while (elapsedTime < rotationDuration)
        {
            
            confirmationPrompt.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 360 * Time.deltaTime / rotationDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        confirmationPrompt.GetComponent<RectTransform>().localRotation = Quaternion.identity;
    }

    public IEnumerator ConfirmationBox() 
    {
        confirmationPrompt.SetActive(true);
        StartCoroutine(RotateConfirmationPrompt());
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}