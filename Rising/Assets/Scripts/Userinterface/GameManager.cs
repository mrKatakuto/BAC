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
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
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
        SetPlayerPosition();
    }

    // Rufen Sie diese Methode auf, wenn die Szene geladen wird, um die Spielerposition zu setzen
    public void SetPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPositionX") && player != null)
        {
            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");
            float z = PlayerPrefs.GetFloat("PlayerPositionZ");
            player.transform.position = new Vector3(x, y, z);
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
        SceneManager.sceneLoaded -= OnSceneLoaded; // Event Handler entfernen
    }
}
