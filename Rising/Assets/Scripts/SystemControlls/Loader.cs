using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevel(string levelToLoad) 
    {
        loadingScreen.SetActive(true);
        loadingSlider.value = 0f;
        mainMenu.SetActive(false);
        StartCoroutine(LoadLevelWithDelay(levelToLoad, 5f)); // 5 Sekunden Verzögerung
    }

    IEnumerator LoadLevelWithDelay(string levelToLoad, float delay) 
    {
        float timeElapsed = 0f;

        while (timeElapsed < delay)
        {
            timeElapsed += Time.deltaTime;
            float progress = timeElapsed / delay;
            loadingSlider.value = progress;
            yield return null;
        }

        SceneManager.LoadScene(levelToLoad);
    }
}