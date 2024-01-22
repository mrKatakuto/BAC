using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelBtn(string levelToLoad) 
    {

        loadingScreen.SetActive(true);
        loadingSlider.value = 0f; 

        mainMenu.SetActive(false);
        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    IEnumerator LoadLevelASync(string levelToLoad) 
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        // bei false bleibt es hängen
        loadOperation.allowSceneActivation = false;

        float minLoadTime = 5f; 
        //float startTime = Time.time;
        
        float timeElapsed =  0;

        while (!loadOperation.isDone)
        {
            timeElapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(timeElapsed / minLoadTime);

            
            loadingSlider.value = progress;

            if (timeElapsed >= minLoadTime)
            {
                Debug.Log("Scene ready to activate");
                loadOperation.allowSceneActivation = true;
                
                // Log-Ausgabe hinzufügen, um zu bestätigen, dass die Szene zur Aktivierung bereit ist
                Debug.Log($"Activating scene: {levelToLoad}");
            }

            yield return null;
        }
    }
}
