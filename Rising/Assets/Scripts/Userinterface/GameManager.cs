using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject confirmationPrompt = null;
    public static GameManager Instance { get; private set; }
    public GameObject player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("GameManager instanziert und SceneLoaded Event registriert");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Zusätzliche Instanz von GameManager zerstört");
        }
    }

    public void SaveGame()
    {
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);
            PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();       
             Debug.Log($"Spiel gespeichert: Position = {player.transform.position}, Level = {SceneManager.GetActiveScene().name}");
        }
        else
        {
            Debug.LogWarning("GameManager: Kein Player-GameObject zum Speichern gefunden");
        }

        StartCoroutine(ConfirmationBox());
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            string levelToLoad = PlayerPrefs.GetString("CurrentLevel");
            SceneManager.LoadScene(levelToLoad);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Szene geladen: {scene.name}");

        SetPlayerPosition();
    }
 
    public void SetPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPositionX") && player != null)
        {
            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");
            float z = PlayerPrefs.GetFloat("PlayerPositionZ");
            player.transform.position = new Vector3(x, y, z);

            Debug.Log($"Spielerposition gesetzt: {player.transform.position}");
        
        }
        else
        {
            Debug.LogWarning("GameManager: Keine gespeicherte Position oder kein Player-GameObject gefunden");
        }
    }

    public IEnumerator ConfirmationBox() 
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
