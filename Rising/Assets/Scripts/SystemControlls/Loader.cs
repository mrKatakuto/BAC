using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [System.Serializable]
    public class LoadingScreenInfo
    {
        public string name; 
        public GameObject loadingScreen; 
        public Slider loadingSlider; 
    }

    public GameObject currentLevelCanvas; 
    [Header("Loading Screens")]
    public LoadingScreenInfo[] loadingScreens;

    private float delayGlobal = 5f;

    public void LoadLevel(string levelToLoad, string loadingScreenName) 
    {
        StartCoroutine(LoadLevelWithDelay(levelToLoad, loadingScreenName, delayGlobal));
    }

    IEnumerator LoadLevelWithDelay(string levelToLoad, string loadingScreenName, float delay) 
    {
        LoadingScreenInfo selectedScreen = null;

        foreach (var screen in loadingScreens)
        {
            if (screen.name == loadingScreenName)
            {
                selectedScreen = screen;
                break;
            }
        }

        if (selectedScreen != null)
        {
            if (currentLevelCanvas != null)
            {
                currentLevelCanvas.SetActive(false);
            }

            selectedScreen.loadingScreen.SetActive(true);
            selectedScreen.loadingSlider.value = 0f;

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);
            asyncLoad.allowSceneActivation = false; 

            float totalDelayTime = delay;
            float timeElapsed = 0f;

            while (!asyncLoad.isDone)
            {
                timeElapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(timeElapsed / totalDelayTime); 

                selectedScreen.loadingSlider.value = progress;

                if (asyncLoad.progress >= 0.9f && timeElapsed >= delay)
                {
                    asyncLoad.allowSceneActivation = true; 
                }

                yield return null;
            }

            selectedScreen.loadingScreen.SetActive(false);
        }
        else
        {
            Debug.LogError("Ladebildschirm mit dem Namen " + loadingScreenName + " nicht gefunden.");
        }
    }
}
