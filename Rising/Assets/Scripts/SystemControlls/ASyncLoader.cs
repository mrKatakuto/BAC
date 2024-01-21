using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelBtn(string levelToLoad) 
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    IEnumerator LoadLevelASync(string levelToLoad) 
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false;

        float minLoadTime = 10f; 
        float startTime = Time.time;

        while (!loadOperation.isDone)
        {
            
            float timeElapsed = Time.time - startTime;
            
            float progress = Mathf.Clamp01(timeElapsed / minLoadTime);
            loadingSlider.value = progress;

            
            if (timeElapsed >= minLoadTime && loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true; 
            }

            yield return null;
        }
    }
}

