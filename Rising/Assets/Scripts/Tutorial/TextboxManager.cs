using UnityEngine;
using System.Collections;

public class TextboxManager : MonoBehaviour
{
    public GameObject textBox1;
    public GameObject textBox2;
    public GameObject textBox3;

    private InputReader inputReader;

    void Start()
    {
        inputReader = FindObjectOfType<InputReader>();
        
        textBox1.SetActive(false);
        textBox2.SetActive(false);
        textBox3.SetActive(false);

        if (!PlayerPrefs.HasKey("PlayerPositionX"))
        {
            StartCoroutine(ShowTextbox1AfterDelay(5f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox1.SetActive(false);
            textBox2.SetActive(false);
            textBox3.SetActive(true);
            PauseGame();
        }
    }

    private IEnumerator ShowTextbox1AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textBox2.SetActive(false);
        textBox3.SetActive(false);
        textBox1.SetActive(true);
        PauseGame();
    }

    public void CloseTextbox()
    {
        textBox1.SetActive(false);
        textBox2.SetActive(false);
        textBox3.SetActive(false);
        ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; 
        if (inputReader != null)
        {
            inputReader.enabled = false;
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; 
        if (inputReader != null)
        {
            inputReader.enabled = true;
        }
    }
}
