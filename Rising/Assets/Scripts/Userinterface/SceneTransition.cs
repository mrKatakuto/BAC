using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Canvas mainMenuCanvas; // Referenz auf das Hauptmenü-Canvas
    public Canvas canvas; // Ladebildschirm-Canvas
    public CanvasGroup canvasGroup;
    public Slider slider;
    public float transitionDuration = 20.0f;

    private int nextSceneIndex;

    void Awake() 
    {
        canvas.gameObject.SetActive(false); // Deaktiviere das Ladebildschirm-Canvas zu Beginn
        Debug.Log("Ladebildschirm-Canvas deaktiviert.");
    }

    void Start()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Nächste Szene Index: " + nextSceneIndex);

        canvasGroup.alpha = 1f; // Starte mit sichtbarem Ladebildschirm
        slider.value = 0f; // Startwert des Sliders
    }

    public void StartSceneTransition()
    {
    Debug.Log("StartSceneTransition aufgerufen");
        if (mainMenuCanvas != null)
        mainMenuCanvas.gameObject.SetActive(false);

    canvas.gameObject.SetActive(true);
    StartCoroutine(LoadNextSceneWithProgress());
    }

    IEnumerator LoadNextSceneWithProgress()
    {
        float time = 0;

        while (time < transitionDuration)
        {
            time += Time.deltaTime;
            float progress = time / transitionDuration; // Fortschritt von 0 bis 1

            slider.value = progress; // Aktualisiere den Slider-Wert
            canvasGroup.alpha = 1 - progress; // Fade-Out-Effekt

            yield return null;
        }

        Debug.Log("Lade nächste Szene: " + SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (mainMenuCanvas != null)
            mainMenuCanvas.enabled = true; // Aktiviere das Hauptmenü-Canvas wieder
        Debug.Log("Hauptmenü-Canvas reaktiviert.");
    }
}
