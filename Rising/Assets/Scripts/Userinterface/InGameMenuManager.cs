using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    [SerializeField] private GameObject confirmationPrompt = null;
    public GameObject pauseMenuUI;
    private InputReader inputReader;

    private void Start()
    {
        inputReader = FindObjectOfType<InputReader>();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pausiert die Zeit im Spiel
        IsPaused = true;
        inputReader.enabled = false;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Setzt die Zeit im Spiel fort
        IsPaused = false;
        inputReader.enabled = true;

        StartCoroutine(ConfirmationBox());
    }

    public void ResumeGameESC()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        inputReader.enabled = true;
    }
    public void ResumeGameExit()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Setzt die Zeit im Spiel fort
        IsPaused = false;
        inputReader.enabled = true;
    }

    public void MainMenuButton() 
    {
        GameManager1.Instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }

    public IEnumerator ConfirmationBox() 
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmationPrompt.SetActive(false);
    }
}
