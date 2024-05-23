using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReactorActivation : MonoBehaviour
{
    public Slider reactorSlider;
    public GameObject sliderGameObject;  
    public TextMeshProUGUI activationText;  
    public GameObject interactionCanvas;  
    public CentralStationController centralStationController; 
    public float sliderSpeed = 0.5f;  
    public bool isActivated = false;  
    public Image sliderFillImage;     
    public Color defaultColor = Color.red;   
    public Color activeColor = Color.green;  

    void Start()
    {
        SoundManager.Instance.Init(); 

        interactionCanvas.SetActive(false); 
        sliderGameObject.SetActive(false);
        reactorSlider.value = 0;
        sliderFillImage.color = defaultColor;  
    }

    void Update()
    {
        if (isActivated) return;

        if (interactionCanvas.activeSelf) {
            IncreaseSliderValue();  

            
            if (reactorSlider.value >= 70 && reactorSlider.value <= 90)
            {
                sliderFillImage.color = activeColor;
            }
            else
            {
                sliderFillImage.color = defaultColor;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.F) && reactorSlider.value >= 70 && reactorSlider.value <= 90 && !isActivated)
        {
            ActivateReactor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionCanvas.SetActive(true);
            sliderGameObject.SetActive(true);  
            SoundManager.Instance.PlaySound(SoundManager.Sound.Robot);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            interactionCanvas.SetActive(false);
            sliderGameObject.SetActive(false); 
            reactorSlider.value = 0;  
        }
    }

    private void ActivateReactor()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.Activate, 0.3f);
        centralStationController.ActivateStation("Reactor");
        isActivated = true;  
        interactionCanvas.SetActive(false);
        sliderGameObject.SetActive(false);  
        reactorSlider.value = 0;  
    }

    private void IncreaseSliderValue()
    {
        if (reactorSlider.value < 100)
            reactorSlider.value += sliderSpeed * Time.deltaTime; 
        else
            reactorSlider.value = 0;  
    }
}
