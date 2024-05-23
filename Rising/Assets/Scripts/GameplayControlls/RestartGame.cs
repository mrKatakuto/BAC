using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public GameObject infoPanel;
    public Button restartButton;

    void Awake()
    {
        infoPanel.SetActive(false);  
        
 
        restartButton.onClick.AddListener(ReloadLevel);
    }

    public void ShowDeathPanel()
    {
        infoPanel.SetActive(true);  
    }

    public void HideDeathPanel()
    {
        infoPanel.SetActive(false);  
    }

    public void ReloadLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);  
    }
}
