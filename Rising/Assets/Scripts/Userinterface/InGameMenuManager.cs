using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    public GameObject pauseMenuUI;
    private InputReader inputReader;
    
    private void Start()
    {
        inputReader = FindObjectOfType<InputReader>();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Pausiere das Spiel
        IsPaused = true;
        inputReader.enabled = false;  
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  // Setze das Spiel fort
        IsPaused = false;
        inputReader.enabled = true;  
    }

    public void MainMenuButton() 
    {
        GameManager1.Instance.SaveGame(); // Speichere das Spiel vor dem Wechsel
        SceneManager.LoadScene("Main_Menu");
    }
}
